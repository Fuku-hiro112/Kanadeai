using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    //�L�����N�^�[�̃X�s�[�h
    [SerializeField] private float _speed = 5f;
    //�L�����N�^�[�̃_�b�V������Ƃ��ɑ������X�s�[�h
    [SerializeField] private float _dashSpeed = 3f;
    //�L�����N�^�[�̃X�^�~�i���O�ɂȂ����Ƃ��̌���
    [SerializeField] private float _speedDown = 2f;
    //�L�����N�^�[�̍ő�X�^�~�i
    [SerializeField] private float _maxStamina = 100f;
    //�_�b�V�����̃X�^�~�i�̌���X�s�[�h
    [SerializeField] private float _staminaCost = 20f;
    //�X�^�~�i�񕜂̃X�s�[�h
    [SerializeField] private float _staminaRecovery = 10f;
    [SerializeField] private Image _image;
    //���݂̃X�^�~�i
    private float _currentStamina;
    private bool _lastStamina = false;
    private float _moveX;
    private Vector3 _turnLeft = new Vector3(1,1,1);
    private Vector3 _turnRight = new Vector3(-1,1,1);
    private Animator _myAnimator;
    private MoveState _moveState;
    private enum MoveState
    {
        Idle,
        Walk,
        Run,
        Tired
    }
    private void Start()
    {
        _currentStamina = _maxStamina;
        _moveState = MoveState.Idle;
        _myAnimator = GetComponent<Animator>();
    }
    public void HandleUpdate()
    {
        float staminaFillAmount = _currentStamina / _maxStamina;
        _image.fillAmount = staminaFillAmount;
        _moveX = Input.GetAxis("Horizontal");

        if (_moveX < 0f)
        {
            transform.localScale = _turnLeft;
        }
        else if (_moveX > 0f)
        {
            transform.localScale = _turnRight;
        }

        switch (_moveState)
        {
            case MoveState.Idle:
                IdleAction();// �Î~
                break;

            case MoveState.Walk:
                WalkAction();// ���s
                break;

            case MoveState.Run:
                RunAction();// �_�b�V��
                break;

            case MoveState.Tired:
                TiredAction();// ��J
                break;
        }
    }
    /// <summary>
    /// �Î~��
    /// </summary>
    public void IdleAction()
    {
        _myAnimator.SetTrigger("PlayerIdle");
        _myAnimator.SetBool("PlayerWalk", false);
        _myAnimator.SetBool("PlayerTired", false);
        RecoveryStamina();
        if (_moveX != 0 && !_lastStamina)
        {
            _moveState = MoveState.Walk;
        }
        if (_moveX != 0 && _lastStamina)
        {
            _moveState = MoveState.Tired;
        }
    }
    /// <summary>
    /// ���s��
    /// </summary>
    private void WalkAction()
    {
        transform.position += new Vector3(_moveX, 0f, 0f) * WalkSpeed();
        _myAnimator.SetBool("PlayerWalk", true);
        _myAnimator.SetBool("PlayerRun", false);
        RecoveryStamina();
        if (_moveX == 0)
        {
            _moveState = MoveState.Idle;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _moveState = MoveState.Run;
        }
    }
    /// <summary>
    /// �_�b�V����
    /// </summary>
    private void RunAction()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _myAnimator.SetBool("PlayerRun", true);
            float CurrentSpeed = _speed + _dashSpeed;
            float Speed = CurrentSpeed * Time.deltaTime;
            transform.position += new Vector3(_moveX, 0f, 0f) * Speed;
            _currentStamina -= _staminaCost * Time.deltaTime;
            //�X�^�~�i���Ȃ��Ȃ����Ƃ�
            if (_currentStamina <= 1f)
            {
                OutStamina();
            }
        }
        else
        {
            _moveState = MoveState.Walk;
        }
    }
    /// <summary>
    /// ��J��
    /// </summary>
    private void TiredAction()
    {
        transform.position += new Vector3(_moveX, 0f, 0f) * WalkSpeed();
        _myAnimator.SetBool("PlayerTired", true);
        RecoveryStamina();
        if (_moveX == 0)
        {
            _moveState = MoveState.Idle;
        }
        //�X�^�~�i���ő�ɂȂ����Ƃ�
        if (_currentStamina >= 99f)
        {
            MaxStamina();
        }
    }

    /// <summary>
    /// �����X�s�[�h
    /// </summary>
    /// <returns>float</returns>
    private float WalkSpeed()=>_speed * Time.deltaTime;
    /// <summary>
    /// �X�^�~�i���ő�ɂȂ����Ƃ��̏���
    /// </summary>
    void MaxStamina()
    {
        _currentStamina = _maxStamina;
        _speed += _speedDown;
        _lastStamina = false;
        _moveState = MoveState.Idle;
    }
    /// <summary>
    /// �X�^�~�i���Ȃ��Ȃ����Ƃ��̏���
    /// </summary>
    void OutStamina()
    {
        _currentStamina = 0f;
        _speed -= _speedDown;
        _lastStamina = true;
        _moveState = MoveState.Tired;
    }
    /// <summary>
    /// �X�^�~�i��
    /// </summary>
    void RecoveryStamina()
    {
        if (_currentStamina <= _maxStamina)
        {
            _currentStamina += _staminaRecovery * Time.deltaTime;
            _currentStamina = Mathf.Clamp(_currentStamina, 0f, _maxStamina);
        }
    }
}
/*
    /// <summary>
    /// �A�^�b�`�����I�u�W�F�N�g���ړ����邱�Ƃ��o����
    /// </summary>
    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(x, 0f, 0f) * Time.deltaTime * speed;
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed /= 2;
        }
    }*/
