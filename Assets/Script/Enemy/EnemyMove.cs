using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float _enemySpeed;

    private enemyMove _enemyMoveState;
    public enum enemyMove
    {
        Idle,
        Walk,
        Dash
    }
    void Start()
    {
        _enemyMoveState = enemyMove.Idle;
    }
    public void HandleUpdate()
    {
        switch (_enemyMoveState)
        {
            case enemyMove.Idle:
                EnemyIdle();
                break;

            case enemyMove.Walk:
                EnemyWalk();
                break;

            case enemyMove.Dash:
                EnemyDash();
                break;
        }
    }

    private void EnemyIdle()
    {
        _enemyMoveState = enemyMove.Walk;
    }

    private void EnemyWalk()
    {
        transform.position += new Vector3(-1, 0f, 0f) * _enemySpeed * Time.deltaTime;
    }

    private void EnemyDash()
    {

    }
}
