using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitClick : MonoBehaviour, IClickAction
{
    [SerializeField] ItemData _keyItem;
    Fade _fade;
    private DialogSystem _dialogSystem;
    private IPlayerController _iplayerController;
    void Start()
    {
        _fade = new Fade();
        _dialogSystem = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogSystem>();
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void ClickAction()
    {
        _iplayerController.BusyStart();
        ClickLockDoor().Forget();
    }
    private async UniTask ClickLockDoor()
    {
        await _dialogSystem.TypeDialogAsync("開かない...", isClick: true);
        if (HasItem(_keyItem))
        {
            await _dialogSystem.TypeDialogAsync($"{_keyItem.name}を使いますか？");
            // ボタン処理
            ButtonSystem btnSystem = ButtonSystem.s_Instance;
            btnSystem.ButtonEnable(true);                       // 表示・非表示
            btnSystem.ButtonAddListener("YesButton", ClickYes); // Yesボタン処理割り当て
            btnSystem.ButtonAddListener("NoButton", ClickNo);  // No ボタン処理割り当て
        }
        else
        {
            _iplayerController.MoveStart();
        }
    }
    bool HasItem(ItemData item)=> Inventory.s_Instance.ItemList.Contains(item);
    /// <summary>
    /// Yesボタン処理
    /// </summary>
    /// <returns>UniTaskVoid</returns>
    private async UniTaskVoid ClickYes()
    {
        ButtonSystem.s_Instance.ButtonEnable(false);
        Inventory.s_Instance.Remove(_keyItem);
        await _dialogSystem.TypeDialogAsync("ドアが空いた。", isClick: true);
        //ゲーム終了
        SceneManager.LoadScene("GameClearScene");
    }
    /// <summary>
    /// Noボタン処理
    /// </summary>
    /// <returns>UniTaskVoid</returns>
    private void ClickNo()
    {
        ButtonSystem.s_Instance.ButtonEnable(false);
        _iplayerController.MoveStart();
    }
}
