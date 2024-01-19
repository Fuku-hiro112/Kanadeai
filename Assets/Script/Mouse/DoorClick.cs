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
        // _iplayerController��l�X�ȏꏊ����Ăяo���Ă���̂��C��������
        _iplayerController.BusyStart();
        if (IsPass())
        {
            // �h�A�N���b�N����
            ClickDoor().Forget();
        }
        else
        {
            // ���t���h�A�N���b�N����
            ClickLockDoor().Forget();
        }
    }
    private bool IsPass() => !_doorLock;
    /// <summary>
    /// �h�A�N���b�N�����@�����Ɉړ�����
    /// </summary>
    /// <returns></returns>
    private async UniTask ClickDoor()
    {
        await _iplayerAreaMove.RoomMove(gameObject, _token);
        _iplayerController.MoveStart();
    }
    /// <summary>
    /// ���t���h�A�̃N���b�N�����@����������Ȃ�
    /// </summary>
    /// <returns></returns>
    private async UniTask ClickLockDoor()
    {
        await _uiManager.DialogSystem.TypeDialogAsync("�����������Ă���B", isClick: true);
        if (HasKey(_keyItem))
        {
            await _uiManager.DialogSystem.TypeDialogAsync($"{_keyItem.name}���g���܂����H");
            // �{�^������
            _uiManager.ButtonSystem.ButtonEnable(true);                       // �\���E��\��
            _uiManager.ButtonSystem.ButtonAddListener("YesButton", ClickYes); // Yes�{�^���������蓖��
            _uiManager.ButtonSystem.ButtonAddListener("NoButton" , ClickNo);  // No �{�^���������蓖��
        }
        else
        {
            _iplayerController.MoveStart();
        }
    }
    /// <summary>
    /// ���������Ă�����true��Ԃ�
    /// </summary>
    /// <returns>���������Ă��邩</returns>
    private bool HasKey(ItemData item) => Inventory.s_Instance.ItemList.Contains(item);
    
    /// <summary>
    /// Yes�{�^������
    /// </summary>
    /// <returns>UniTaskVoid</returns>
    private async UniTaskVoid ClickYes()
    {
        _uiManager.ButtonSystem.ButtonEnable(false);
        Inventory.s_Instance.Remove(_keyItem);
        _doorLock = false;
        await _uiManager.DialogSystem.TypeDialogAsync("�h�A���󂢂��B", isClick: true);
        await ClickDoor();
    }
    /// <summary>
    /// No�{�^������
    /// </summary>
    /// <returns>UniTaskVoid</returns>
    private void ClickNo()
    {
        _uiManager.ButtonSystem.ButtonEnable(false);
        _iplayerController.MoveStart();
    }
}
