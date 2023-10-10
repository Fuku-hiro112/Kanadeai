using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// �A�C�e�����N���b�N�����Ƃ��̏���
public class ItemClick : MonoBehaviour, IClickAction
{
    private UIManager _uiManager;
    private DropItem _dropItem;
    private IPlayerController _iplayerController;

    private void Start()
    {
        _dropItem = GetComponent<DropItem>();
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    public void ClickAction()
    {
        ClickItemAsync().Forget();
    }
    /// <summary>
    /// �A�C�e���N���b�N����
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid ClickItemAsync()
    {
        // Busy��Ԃֈڍs
        _iplayerController.BusyStart();

        await _uiManager.DialogSystem.TypeDialogAsync($"{_dropItem.Item.name}������B", isClick: true);
        if (_dropItem == null)// dropItem���Ȃ��ꍇ
        {
            Debug.Log("<color=red>dropItem������܂���</color>");
            return;
        }
        await _uiManager.DialogSystem.TypeDialogAsync("�E���܂����H");

        // �{�^���A�͂��E�������@�ɏ��������蓖�Ă�
        // �{�^���̕\���A��\��
        _uiManager.ButtonSystem.ButtonEnable(true);
        // �{�^���֏����̊��蓖��
        _uiManager.ButtonSystem.ButtonAddListener("YesButton",ClickYes);
        _uiManager.ButtonSystem.ButtonAddListener("NoButton", ClickNo);
    }

    private async UniTaskVoid�@ClickYes()
    {
        // �A�C�e�����擾
        _dropItem.GetItem();
        _uiManager.ButtonSystem.ButtonEnable(false);
        await _uiManager.DialogSystem.TypeDialogAsync($"{_dropItem.Item.name}���擾�����B", isClick: true);
        _uiManager.DialogSystem.TextInvisible();
        // ������悤�ɂȂ�
        _iplayerController.MoveStart();
    }
    private void ClickNo() 
    {
        _uiManager.ButtonSystem.ButtonEnable(false);
        _uiManager.DialogSystem.TextInvisible();
        _iplayerController.MoveStart();
    }
}
