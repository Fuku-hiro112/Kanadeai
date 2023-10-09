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
    /// �I���S�[���N���b�N����
    /// </summary>
    /// <returns>UniTaskVoid</returns>
    private async UniTaskVoid ClickMusicBox()
    {
        _iplayerController.BusyStart();
        await _dialogSystem.TypeDialogAsync("�I���S�[��������B",true);
        
        // ���ׂ�H�@�͂��A����������
        if (HasScrew(_goldScrew) || HasScrew(_silverScrew))
        {
            await _dialogSystem.TypeDialogAsync("���ׂ�H");
            // �{�^���̕\���A��\��
            ButtonSystem buttonSystem = ButtonSystem.s_Instance;
            buttonSystem.ButtonEnable(true);
            // �{�^���֏����̊��蓖�ā@
            buttonSystem.ButtonAddListener("YesButton", ClickCheckYes);
            buttonSystem.ButtonAddListener("NoButton", ClickNo);
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
        ButtonSystem.s_Instance.ButtonEnable(false);
        _dialogSystem.TextInvisible();
        _iplayerController.MoveStart();
    }
    /// <summary>
    /// ���ׂ�H�́u�͂��v����
    /// </summary>
    private async UniTaskVoid ClickCheckYes()
    {
        ButtonSystem buttonSystem = ButtonSystem.s_Instance;
        buttonSystem.ButtonEnable(false);
        _enemy.SetActive(true);// �G�o��

        // �l�W���g���܂����H�́@�͂��A�������������蓖��
        if (HasScrew(_goldScrew))
        {
            await _dialogSystem.TypeDialogAsync($"{_goldScrew.name}���g���܂����H");
            // �{�^���̕\���A��\��
            buttonSystem.ButtonEnable(true);
            // �{�^���֏����̊��蓖��
            buttonSystem.ButtonAddListener("YesButton", ClickTrueYes);
            buttonSystem.ButtonAddListener("NoButton", ClickNo);
        }
        else// ��̃l�W�̏ꍇ
        {
            await _dialogSystem.TypeDialogAsync($"{_silverScrew.name}���g���܂����H");
            // �{�^���̕\���A��\��
            buttonSystem.ButtonEnable(true);
            // �{�^���֏����̊��蓖��
            buttonSystem.ButtonAddListener("YesButton", ClickFalseYes);
            buttonSystem.ButtonAddListener("NoButton", ClickNo);
        }
    }
    /// <summary>
    /// ���̃l�W�̏ꍇ�̃l�W���g���܂����H�́u�͂��v����
    /// </summary>
    void ClickTrueYes() 
    {
        _dialogSystem.TextInvisible();
        ButtonSystem.s_Instance.ButtonEnable(false);
        _ienemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
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
        _dialogSystem.TextInvisible();
        ButtonSystem.s_Instance.ButtonEnable(false);
        //�I���S�[���̉����o��
        _audioSource.PlayOneShot(_falseSound);
        _iplayerController.MoveStart();
    }
}
