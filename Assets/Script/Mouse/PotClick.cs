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

    //表示されるテキスト
    private async UniTaskVoid ClickPot()
    {
        _iplayerController.BusyStart();
        await _uiManager.DialogSystem.TypeDialogAsync("塩コショウで味付けされたスープがある", true);
        await _uiManager.DialogSystem.TypeDialogAsync("調理する？");
        //ボタンを呼び出す
        _uiManager.ButtonSystem.ButtonEnable(true);
        _uiManager.ButtonSystem.ButtonAddListener(_yesButton, YesButton);
        _uiManager.ButtonSystem.ButtonAddListener(_noButton, NoButton);
    }

    //「はい」を押されたとき
    private async UniTaskVoid YesButton()
    {
        _uiManager.ButtonSystem.ButtonEnable(false);
        _uiManager.DialogSystem.TextInvisible();
        if (HasFrogs())
        {
            RemoveSoupMaterial();
            Inventory.s_Instance.Add(_soup);
            await _uiManager.DialogSystem.TypeDialogAsync($"{_soup.name}を手に入れた", true);
        }
        else
        {
            await _uiManager.DialogSystem.TypeDialogAsync("具材が足りない", true);
        }
        _iplayerController.MoveStart();
    }

    //「いいえ」を押されたとき
    private void NoButton()
    {
        _uiManager.ButtonSystem.ButtonEnable(false);
        _uiManager.DialogSystem.TextInvisible();
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
