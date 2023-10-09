using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// アイテムをクリックしたときの処理
public class ItemClick : MonoBehaviour, IClickAction
{
    private DialogSystem _dialogSystem;
    private DropItem _dropItem;
    private IPlayerController _iplayerController;

    private void Start()
    {
        _dropItem = GetComponent<DropItem>();
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _dialogSystem      = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogSystem>();
    }

    public void ClickAction()
    {
        ClickItemAsync().Forget();
    }
    /// <summary>
    /// アイテムクリック処理
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid ClickItemAsync()
    {
        // Busy状態へ移行
        _iplayerController.BusyStart();

        await _dialogSystem.TypeDialogAsync($"{_dropItem.Item.name}がある。", isClick: true);
        if (_dropItem == null)// dropItemがない場合
        {
            Debug.Log("<color=red>dropItemがありません</color>");
            return;
        }
        await _dialogSystem.TypeDialogAsync("拾いますか？");

        // ボタン、はい・いいえ　に処理を割り当てる
        // ボタンの表示、非表示
        ButtonSystem.s_Instance.ButtonEnable(true);
        // ボタンへ処理の割り当て
        ButtonSystem.s_Instance.ButtonAddListener("YesButton",ClickYes);
        ButtonSystem.s_Instance.ButtonAddListener("NoButton", ClickNo);
    }

    private async UniTaskVoid　ClickYes()
    {
        // アイテムを取得
        _dropItem.GetItem();
        ButtonSystem.s_Instance.ButtonEnable(false);
        await _dialogSystem.TypeDialogAsync($"{_dropItem.Item.name}を取得した。", isClick: true);
        _dialogSystem.TextInvisible();
        // 動けるようになる
        _iplayerController.MoveStart();
    }
    private void ClickNo() 
    {
        ButtonSystem.s_Instance.ButtonEnable(false);
        _dialogSystem.TextInvisible();
        _iplayerController.MoveStart();
    }
}
