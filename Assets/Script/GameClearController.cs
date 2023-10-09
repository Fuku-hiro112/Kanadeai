using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearController : MonoBehaviour
{
    //ƒRƒƒ“ƒg‘‚¯
    [SerializeField] private Image _blackPanel;
    [SerializeField] private Image _thankImage;
    [SerializeField] private Image _youImage;
    [SerializeField] private Image _forImage;
    [SerializeField] private Image _playingImage;
    [SerializeField] private int _alpha = 10;
    [SerializeField] private int _frame = 2;
    [SerializeField] private int _textAlpha = 10;
    [SerializeField] private int _textFrame = 2;
    [SerializeField] private float _sceneChangeWaitTime = 2;
    private CancellationToken _token;
    Fade _fade;
    void Start()
    {
        Application.targetFrameRate = 60;
        _fade = new Fade();
        _token = this.GetCancellationTokenOnDestroy();
        GameClearUI().Forget();
    }


    private async UniTaskVoid GameClearUI()
    {
        await _fade.FadeOut(_alpha, _frame, _blackPanel, _token);
        await _fade.FadeIn(_textAlpha, _textFrame, _thankImage, _token);
        await _fade.FadeIn(_textAlpha, _textFrame, _youImage, _token);
        await _fade.FadeIn(_textAlpha, _textFrame, _forImage, _token);
        await _fade.FadeIn(_textAlpha, _textFrame, _playingImage, _token);
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _token);
        await _fade.FadeIn(_alpha, _frame,  _blackPanel, _token);
        await UniTask.Delay(TimeSpan.FromSeconds(_sceneChangeWaitTime),cancellationToken:_token);
        SceneManager.LoadScene("TitleScene");
    }
}
