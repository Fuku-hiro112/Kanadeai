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
    private IPlayerController _iplayerController;
    private DialogSystem _dialogSystem;

    private void Start()
    {
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _dialogSystem = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogSystem>();
    }

    public void ClickAction()
    {
        ClickPot().Forget();
    }

    //�\�������e�L�X�g
    private async UniTaskVoid ClickPot()
    {
        _iplayerController.BusyStart();
        await _dialogSystem.TypeDialogAsync("���R�V���E�Ŗ��t�����ꂽ�X�[�v������", true);
        await _dialogSystem.TypeDialogAsync("��������H");
        //�{�^�����Ăяo��
        ButtonSystem.s_Instance.ButtonEnable(true);
        ButtonSystem.s_Instance.ButtonAddListener(_yesButton, YesButton);
        ButtonSystem.s_Instance.ButtonAddListener(_noButton, NoButton);
    }

    //�u�͂��v�������ꂽ�Ƃ�
    private async UniTaskVoid YesButton()
    {
        ButtonSystem.s_Instance.ButtonEnable(false);
        _dialogSystem.TextInvisible();
        if (HasFrogs())
        {
            RemoveSoupMaterial();
            Inventory.s_Instance.Add(_soup);
            await _dialogSystem.TypeDialogAsync($"{_soup.name}����ɓ��ꂽ", true);
        }
        else
        {
            await _dialogSystem.TypeDialogAsync("��ނ�����Ȃ�", true);
        }
        _iplayerController.MoveStart();
    }

    //�u�������v�������ꂽ�Ƃ�
    private void NoButton()
    {
        ButtonSystem.s_Instance.ButtonEnable(false);
        _dialogSystem.TextInvisible();
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
