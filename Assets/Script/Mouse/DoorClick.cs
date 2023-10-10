using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

public class DoorClick : MonoBehaviour, IClickAction
{
    [SerializeField] private ItemData _keyItem;
    [SerializeField] private bool _doorLock;
    [Inject] private IPlayerAreaMove _iplayerAreaMove;

    private UIManager _uiManager;
    private CancellationToken _token;
    private IPlayerController _iplayerController;

    void Start()
    {
        _token = this.GetCancellationTokenOnDestroy();
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
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
        await _uiManager.DialogSystem.TypeDialogAsync("鍵がかかっている。", isClick: true);
        if (HasKey(_keyItem))
        {
            await _uiManager.DialogSystem.TypeDialogAsync($"{_keyItem.name}を使いますか？");
            // ボタン処理
            _uiManager.ButtonSystem.ButtonEnable(true);                       // 表示・非表示
            _uiManager.ButtonSystem.ButtonAddListener("YesButton", ClickYes); // Yesボタン処理割り当て
            _uiManager.ButtonSystem.ButtonAddListener("NoButton" , ClickNo);  // No ボタン処理割り当て
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
        _uiManager.ButtonSystem.ButtonEnable(false);
        Inventory.s_Instance.Remove(_keyItem);
        _doorLock = false;
        await _uiManager.DialogSystem.TypeDialogAsync("ドアが空いた。", isClick: true);
        await ClickDoor();
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
