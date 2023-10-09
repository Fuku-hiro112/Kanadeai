using Cysharp.Threading.Tasks;
using UnityEngine;

public class PadlockClick : MonoBehaviour, IClickAction
{
    [SerializeField] private Canvas Padlock;

    private IPlayerController _iplayerController;
    private DialogSystem _dialogSystem;
    void Start()
    {
        Padlock.gameObject.SetActive(false);
        _dialogSystem      = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogSystem>();
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
        await _dialogSystem.TypeDialogAsync("鍵のかかった箱がある", true);
        await _dialogSystem.TypeDialogAsync("調べる？");
        // ボタン、はい・いいえ　に処理を割り当てる
        // ボタンの表示、非表示
        ButtonSystem buttonSystem = ButtonSystem.s_Instance;
        buttonSystem.ButtonEnable(true);
        // ボタンへ処理の割り当て
        buttonSystem.ButtonAddListener("YesButton", ClickYes);
        buttonSystem.ButtonAddListener("NoButton", ClickNo);
    }

    private void ClickYes()
    {
        _dialogSystem.TextInvisible();
        ButtonSystem.s_Instance.ButtonEnable(false);
        Padlock.gameObject.SetActive(true);
    }
    private void ClickNo()
    {
        _dialogSystem.TextInvisible();
        ButtonSystem.s_Instance.ButtonEnable(false);
        _iplayerController.MoveStart();
    }

}
