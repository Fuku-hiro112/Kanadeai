using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

// �o���̃h�A�@���Ԃ������DoorClick�ɓ�������������
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
    /// <summary>
    /// �o���h�A�̏���
    /// </summary>
    /// <returns></returns>
    private async UniTask ClickLockDoor()
    {
        await _uiManager.DialogSystem.TypeDialogAsync("�J���Ȃ�...", isClick: true);
        if (HasItem(_keyItem))
        {
            await _uiManager.DialogSystem.TypeDialogAsync($"{_keyItem.name}���g���܂����H");
            // �{�^������
            _uiManager.ButtonSystem.ButtonEnable(true);                       // �\���E��\��
            _uiManager.ButtonSystem.ButtonAddListener("YesButton", ClickYes); // Yes�{�^���������蓖��
            _uiManager.ButtonSystem.ButtonAddListener("NoButton", ClickNo);  // No �{�^���������蓖��
        }
        else
        {
            _iplayerController.MoveStart();
        }
    }
    /// <summary>
    /// �A�C�e���������Ă��邩�ǂ���
    /// </summary>
    /// <param name="item">�w��A�C�e��</param>
    /// <returns>�����Ă��邩�ǂ���</returns>
    bool HasItem(ItemData item)=> Inventory.s_Instance.ItemList.Contains(item);
    /// <summary>
    /// Yes�{�^������
    /// </summary>
    /// <returns>UniTaskVoid</returns>
    private async UniTaskVoid ClickYes()
    {
        _uiManager.ButtonSystem.ButtonEnable(false);
        Inventory.s_Instance.Remove(_keyItem);
        await _uiManager.DialogSystem.TypeDialogAsync("�h�A���󂢂��B", isClick: true);
        //�Q�[���I��
        SceneManager.LoadScene("GameClearScene");
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
