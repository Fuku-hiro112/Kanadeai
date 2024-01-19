using UnityEngine;

// UI����n�N���X�̑����g�N���X�@�ˑ��֌W��P��������
public class UIManager : MonoBehaviour
{
    // �{�^���V�X�e���́u�͂��E�������v�Ɋ���U��N���X
    [SerializeField]private ButtonSystem _buttonSystem;// ���ɂ�Botton����n��������ꍇ��ʂ���������Serialeze�Ŏ���Ă���悤�ɂ��Ă���
    // ������\��������N���X
    private DialogSystem _dialogSystem;
    // �t�F�[�h������N���X
    private Fade _fade;�@�@�@�@�@�@�@�@

    public DialogSystem DialogSystem { get => _dialogSystem;}
    public ButtonSystem ButtonSystem { get => _buttonSystem;}
    public Fade Fade { get => _fade;}

    void Start()
    {
        _fade = new Fade();
        _dialogSystem = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogSystem>();
    }
}
