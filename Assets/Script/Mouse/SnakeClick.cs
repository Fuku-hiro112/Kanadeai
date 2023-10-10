using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class SnakeClick : MonoBehaviour,IClickAction
{
    [SerializeField] private Image _imageFade;
    [SerializeField] private ItemData _soup;
    [SerializeField] private int _alpha = 10;
    [SerializeField] private int _frame = 2;

    private string _yesButton = "YesButton";
    private string _noButton = "NoButton";
    private Collider2D _myCollider;
    private UIManager _uiManager;
    private CancellationToken _token;
    private IPlayerController _iplayerController;

    void Start()
    {
        _imageFade.gameObject.SetActive(false);
        _myCollider        = GetComponent<Collider2D>();
        _uiManager         = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _token = this.GetCancellationTokenOnDestroy();
    }

    public void ClickAction()
    {
        _iplayerController.BusyStart();
        DisplayText().Forget();
    }

    //蛇をクリックしたときの処理
    private async UniTaskVoid DisplayText()
    {
        await _uiManager.DialogSystem.TypeDialogAsync("蛇が邪魔で進めない", true);
        if (HasSoup(_soup))
        {
            await _uiManager.DialogSystem.TypeDialogAsync("カエルのスープを渡す？");
            _uiManager.ButtonSystem.ButtonEnable(true);
            _uiManager.ButtonSystem.ButtonAddListener(_yesButton, YesButton);
            _uiManager.ButtonSystem.ButtonAddListener(_noButton, NoButton);
        }
        else
        {
            _myCollider.enabled = true;
            _iplayerController.MoveStart();
        }
    }

    private bool HasSoup(ItemData item) => Inventory.s_Instance.ItemList.Contains(item);

    //「はい」を押されたとき
    private async UniTaskVoid YesButton()
    {
        _uiManager.ButtonSystem.ButtonEnable(false);
        Inventory.s_Instance.Remove(_soup);
        _imageFade.gameObject.SetActive(true);
        _uiManager.DialogSystem.TextInvisible();
        await _uiManager.Fade.FadeIn(_alpha, _frame, _imageFade, _token);
        gameObject.SetActive(false);
        await _uiManager.Fade.FadeOut(_alpha, _frame, _imageFade, _token);
        await _uiManager.DialogSystem.TypeDialogAsync("蛇がいなくなった", true);
        _imageFade.gameObject.SetActive(false);
        _iplayerController.MoveStart();
    }

    //「いいえ」を押されたとき
    private void NoButton()
    {
        _uiManager.ButtonSystem.ButtonEnable(false);
        _uiManager.DialogSystem.TextInvisible();
        _myCollider.enabled = true;
        _iplayerController.MoveStart();
    }
}
