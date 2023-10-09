using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using System;
using System.Threading;
using UnityEngine.SceneManagement;

public enum GameState
{// 画面フローごとにStateを用意してもよさそう
    Busy,
    Title,
    Play,
    GameOver
}
public class GameManager : MonoBehaviour, IGameManager
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private DialogSystem _dialogSystem;
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject _gameCanvas;


    [SerializeField] private GameObject _synopsisCanvas;
    [SerializeField] private GameObject _fadePanel;
    [SerializeField] private GameObject _textPanel;
    [SerializeField] private int _alpha;
    [SerializeField] private int _frame;
    [SerializeField] private Type[] _types;
    private CancellationToken _token;
    private Fade _fade;
    // シングルトンに　ゲーム中1つしか存在できないようにするため　シーン遷移でDestroyしないため
    void Start()
    {
        _token = this.GetCancellationTokenOnDestroy();
        _fade = new Fade();
        AudioManager.Instance.StopBGM(true);
        _gameState = GameState.Busy;
        _dialogSystem = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogSystem>();
        StartOP().Forget();
    }

    void Update()
    {
        switch (_gameState)
        {
            case GameState.Busy:
                break;
            case GameState.Play:
                _playerController.HandleUpdate();
                _enemyController.HandleUpdate();
                break;
            case GameState.GameOver:

                break;
        }
        
    }
    private void FixedUpdate()
    {
        switch(_gameState)
        {
            case GameState.Play:
                _playerController.HandleFixedUpdate();
                break;
            default: break;
        }
    }

    async UniTaskVoid StartOP()
    {
        _synopsisCanvas.SetActive(true);
        await _fade.FadeOut(_alpha, _frame, _fadePanel.GetComponent<Image>(), _token);
        await UniTask.Delay(2000);
        //テキストパネルをa170までフェードイン
        await _fade.FadeIn(_alpha,_frame,170,_textPanel.GetComponent<Image>(),_token);
        foreach (Type type in _types)
        {
            await _dialogSystem.TypeDialogAsync(type.Text,type.Letter, true);
        }
        _synopsisCanvas.SetActive(false);
        _gameState = GameState.Play;
    }

    public void StartGameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
[Serializable]
public class Type
{
    [SerializeField] private Text _text;
    [SerializeField] private string _letter;

    public Text Text { get => _text; set => _text = value; }
    public string Letter { get => _letter; }
}
