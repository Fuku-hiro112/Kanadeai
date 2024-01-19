using Cysharp.Threading.Tasks;
using UnityEngine;

public class PadlockClick : MonoBehaviour, IClickAction
{
    [SerializeField] private Canvas _padlock;
    private UIManager _uiManager;
    private IPlayerController _iplayerController;

    void Start()
    {
        _padlock.gameObject.SetActive(false);
        _uiManager         = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    //オブジェクトをクリックしたらテキストがこの表示される
    public void ClickAction()
    {
        ClickGimmick().Forget();
    }
    private async UniTaskVoid ClickGimmick()
    {
        _iplayerController.BusyStart();
        await _uiManager.DialogSystem.TypeDialogAsync("鍵のかかった箱がある", true);
        await _uiManager.DialogSystem.TypeDialogAsync("調べる？");
        // ボタン、はい・いいえ　に処理を割り当てる
        // ボタンの表示、非表示
        _uiManager.ButtonSystem.ButtonEnable(true);
        // ボタンへ処理の割り当て
        _uiManager.ButtonSystem.ButtonAddListener("YesButton", ClickYes);
        _uiManager.ButtonSystem.ButtonAddListener("NoButton", ClickNo);
    }

    private void ClickYes()
    {
        _uiManager.DialogSystem.TextInvisible();
        _uiManager.ButtonSystem.ButtonEnable(false);
        _padlock.gameObject.SetActive(true);
    }
    private void ClickNo()
    {
        _uiManager.DialogSystem.TextInvisible();
        _uiManager.ButtonSystem.ButtonEnable(false);
        _iplayerController.MoveStart();
    }

}
