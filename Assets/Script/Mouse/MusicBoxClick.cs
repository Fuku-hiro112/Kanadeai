using Cysharp.Threading.Tasks;
using UnityEngine;

public class MusicBoxClick : MonoBehaviour, IClickAction
{
    [SerializeField] private ItemData _goldScrew;
    [SerializeField] private ItemData _silverScrew;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private AudioClip _trueSound;
    [SerializeField] private AudioClip _falseSound;
    private IPlayerController _iplayerController;
    private IEnemyController _ienemyController;
    private DialogSystem _dialogSystem;
    private AudioSource _audioSource;
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _dialogSystem = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogSystem>();
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
        await _dialogSystem.TypeDialogAsync("オルゴールがある。",true);
        
        // 調べる？　はい、いいえ処理
        if (HasScrew(_goldScrew) || HasScrew(_silverScrew))
        {
            await _dialogSystem.TypeDialogAsync("調べる？");
            // ボタンの表示、非表示
            ButtonSystem buttonSystem = ButtonSystem.s_Instance;
            buttonSystem.ButtonEnable(true);
            // ボタンへ処理の割り当て　
            buttonSystem.ButtonAddListener("YesButton", ClickCheckYes);
            buttonSystem.ButtonAddListener("NoButton", ClickNo);
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
        ButtonSystem.s_Instance.ButtonEnable(false);
        _dialogSystem.TextInvisible();
        _iplayerController.MoveStart();
    }
    /// <summary>
    /// 調べる？の「はい」処理
    /// </summary>
    private async UniTaskVoid ClickCheckYes()
    {
        ButtonSystem buttonSystem = ButtonSystem.s_Instance;
        buttonSystem.ButtonEnable(false);
        _enemy.SetActive(true);// 敵出現

        // ネジを使いますか？の　はい、いいえ処理割り当て
        if (HasScrew(_goldScrew))
        {
            await _dialogSystem.TypeDialogAsync($"{_goldScrew.name}を使いますか？");
            // ボタンの表示、非表示
            buttonSystem.ButtonEnable(true);
            // ボタンへ処理の割り当て
            buttonSystem.ButtonAddListener("YesButton", ClickTrueYes);
            buttonSystem.ButtonAddListener("NoButton", ClickNo);
        }
        else// 銀のネジの場合
        {
            await _dialogSystem.TypeDialogAsync($"{_silverScrew.name}を使いますか？");
            // ボタンの表示、非表示
            buttonSystem.ButtonEnable(true);
            // ボタンへ処理の割り当て
            buttonSystem.ButtonAddListener("YesButton", ClickFalseYes);
            buttonSystem.ButtonAddListener("NoButton", ClickNo);
        }
    }
    /// <summary>
    /// 金のネジの場合のネジを使いますか？の「はい」処理
    /// </summary>
    void ClickTrueYes() 
    {
        _dialogSystem.TextInvisible();
        ButtonSystem.s_Instance.ButtonEnable(false);
        _ienemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        //オルゴールの音が出る
        _audioSource.PlayOneShot(_trueSound);
        //幽霊のステート変化
        _ienemyController.StartReMove();
    }
    /// <summary>
    /// 銀のネジの場合のネジを使いますか？の「はい」処理
    /// </summary>
    void ClickFalseYes()
    {
        _dialogSystem.TextInvisible();
        ButtonSystem.s_Instance.ButtonEnable(false);
        //オルゴールの音が出る
        _audioSource.PlayOneShot(_falseSound);
        _iplayerController.MoveStart();
    }
}
