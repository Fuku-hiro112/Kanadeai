using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class EnemyRemove : MonoBehaviour
{
    [SerializeField] private float _waitTime = 3f;
    [SerializeField] private SpriteRenderer _clearEnemyRenderer;
    [SerializeField] private GameObject _dropObject;
    private SpriteRenderer _enemyRenderer;
    private Collider2D _myCollider;
    private UIManager _uiManager;
    private IPlayerController _iplayerController;
    private CancellationToken _token;

    void Start()
    {
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        _myCollider    = GetComponent<Collider2D>();
        _enemyRenderer = GetComponent<SpriteRenderer>();
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _token = this.GetCancellationTokenOnDestroy();// this�t���Ȃ��ƃG���[���o��

        _dropObject.gameObject.SetActive(false);
    }
    /// <summary>
    /// Enemy����������
    /// </summary>
    /// <returns></returns>
    public async UniTaskVoid RemoveEnemy()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_waitTime), cancellationToken: _token);
        // �|���H��t�F�[�h�A�E�g
        _uiManager.Fade.FadeOut(10, 2, _enemyRenderer, _token).Forget();

        await UniTask.Delay(TimeSpan.FromSeconds(_waitTime), cancellationToken: _token);
        // �Y��ȗH��t�F�[�h�C��
        await _uiManager.Fade.FadeIn(10, 2, _clearEnemyRenderer, _token);

        await UniTask.Delay(TimeSpan.FromSeconds(_waitTime), cancellationToken: _token);
        // �Y��ȗH��t�F�[�h�A�E�g
        await _uiManager.Fade.FadeOut(10, 2, _clearEnemyRenderer, _token);

        // �H��������@Key���q�ɂ��Ă��邽�ߏ����Ȃ��̂ŃR���C�_�[�݂̂�OFF�ɂ��Ă���
        _myCollider.enabled = false;
        _dropObject.gameObject.SetActive(true);

        _iplayerController.MoveStart();
    }
}
