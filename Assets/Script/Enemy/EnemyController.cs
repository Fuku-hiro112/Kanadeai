using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour ,IEnemyController
{
    [SerializeField] private byte alpha = 10;
    [SerializeField] private int ms = 2;
    private IGameManager _igameManager;
    private EnemyMove _enemyMove;
    private EnemyRemove _enemyRemove;
    private Fade _fade; 
    private SpriteRenderer _enemySprite;
    private CancellationToken _token;

    public enum EnemyState
    {
        Busy,
        ReMove,
        Moving
    }
    private EnemyState _enemyControllerState;

    void Start()
    {
        _igameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _token = this.GetCancellationTokenOnDestroy();// this付けないとエラーが出る
        _fade  = new Fade();
        _enemyRemove = GetComponent<EnemyRemove>();
        _enemySprite = GetComponent<SpriteRenderer>();
        _enemyMove   = GetComponent<EnemyMove>();
        _enemyControllerState = EnemyState.Busy;
        EnemyAppearance().Forget();
    }

    public void HandleUpdate()
    {
        switch (_enemyControllerState)
        {
            case EnemyState.Busy:
                break;
            case EnemyState.ReMove:
                _enemyRemove.RemoveEnemy().Forget();
                _enemyControllerState = EnemyState.Busy;
                break;
            case EnemyState.Moving:
                _enemyMove.HandleUpdate();
                break;
        }
    }

    private async UniTaskVoid EnemyAppearance()
    {
        // SEなってから
        await _fade.FadeIn(alpha, ms, _enemySprite,_token);
        _enemyControllerState = EnemyState.Moving;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);

            _igameManager.StartGameOver();
        }
    }

    public void StartReMove()
    {
        _enemyControllerState = EnemyState.ReMove;
    }
    public void StartMoving()
    {
        _enemyControllerState = EnemyState.Moving;
    }
}
