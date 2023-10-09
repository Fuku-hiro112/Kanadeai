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

    //表示されるテキスト
    private async UniTaskVoid ClickPot()
    {
        _iplayerController.BusyStart();
        await _dialogSystem.TypeDialogAsync("塩コショウで味付けされたスープがある", true);
        await _dialogSystem.TypeDialogAsync("調理する？");
        //ボタンを呼び出す
        ButtonSystem.s_Instance.ButtonEnable(true);
        ButtonSystem.s_Instance.ButtonAddListener(_yesButton, YesButton);
        ButtonSystem.s_Instance.ButtonAddListener(_noButton, NoButton);
    }

    //「はい」を押されたとき
    private async UniTaskVoid YesButton()
    {
        ButtonSystem.s_Instance.ButtonEnable(false);
        _dialogSystem.TextInvisible();
        if (HasFrogs())
        {
            RemoveSoupMaterial();
            Inventory.s_Instance.Add(_soup);
            await _dialogSystem.TypeDialogAsync($"{_soup.name}を手に入れた", true);
        }
        else
        {
            await _dialogSystem.TypeDialogAsync("具材が足りない", true);
        }
        _iplayerController.MoveStart();
    }

    //「いいえ」を押されたとき
    private void NoButton()
    {
        ButtonSystem.s_Instance.ButtonEnable(false);
        _dialogSystem.TextInvisible();
        _iplayerController.MoveStart();
    }


    //消すアイテム
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
