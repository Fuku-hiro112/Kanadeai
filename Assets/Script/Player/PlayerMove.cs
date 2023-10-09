using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    //キャラクターのスピード
    [SerializeField] private float _speed = 5f;
    //キャラクターのダッシュするときに足されるスピード
    [SerializeField] private float _dashSpeed = 3f;
    //キャラクターのスタミナが０になったときの減速
    [SerializeField] private float _speedDown = 2f;
    //キャラクターの最大スタミナ
    [SerializeField] private float _maxStamina = 100f;
    //ダッシュ時のスタミナの減るスピード
    [SerializeField] private float _staminaCost = 20f;
    //スタミナ回復のスピード
    [SerializeField] private float _staminaRecovery = 10f;
    [SerializeField] private Image _image;
    //現在のスタミナ
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
                IdleAction();// 静止
                break;

            case MoveState.Walk:
                WalkAction();// 歩行
                break;

            case MoveState.Run:
                RunAction();// ダッシュ
                break;

            case MoveState.Tired:
                TiredAction();// 疲労
                break;
        }
    }
    /// <summary>
    /// 静止時
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
    /// 歩行時
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
    /// ダッシュ時
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
            //スタミナがなくなったとき
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
    /// 疲労時
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
        //スタミナが最大になったとき
        if (_currentStamina >= 99f)
        {
            MaxStamina();
        }
    }

    /// <summary>
    /// 歩くスピード
    /// </summary>
    /// <returns>float</returns>
    private float WalkSpeed()=>_speed * Time.deltaTime;
    /// <summary>
    /// スタミナが最大になったときの処理
    /// </summary>
    void MaxStamina()
    {
        _currentStamina = _maxStamina;
        _speed += _speedDown;
        _lastStamina = false;
        _moveState = MoveState.Idle;
    }
    /// <summary>
    /// スタミナがなくなったときの処理
    /// </summary>
    void OutStamina()
    {
        _currentStamina = 0f;
        _speed -= _speedDown;
        _lastStamina = true;
        _moveState = MoveState.Tired;
    }
    /// <summary>
    /// スタミナ回復
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
    /// アタッチしたオブジェクトを移動することが出来る
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
