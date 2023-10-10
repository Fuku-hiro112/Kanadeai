using Cysharp.Threading.Tasks;
using UnityEngine;

public class PotClick : MonoBehaviour, IClickAction
{
    [SerializeField] private ItemData _frog1;
    [SerializeField] private ItemData _frog2;
    [SerializeField] private ItemData _frog3;
    [SerializeField] private ItemData _frogEgg;
    [SerializeField] private ItemData _soup;

    private string _yesButton = "YesButton";
    private string _noButton = "NoButton";
    private UIManager _uiManager;
    private IPlayerController _iplayerController;

    private void Start()
    {
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    public void ClickAction()
    {
        ClickPot().Forget();
    }

    //�\�������e�L�X�g
    private async UniTaskVoid ClickPot()
    {
        _iplayerController.BusyStart();
        await _uiManager.DialogSystem.TypeDialogAsync("���R�V���E�Ŗ��t�����ꂽ�X�[�v������", true);
        await _uiManager.DialogSystem.TypeDialogAsync("��������H");
        //�{�^�����Ăяo��
        _uiManager.ButtonSystem.ButtonEnable(true);
        _uiManager.ButtonSystem.ButtonAddListener(_yesButton, YesButton);
        _uiManager.ButtonSystem.ButtonAddListener(_noButton, NoButton);
    }

    //�u�͂��v�������ꂽ�Ƃ�
    private async UniTaskVoid YesButton()
    {
        _uiManager.ButtonSystem.ButtonEnable(false);
        _uiManager.DialogSystem.TextInvisible();
        if (HasFrogs())
        {
            RemoveSoupMaterial();
            Inventory.s_Instance.Add(_soup);
            await _uiManager.DialogSystem.TypeDialogAsync($"{_soup.name}����ɓ��ꂽ", true);
        }
        else
        {
            await _uiManager.DialogSystem.TypeDialogAsync("��ނ�����Ȃ�", true);
        }
        _iplayerController.MoveStart();
    }

    //�u�������v�������ꂽ�Ƃ�
    private void NoButton()
    {
        _uiManager.ButtonSystem.ButtonEnable(false);
        _uiManager.DialogSystem.TextInvisible();
        _iplayerController.MoveStart();
    }


    //�����A�C�e��
    private void RemoveSoupMaterial()
    {
        Inventory.s_Instance.Remove(_frog1);
        Inventory.s_Instance.Remove(_frog2);
        Inventory.s_Instance.Remove(_frog3);
        Inventory.s_Instance.Remove(_frogEgg);
    }

    private bool HasFrog(ItemData item) => Inventory.s_Instance.ItemList.Contains(item);
    private bool HasFrogs()=> HasFrog(_frog1) && HasFrog(_frog2) && HasFrog(_frog3) && HasFrog(_frogEgg);
}
