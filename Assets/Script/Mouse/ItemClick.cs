using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// �A�C�e�����N���b�N�����Ƃ��̏���
public class ItemClick : MonoBehaviour, IClickAction
{
    private DialogSystem _dialogSystem;
    private DropItem _dropItem;
    private IPlayerController _iplayerController;

    private void Start()
    {
        _dropItem = GetComponent<DropItem>();
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _dialogSystem      = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogSystem>();
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

        await _dialogSystem.TypeDialogAsync($"{_dropItem.Item.name}������B", isClick: true);
        if (_dropItem == null)// dropItem���Ȃ��ꍇ
        {
            Debug.Log("<color=red>dropItem������܂���</color>");
            return;
        }
        await _dialogSystem.TypeDialogAsync("�E���܂����H");

        // �{�^���A�͂��E�������@�ɏ��������蓖�Ă�
        // �{�^���̕\���A��\��
        ButtonSystem.s_Instance.ButtonEnable(true);
        // �{�^���֏����̊��蓖��
        ButtonSystem.s_Instance.ButtonAddListener("YesButton",ClickYes);
        ButtonSystem.s_Instance.ButtonAddListener("NoButton", ClickNo);
    }

    private async UniTaskVoid�@ClickYes()
    {
        // �A�C�e�����擾
        _dropItem.GetItem();
        ButtonSystem.s_Instance.ButtonEnable(false);
        await _dialogSystem.TypeDialogAsync($"{_dropItem.Item.name}���擾�����B", isClick: true);
        _dialogSystem.TextInvisible();
        // ������悤�ɂȂ�
        _iplayerController.MoveStart();
    }
    private void ClickNo() 
    {
        ButtonSystem.s_Instance.ButtonEnable(false);
        _dialogSystem.TextInvisible();
        _iplayerController.MoveStart();
    }
}
