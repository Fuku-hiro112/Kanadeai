using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

public enum PlayerState
{
    Moving,
    AreaMoving,
    Busy
}

public class PlayerController : MonoBehaviour, IPlayerController
{
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private AreaMoveData _areaMoveData;
    [SerializeField] private MouseClick _mouseClick;
    [Inject] private IPlayerAreaMove _iplayerAreaMove;// 依存性注入(ID)

    private PlayerState _playerState;
    private CancellationToken token;

    void Start()
    {
        _playerState = PlayerState.Moving;
        token = this.GetCancellationTokenOnDestroy();
    }
    /// <summary>
    /// GameManagerから呼び出す
    /// </summary>
    public void HandleFixedUpdate()
    {
        switch (_playerState)
        {
            case PlayerState.Moving:
                _playerMove.HandleUpdate();
                break;

            default: break;
        }
    }
    /// <summary>
    /// GameManagerから呼び出す
    /// </summary>
    public void HandleUpdate()
    {
        // カメラの位置を補正
        Vector3 _cameraPosition = new Vector3(transform.position.x, transform.position.y, -10);
        Camera.main.transform.position = _cameraPosition;
        switch (_playerState)
        {
            case PlayerState.Moving:
                _mouseClick.HandolUpdate();
                break;

            case PlayerState.AreaMoving:
                _playerMove.IdleAction();
                break;

            case PlayerState.Busy:
                _playerMove.IdleAction();
                break;

            default: break;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Stairs"))
        {
            _iplayerAreaMove.FloorMove(other.gameObject, token).Forget();
        }
        else if (other.gameObject.CompareTag("Clickable"))
        {
            try
            {
                other.transform.parent.GetComponent<AudioSource>().Play();
            }
            catch{}
            finally
            {
                other.gameObject.transform.parent.GetComponent<Collider2D>().enabled = true;
            }
        }
        else{}
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Clickable"))
        {
            try
            {
                other.transform.parent.GetComponent<AudioSource>().Stop();
            }// catchのExseptionの内容を記述する必要がある
            catch{}
            finally
            {
                other.gameObject.transform.parent.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    /// <summary>
    /// PlayerState変化関数
    /// </summary>
    public void MoveStart()
    {
        _playerState = PlayerState.Moving;
    }
    public void AreaMoveStart()
    {
        _playerState = PlayerState.AreaMoving;
    }
    public void BusyStart()
    {
        _playerState = PlayerState.Busy;
    }
}
