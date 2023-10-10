using Cysharp.Threading.Tasks;
using System;
using Random = UnityEngine.Random;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MirrorGimmick : MonoBehaviour, IClickAction
{
    [SerializeField] private RawImage _mirrorVideo;
    [SerializeField] private Image _mirrorImage;
    [SerializeField] private float _mirrorVideoTime = 6;//ビデオが流れてる時間待つ
    [SerializeField] private float _mirrorDisplayTime = 2;
    [SerializeField] private float _videoPercent = 1f;//動画が流れる確率
    private CancellationToken _token;
    private float _randomNumber;
    private IPlayerController _iplayerController;
    private UIManager _uiManager;

    void Start()
    {
        _uiManager         = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        _iplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _token             = this.GetCancellationTokenOnDestroy();

        _mirrorImage.gameObject.SetActive(false);
        _mirrorVideo.gameObject.SetActive(false);
    }

    public void ClickAction()
    {
        _iplayerController.BusyStart();
        RandomMirror().Forget();
    }

    private async UniTaskVoid RandomMirror()
    {
        _randomNumber = Random.Range(0f, 10f);
        //確率で当たったとき、動画を流す
        if (_randomNumber <= _videoPercent)
        {
            _mirrorVideo.gameObject.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(_mirrorVideoTime), cancellationToken: _token);
            _mirrorVideo.gameObject.SetActive(false);
            await _uiManager.DialogSystem.TypeDialogAsync("。。。。。。", true);
        }
        //外れた時、画像を出す
        else
        {
            _mirrorImage.gameObject.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(_mirrorDisplayTime), cancellationToken: _token);
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _token);
            _mirrorImage.gameObject.SetActive(false);
        }
        _iplayerController.MoveStart();
    }
}
