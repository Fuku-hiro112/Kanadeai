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
    /// バスタブクリック時
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid ClickBath()
    {
        await _uiManager.DialogSystem.TypeDialogAsync("濁った水がある",true);
        if (_getItem != null)
        {
            await _uiManager.DialogSystem.TypeDialogAsync("水を抜く？");

            //ボタンクリック処理、割り当て
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
        _uiManager.DialogSystem.TextInvisible();//文字を消す
        //画像変更
        GetComponent<SpriteRenderer>().sprite = _changeSprite;
        await _uiManager.DialogSystem.TypeDialogAsync($"{_getItem.name}がある", true);
        Inventory.s_Instance.Add(_getItem);//アイテム入手
        await _uiManager.DialogSystem.TypeDialogAsync($"{_getItem.name}を手に入れた", true);
        _getItem = null;
        _iplayerController.MoveStart();
    }
    private void ClickNo()
    {
        _uiManager.ButtonSystem.ButtonEnable(false);
        _uiManager.DialogSystem.TextInvisible();// 文字を消す

        _iplayerController.MoveStart();
    }
}
