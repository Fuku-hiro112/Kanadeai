using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

// タイトル画面の操作
public class TitleController : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;
    [SerializeField] private Image _titleLogo;
    [SerializeField] private Image _titleTap;
    [SerializeField] private float _flashingSpeed = 1f;
    [SerializeField] private int _alpha = 10;
    [SerializeField] private int _frame = 2;
    [SerializeField] private float _titleLogoWaitTime;
    [SerializeField] private float _titleTapWaitTime;
    [SerializeField] private VideoPlayer _titleVideo;
    
    private bool _tapFlashing = false;
    private Fade _fade;
    private CancellationToken _token;

    void Start()
    {
        AudioManager.Instance.PlayBGM(BGMSoundData.BGM.Title,true);
        _token = this.GetCancellationTokenOnDestroy();
        _fade = new Fade();
        TitleUI().Forget();
    }

    //タイトル画面の動作
    private async UniTaskVoid TitleUI()
    {
        await _fade.FadeOut(_alpha, _frame, _fadeImage, _token);
        _titleVideo.Play();
        await UniTask.Delay(TimeSpan.FromSeconds(_titleLogoWaitTime), cancellationToken: _token);
        await _fade.FadeIn(_alpha, _frame, _titleLogo, _token);
        await UniTask.Delay(TimeSpan.FromSeconds(_titleTapWaitTime), cancellationToken: _token);
        await _fade.FadeIn(_alpha, _frame, _titleTap, _token);
        _tapFlashing = true;
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _token);
        await _fade.FadeIn(_alpha, _frame, _fadeImage, _token);
        SceneManager.LoadScene("GameScene");
    }

    private void Update()
    {
        if(_tapFlashing)
        {
            Flashing().Forget();
        }
    }


    //tapの点滅
    private async UniTaskVoid Flashing()
    {
        _tapFlashing = false;
        await _fade.FadeIn(_alpha, _frame, _titleTap, _token);
        await UniTask.Delay(TimeSpan.FromSeconds(_flashingSpeed), cancellationToken: _token);
        await _fade.FadeOut(_alpha, _frame, _titleTap, _token);
        await UniTask.Delay(TimeSpan.FromSeconds(_flashingSpeed), cancellationToken: _token);
        _tapFlashing = true;
    }
}
