using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ゲームオーバー時の操作
public class GameOverController : MonoBehaviour
{
    //コメント書け
    [SerializeField] private RawImage _gameOverVideo;
    [SerializeField] private Text _returnTitleText;
    [SerializeField] private Image _blackParen; 
    [SerializeField] private int _alpha = 10;
    [SerializeField] private int _frame = 2;
    [SerializeField] private float _imageWaitTime = 3f;//画像が変わり始めるまでの時間
    [SerializeField] private float _textWaitTime = 3f;//テキストが出てくるまでの時間
    
    private Fade _fade;
    private CancellationToken _token;

    void Start()
    {
        Application.targetFrameRate = 60;
        _returnTitleText.gameObject.SetActive(false);
        _fade  = new Fade();
        _token = this.GetCancellationTokenOnDestroy();
        GameOverUI().Forget();
    }


    private async UniTaskVoid GameOverUI()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_imageWaitTime), cancellationToken: _token);
        //動画のStActiveを切ってる
        _gameOverVideo.gameObject.SetActive(false);

        //fadeOutする
        await _fade.FadeOut(_alpha, _frame, _blackParen, _token);
        await UniTask.Delay(TimeSpan.FromSeconds(_textWaitTime), cancellationToken: _token);

        //ゲームオーバーの画像を出す
        _returnTitleText.gameObject.SetActive(true);
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _token);

        //シーンを切り替える前にfadeInする
        await _fade.FadeIn(_alpha, _frame, _blackParen, _token);
        SceneManager.LoadScene("TitleScene");
    }
}
