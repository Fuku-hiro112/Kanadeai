using Cysharp.Threading.Tasks;
using UnityEngine;

public class BathClick : MonoBehaviour, IClickAction
{
    [SerializeField] private ItemData _getItem;
    [SerializeField] private Sprite _changeSprite;
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
        ClickBath().Forget();
    }
    /// <summary>
    /// �o�X�^�u�N���b�N��
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid ClickBath()
    {
        await _uiManager.DialogSystem.TypeDialogAsync("��������������",true);
        if (_getItem != null)
        {
            await _uiManager.DialogSystem.TypeDialogAsync("���𔲂��H");

            //�{�^���N���b�N�����A���蓖��
            _uiManager.ButtonSystem.ButtonEnable(true);
            _uiManager.ButtonSystem.ButtonAddListener("YesButton", ClickYes);
            _uiManager.ButtonSystem.ButtonAddListener("NoButton", ClickNo);
        }
        else
        {
            _iplayerController.MoveStart();
        }
    }
    private async UniTaskVoid ClickYes()
    {
        _uiManager.ButtonSystem.ButtonEnable(false);
        _uiManager.DialogSystem.TextInvisible();//����������
        //�摜�ύX
        GetComponent<SpriteRenderer>().sprite = _changeSprite;
        await _uiManager.DialogSystem.TypeDialogAsync($"{_getItem.name}������", true);
        Inventory.s_Instance.Add(_getItem);//�A�C�e������
        await _uiManager.DialogSystem.TypeDialogAsync($"{_getItem.name}����ɓ��ꂽ", true);
        _getItem = null;
        _iplayerController.MoveStart();
    }
    private void ClickNo()
    {
        _uiManager.ButtonSystem.ButtonEnable(false);
        _uiManager.DialogSystem.TextInvisible();// ����������

        _iplayerController.MoveStart();
    }
}
