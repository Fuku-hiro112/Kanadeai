using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// �Q�[���I�[�o�[���̑���
public class GameOverController : MonoBehaviour
{
    //�R�����g����
    [SerializeField] private RawImage _gameOverVideo;
    [SerializeField] private Text _returnTitleText;
    [SerializeField] private Image _blackParen; 
    [SerializeField] private int _alpha = 10;
    [SerializeField] private int _frame = 2;
    [SerializeField] private float _imageWaitTime = 3f;//�摜���ς��n�߂�܂ł̎���
    [SerializeField] private float _textWaitTime = 3f;//�e�L�X�g���o�Ă���܂ł̎���
    
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
        //�����StActive��؂��Ă�
        _gameOverVideo.gameObject.SetActive(false);

        //fadeOut����
        await _fade.FadeOut(_alpha, _frame, _blackParen, _token);
        await UniTask.Delay(TimeSpan.FromSeconds(_textWaitTime), cancellationToken: _token);

        //�Q�[���I�[�o�[�̉摜���o��
        _returnTitleText.gameObject.SetActive(true);
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _token);

        //�V�[����؂�ւ���O��fadeIn����
        await _fade.FadeIn(_alpha, _frame, _blackParen, _token);
        SceneManager.LoadScene("TitleScene");
    }
}
