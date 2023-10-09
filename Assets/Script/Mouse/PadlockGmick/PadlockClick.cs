using Cysharp.Threading.Tasks;
using UnityEngine;

public class PadlockClick : MonoBehaviour, IClickAction
{
    [SerializeField] private Canvas Padlock;

    private IPlayerController _iplayerController;
    private DialogSystem _dialogSystem;
    void Start()
    {
        Padlock.gameObject.SetActive(false);
        _dialogSystem      = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogSystem>();
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    //�I�u�W�F�N�g���N���b�N������e�L�X�g�����̕\�������
    public void ClickAction()
    {
        ClickGimmick().Forget();
    }
    private async UniTaskVoid ClickGimmick()
    {
        _iplayerController.BusyStart();
        await _dialogSystem.TypeDialogAsync("���̂���������������", true);
        await _dialogSystem.TypeDialogAsync("���ׂ�H");
        // �{�^���A�͂��E�������@�ɏ��������蓖�Ă�
        // �{�^���̕\���A��\��
        ButtonSystem buttonSystem = ButtonSystem.s_Instance;
        buttonSystem.ButtonEnable(true);
        // �{�^���֏����̊��蓖��
        buttonSystem.ButtonAddListener("YesButton", ClickYes);
        buttonSystem.ButtonAddListener("NoButton", ClickNo);
    }

    private void ClickYes()
    {
        _dialogSystem.TextInvisible();
        ButtonSystem.s_Instance.ButtonEnable(false);
        Padlock.gameObject.SetActive(true);
    }
    private void ClickNo()
    {
        _dialogSystem.TextInvisible();
        ButtonSystem.s_Instance.ButtonEnable(false);
        _iplayerController.MoveStart();
    }

}
