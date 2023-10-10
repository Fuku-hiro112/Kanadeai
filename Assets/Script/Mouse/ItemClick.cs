using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// アイテムをクリックしたときの処理
public class ItemClick : MonoBehaviour, IClickAction
{
    private UIManager _uiManager;
    private DropItem _dropItem;
    private IPlayerController _iplayerController;

    private void Start()
    {
        _dropItem = GetComponent<DropItem>();
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
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

        await _uiManager.DialogSystem.TypeDialogAsync($"{_dropItem.Item.name}がある。", isClick: true);
        if (_dropItem == null)// dropItemがない場合
        {
            Debug.Log("<color=red>dropItemがありません</color>");
            return;
        }
        await _uiManager.DialogSystem.TypeDialogAsync("拾いますか？");

        // ボタン、はい・いいえ　に処理を割り当てる
        // ボタンの表示、非表示
        _uiManager.ButtonSystem.ButtonEnable(true);
        // ボタンへ処理の割り当て
        _uiManager.ButtonSystem.ButtonAddListener("YesButton",ClickYes);
        _uiManager.ButtonSystem.ButtonAddListener("NoButton", ClickNo);
    }

    private async UniTaskVoid　ClickYes()
    {
        // アイテムを取得
        _dropItem.GetItem();
        _uiManager.ButtonSystem.ButtonEnable(false);
        await _uiManager.DialogSystem.TypeDialogAsync($"{_dropItem.Item.name}を取得した。", isClick: true);
        _uiManager.DialogSystem.TextInvisible();
        // 動けるようになる
        _iplayerController.MoveStart();
    }
    private void ClickNo() 
    {
        _uiManager.ButtonSystem.ButtonEnable(false);
        _uiManager.DialogSystem.TextInvisible();
        _iplayerController.MoveStart();
    }
}
