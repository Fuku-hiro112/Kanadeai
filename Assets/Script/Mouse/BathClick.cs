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
    /// バスタブクリック時
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid ClickBath()
    {
        await _dialogSystem.TypeDialogAsync("濁った水がある",true);
        if (_getItem != null)
        {
            await _dialogSystem.TypeDialogAsync("水を抜く？");

            //ボタンクリック処理、割り当て
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
        _dialogSystem.TextInvisible();//文字を消す
        //画像変更
        GetComponent<SpriteRenderer>().sprite = _changeSprite;
        await _dialogSystem.TypeDialogAsync($"{_getItem.name}がある", true);
        Inventory.s_Instance.Add(_getItem);//アイテム入手
        await _dialogSystem.TypeDialogAsync($"{_getItem.name}を手に入れた", true);
        _getItem = null;
        _iplayerController.MoveStart();
    }
    private void ClickNo()
    {
        ButtonSystem.s_Instance.ButtonEnable(false);
        _dialogSystem.TextInvisible();// 文字を消す

        _iplayerController.MoveStart();
    }
}
