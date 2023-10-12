using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public enum EnemyState
{
    Busy,
    ReMove,
    Moving
}
// 敵の管理
public class EnemyController : MonoBehaviour ,IEnemyController
{
    [SerializeField] private byte alpha = 10;
    [SerializeField] private int ms = 2;

    private EnemyState _enemyControllerState;
    private IGameManager _igameManager;
    private EnemyMove _enemyMove;
    private EnemyRemove _enemyRemove;
    private UIManager _uiManager;
    private SpriteRenderer _enemySprite;
    private CancellationToken _token;

    void Start()
    {
        _uiManager    = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        _igameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _enemyRemove = GetComponent<EnemyRemove>();
        _enemySprite = GetComponent<SpriteRenderer>();
        _enemyMove   = GetComponent<EnemyMove>();
        _enemyControllerState = EnemyState.Busy;
        _token = this.GetCancellationTokenOnDestroy();// this付けないとエラーが出る

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
        await _uiManager.Fade.FadeIn(alpha, ms, _enemySprite,_token);
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
