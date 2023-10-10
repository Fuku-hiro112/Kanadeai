using Cysharp.Threading.Tasks;
using UnityEngine;

public class PadlockClick : MonoBehaviour, IClickAction
{
    [SerializeField] private Canvas _padlock;
    private UIManager _uiManager;
    private IPlayerController _iplayerController;

    void Start()
    {
        _padlock.gameObject.SetActive(false);
        _uiManager         = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
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
        await _uiManager.DialogSystem.TypeDialogAsync("���̂���������������", true);
        await _uiManager.DialogSystem.TypeDialogAsync("���ׂ�H");
        // �{�^���A�͂��E�������@�ɏ��������蓖�Ă�
        // �{�^���̕\���A��\��
        _uiManager.ButtonSystem.ButtonEnable(true);
        // �{�^���֏����̊��蓖��
        _uiManager.ButtonSystem.ButtonAddListener("YesButton", ClickYes);
        _uiManager.ButtonSystem.ButtonAddListener("NoButton", ClickNo);
    }

    private void ClickYes()
    {
        _uiManager.DialogSystem.TextInvisible();
        _uiManager.ButtonSystem.ButtonEnable(false);
        _padlock.gameObject.SetActive(true);
    }
    private void ClickNo()
    {
        _uiManager.DialogSystem.TextInvisible();
        _uiManager.ButtonSystem.ButtonEnable(false);
        _iplayerController.MoveStart();
    }

}
