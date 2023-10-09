using Cysharp.Threading.Tasks;
using UnityEngine;

public class BathClick : MonoBehaviour, IClickAction
{
    [SerializeField] ItemData _getItem;
    [SerializeField] Sprite _changeSprite;

    private DialogSystem _dialogSystem;
    private IPlayerController _iplayerController;

    void Start()
    {
        _dialogSystem = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogSystem>();
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void ClickAction()
    {
        _iplayerController.BusyStart();
        ClickBath().Forget();
    }
    /// <summary>
    /// �o�X�^�u�N���b�N��
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid ClickBath()
    {
        await _dialogSystem.TypeDialogAsync("��������������",true);
        if (_getItem != null)
        {
            await _dialogSystem.TypeDialogAsync("���𔲂��H");

            //�{�^���N���b�N�����A���蓖��
            ButtonSystem buttonSystem = ButtonSystem.s_Instance;
            buttonSystem.ButtonEnable(true);
            ButtonSystem.s_Instance.ButtonAddListener("YesButton", ClickYes);
            ButtonSystem.s_Instance.ButtonAddListener("NoButton", ClickNo);
        }
        else
        {
            _iplayerController.MoveStart();
        }
    }
    private async UniTaskVoid ClickYes()
    {
        ButtonSystem.s_Instance.ButtonEnable(false);
        _dialogSystem.TextInvisible();//����������
        //�摜�ύX
        GetComponent<SpriteRenderer>().sprite = _changeSprite;
        await _dialogSystem.TypeDialogAsync($"{_getItem.name}������", true);
        Inventory.s_Instance.Add(_getItem);//�A�C�e������
        await _dialogSystem.TypeDialogAsync($"{_getItem.name}����ɓ��ꂽ", true);
        _getItem = null;
        _iplayerController.MoveStart();
    }
    private void ClickNo()
    {
        ButtonSystem.s_Instance.ButtonEnable(false);
        _dialogSystem.TextInvisible();// ����������

        _iplayerController.MoveStart();
    }
}
