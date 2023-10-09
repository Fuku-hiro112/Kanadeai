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
    private Fade _fade;
    private Collider2D _myCollider;
    private DialogSystem _dialogSystem;
    private IPlayerController _iplayerController;
    private CancellationToken _token;

    void Start()
    {
        _fade = new Fade();
        _imageFade.gameObject.SetActive(false);
        _myCollider        = GetComponent<Collider2D>();
        _dialogSystem      = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogSystem>();
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
        await _dialogSystem.TypeDialogAsync("蛇が邪魔で進めない", true);
        if (HasSoup(_soup))
        {
            await _dialogSystem.TypeDialogAsync("カエルのスープを渡す？");
            ButtonSystem.s_Instance.ButtonEnable(true);
            ButtonSystem.s_Instance.ButtonAddListener(_yesButton, YesButton);
            ButtonSystem.s_Instance.ButtonAddListener(_noButton, NoButton);
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
        ButtonSystem.s_Instance.ButtonEnable(false);
        Inventory.s_Instance.Remove(_soup);
        _imageFade.gameObject.SetActive(true);
        _dialogSystem.TextInvisible();
        await _fade.FadeIn(_alpha, _frame, _imageFade, _token);
        gameObject.SetActive(false);
        await _fade.FadeOut(_alpha, _frame, _imageFade, _token);
        await _dialogSystem.TypeDialogAsync("蛇がいなくなった", true);
        _imageFade.gameObject.SetActive(false);
        _iplayerController.MoveStart();
    }

    //「いいえ」を押されたとき
    private void NoButton()
    {
        ButtonSystem.s_Instance.ButtonEnable(false);
        _dialogSystem.TextInvisible();
        _myCollider.enabled = true;
        _iplayerController.MoveStart();
    }
}
