using Cysharp.Threading.Tasks;
using UnityEngine;

public class MusicBoxClick : MonoBehaviour, IClickAction
{
    [SerializeField] private ItemData _goldScrew;
    [SerializeField] private ItemData _silverScrew;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private AudioClip _trueSound;
    [SerializeField] private AudioClip _falseSound;

    private UIManager _uiManager;
    private AudioSource _audioSource;
    private IEnemyController _ienemyController;
    private IPlayerController _iplayerController;
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _ienemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
    }
    public void ClickAction()
    {
        ClickMusicBox().Forget();
    }
    /// <summary>
    /// �I���S�[���N���b�N����
    /// </summary>
    /// <returns>UniTaskVoid</returns>
    private async UniTaskVoid ClickMusicBox()
    {
        _iplayerController.BusyStart();
        await _uiManager.DialogSystem.TypeDialogAsync("�I���S�[��������B",true);
        
        // ���ׂ�H�@�͂��A����������
        if (HasScrew(_goldScrew) || HasScrew(_silverScrew))
        {
            await _uiManager.DialogSystem.TypeDialogAsync("���ׂ�H");
            // �{�^���̕\���A��\��
            _uiManager.ButtonSystem.ButtonEnable(true);
            // �{�^���֏����̊��蓖�ā@
            _uiManager.ButtonSystem.ButtonAddListener("YesButton", ClickCheckYes);
            _uiManager.ButtonSystem.ButtonAddListener("NoButton", ClickNo);
        }
        else // �l�W�������Ă��Ȃ��ꍇ
        {
            _iplayerController.MoveStart();
        }
    }
    /// <summary>
    /// �l�W�������Ă��邩�ǂ���
    /// </summary>
    /// <param name="item"></param>
    /// <returns>bool�l�W�������Ă��邩�ǂ���</returns>
    private bool HasScrew(ItemData item)=> Inventory.s_Instance.ItemList.Contains(item);
    /// <summary>
    ///�u�������v����
    /// </summary>
    private void ClickNo()
    {
        _uiManager.ButtonSystem.ButtonEnable(false);
        _uiManager.DialogSystem.TextInvisible();
        _iplayerController.MoveStart();
    }
    /// <summary>
    /// ���ׂ�H�́u�͂��v����
    /// </summary>
    private async UniTaskVoid ClickCheckYes()
    {
        _uiManager.ButtonSystem.ButtonEnable(false);
        _enemy.SetActive(true);// �G�o��

        // �l�W���g���܂����H�́@�͂��A�������������蓖��
        if (HasScrew(_goldScrew))
        {
            await _uiManager.DialogSystem.TypeDialogAsync($"{_goldScrew.name}���g���܂����H");
            // �{�^���̕\���A��\��
            _uiManager.ButtonSystem.ButtonEnable(true);
            // �{�^���֏����̊��蓖��
            _uiManager.ButtonSystem.ButtonAddListener("YesButton", ClickTrueYes);
            _uiManager.ButtonSystem.ButtonAddListener("NoButton", ClickNo);
        }
        else// ��̃l�W�̏ꍇ
        {
            await _uiManager.DialogSystem.TypeDialogAsync($"{_silverScrew.name}���g���܂����H");
            // �{�^���̕\���A��\��
            _uiManager.ButtonSystem.ButtonEnable(true);
            // �{�^���֏����̊��蓖��
            _uiManager.ButtonSystem.ButtonAddListener("YesButton", ClickFalseYes);
            _uiManager.ButtonSystem.ButtonAddListener("NoButton", ClickNo);
        }
    }
    /// <summary>
    /// ���̃l�W�̏ꍇ�̃l�W���g���܂����H�́u�͂��v����
    /// </summary>
    void ClickTrueYes() 
    {
        _uiManager.DialogSystem.TextInvisible();
        _uiManager.ButtonSystem.ButtonEnable(false);
        //�I���S�[���̉����o��
        _audioSource.PlayOneShot(_trueSound);
        //�H��̃X�e�[�g�ω�
        _ienemyController.StartReMove();
    }
    /// <summary>
    /// ��̃l�W�̏ꍇ�̃l�W���g���܂����H�́u�͂��v����
    /// </summary>
    void ClickFalseYes()
    {
        _uiManager.DialogSystem.TextInvisible();
        _uiManager.ButtonSystem.ButtonEnable(false);
        //�I���S�[���̉����o��
        _audioSource.PlayOneShot(_falseSound);
        _iplayerController.MoveStart();
    }
}
