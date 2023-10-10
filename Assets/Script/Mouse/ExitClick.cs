using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitClick : MonoBehaviour, IClickAction
{
    [SerializeField] ItemData _keyItem;
    private UIManager _uiManager;

    private IPlayerController _iplayerController;

    void Start()
    {
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void ClickAction()
    {
        _iplayerController.BusyStart();
        ClickLockDoor().Forget();
    }
    private async UniTask ClickLockDoor()
    {
        await _uiManager.DialogSystem.TypeDialogAsync("開かない...", isClick: true);
        if (HasItem(_keyItem))
        {
            await _uiManager.DialogSystem.TypeDialogAsync($"{_keyItem.name}を使いますか？");
            // ボタン処理
            _uiManager.ButtonSystem.ButtonEnable(true);                       // 表示・非表示
            _uiManager.ButtonSystem.ButtonAddListener("YesButton", ClickYes); // Yesボタン処理割り当て
            _uiManager.ButtonSystem.ButtonAddListener("NoButton", ClickNo);  // No ボタン処理割り当て
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
        _uiManager.ButtonSystem.ButtonEnable(false);
        Inventory.s_Instance.Remove(_keyItem);
        await _uiManager.DialogSystem.TypeDialogAsync("ドアが空いた。", isClick: true);
        //ゲーム終了
        SceneManager.LoadScene("GameClearScene");
    }
    /// <summary>
    /// Noボタン処理
    /// </summary>
    /// <returns>UniTaskVoid</returns>
    private void ClickNo()
    {
        _uiManager.ButtonSystem.ButtonEnable(false);
        _iplayerController.MoveStart();
    }
}
