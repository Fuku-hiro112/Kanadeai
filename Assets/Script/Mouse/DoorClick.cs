using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

public class DoorClick : MonoBehaviour, IClickAction
{
    [SerializeField] private ItemData _keyItem;
    [SerializeField] private bool _doorLock;
    [Inject] private IPlayerAreaMove _iplayerAreaMove;
    private DialogSystem _dialogSystem;
    private IPlayerController _iplayerController;
    private CancellationToken _token;

    void Start()
    {
        _token = this.GetCancellationTokenOnDestroy();
        _dialogSystem      = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogSystem>();
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public void ClickAction()
    {
        // _iplayerControllerを様々な場所から呼び出しているのを修正したい
        _iplayerController.BusyStart();
        if (IsPass())
        {
            // ドアクリック処理
            ClickDoor().Forget();
        }
        else
        {
            // 鍵付きドアクリック処理
            ClickLockDoor().Forget();
        }
    }
    private bool IsPass() => !_doorLock;
    /// <summary>
    /// ドアクリック処理　部屋に移動する
    /// </summary>
    /// <returns></returns>
    private async UniTask ClickDoor()
    {
        await _iplayerAreaMove.RoomMove(gameObject, _token);
        _iplayerController.MoveStart();
    }
    /// <summary>
    /// 鍵付きドアのクリック処理　鍵をあけるなど
    /// </summary>
    /// <returns></returns>
    private async UniTask ClickLockDoor()
    {
        await _dialogSystem.TypeDialogAsync("鍵がかかっている。", isClick: true);
        if (HasKey(_keyItem))
        {
            await _dialogSystem.TypeDialogAsync($"{_keyItem.name}を使いますか？");
            // ボタン処理
            ButtonSystem btnSystem = ButtonSystem.s_Instance;
            btnSystem.ButtonEnable(true);                       // 表示・非表示
            btnSystem.ButtonAddListener("YesButton", ClickYes); // Yesボタン処理割り当て
            btnSystem.ButtonAddListener("NoButton" , ClickNo);  // No ボタン処理割り当て
        }
        else
        {
            _iplayerController.MoveStart();
        }
    }
    /// <summary>
    /// 鍵を持っていたらtrueを返す
    /// </summary>
    /// <returns>鍵を持っているか</returns>
    private bool HasKey(ItemData item) => Inventory.s_Instance.ItemList.Contains(item);
    
    /// <summary>
    /// Yesボタン処理
    /// </summary>
    /// <returns>UniTaskVoid</returns>
    private async UniTaskVoid ClickYes()
    {
        ButtonSystem.s_Instance.ButtonEnable(false);
        Inventory.s_Instance.Remove(_keyItem);
        _doorLock = false;
        await _dialogSystem.TypeDialogAsync("ドアが空いた。", isClick: true);
        await ClickDoor();
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
