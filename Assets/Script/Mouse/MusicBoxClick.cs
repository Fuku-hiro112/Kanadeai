using Cysharp.Threading.Tasks;
using UnityEngine;

// オルゴールクリック処理　長いのでクラスやメソッドを分けよう
public class MusicBoxClick : MonoBehaviour, IClickAction
{
    // オルゴールを鳴らす為のネジ2種類
    [SerializeField] private ItemData _goldScrew;
    [SerializeField] private ItemData _silverScrew;
    // サウンド2種
    [SerializeField] private AudioClip _trueSound;
    [SerializeField] private AudioClip _falseSound;

    [SerializeField] private GameObject _enemy;

    private UIManager _uiManager;
    private AudioSource _audioSource;
    private IEnemyController _ienemyController;
    private IPlayerController _iplayerController;
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public void ClickAction()
    {
        ClickMusicBox().Forget();
    }
    /// <summary>
    /// オルゴールクリック処理
    /// </summary>
    /// <returns>UniTaskVoid</returns>
    private async UniTaskVoid ClickMusicBox()
    {
        _iplayerController.BusyStart();
        await _uiManager.DialogSystem.TypeDialogAsync("音のならないオルゴールがある。",true);
        
        // 調べる？　はい、いいえ処理
        if (HasScrew(_goldScrew) || HasScrew(_silverScrew))
        {
            await _uiManager.DialogSystem.TypeDialogAsync("調べる？");
            // ボタンの表示、非表示
            _uiManager.ButtonSystem.ButtonEnable(true);
            // ボタンへ処理の割り当て　
            _uiManager.ButtonSystem.ButtonAddListener("YesButton", ClickCheckYes);
            _uiManager.ButtonSystem.ButtonAddListener("NoButton", ClickNo);
        }
        else // ネジを持っていない場合
        {
            _iplayerController.MoveStart();
        }
    }
    /// <summary>
    /// ネジを持っているかどうか
    /// </summary>
    /// <param name="item"></param>
    /// <returns>boolネジを持っているかどうか</returns>
    private bool HasScrew(ItemData item)=> Inventory.s_Instance.ItemList.Contains(item);
    /// <summary>
    ///「いいえ」処理
    /// </summary>
    private void ClickNo()
    {
        _uiManager.ButtonSystem.ButtonEnable(false);
        _uiManager.DialogSystem.TextInvisible();
        _iplayerController.MoveStart();
    }
    /// <summary>
    /// 調べる？の「はい」処理
    /// </summary>
    private async UniTaskVoid ClickCheckYes()
    {
        _uiManager.ButtonSystem.ButtonEnable(false);
        _enemy.SetActive(true);// 敵出現

        // ネジを使いますか？の　はい、いいえ処理割り当て
        if (HasScrew(_goldScrew))
        {
            await _uiManager.DialogSystem.TypeDialogAsync($"{_goldScrew.name}を使いますか？");
            // ボタンの表示、非表示
            _uiManager.ButtonSystem.ButtonEnable(true);
            // ボタンへ処理の割り当て
            _uiManager.ButtonSystem.ButtonAddListener("YesButton", ClickTrueYes);
            _uiManager.ButtonSystem.ButtonAddListener("NoButton", ClickNo);
        }
        else// 銀のネジの場合
        {
            await _uiManager.DialogSystem.TypeDialogAsync($"{_silverScrew.name}を使いますか？");
            // ボタンの表示、非表示
            _uiManager.ButtonSystem.ButtonEnable(true);
            // ボタンへ処理の割り当て
            _uiManager.ButtonSystem.ButtonAddListener("YesButton", ClickFalseYes);
            _uiManager.ButtonSystem.ButtonAddListener("NoButton", ClickNo);
        }
    }
    /// <summary>
    /// 金のネジの場合のネジを使いますか？の「はい」処理
    /// </summary>
    void ClickTrueYes() 
    {
        _uiManager.DialogSystem.TextInvisible();
        _uiManager.ButtonSystem.ButtonEnable(false);
        // 初めはsetActive(false)なので、GetComponent出来ないためここでGetしている
        _ienemyController = _enemy.GetComponent<EnemyController>();
        // オルゴールの音が出る
        _audioSource.PlayOneShot(_trueSound);
        // 幽霊のステート変化
        _ienemyController.StartReMove();
    }
    /// <summary>
    /// 銀のネジの場合のネジを使いますか？の「はい」処理
    /// </summary>
    void ClickFalseYes()
    {
        _uiManager.DialogSystem.TextInvisible();
        _uiManager.ButtonSystem.ButtonEnable(false);
        //オルゴールの音が出る
        _audioSource.PlayOneShot(_falseSound);
        _iplayerController.MoveStart();
    }
}
