using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] private int _letterPerSecond; // 1文字辺りの時間
    [SerializeField] private Text _dialogText;

    private CancellationToken _token;
    private void Start()
    {
        // this付けないとエラーが出ます
        _token = this.GetCancellationTokenOnDestroy();
    }

    /// <summary>
    /// タイプ形式（１文字づつ）で文字を表示する
    /// </summary>
    public async UniTask TypeDialogAsync(string dialog, bool isClick = false)
    {
        _dialogText.text = "";
        foreach (char letter in dialog)
        {
            //選択音SE
            _dialogText.text += letter;
            await UniTask.Delay(TimeSpan.FromSeconds(1f / _letterPerSecond), cancellationToken: _token);
        }
        if (isClick)
        {
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _token);
            TextInvisible();
        }
    }
    public async UniTask TypeDialogAsync(Text text,string dialog, bool isClick = false)
    {
        text.text = "";
        foreach (char letter in dialog)
        {
            //選択音SE
           text.text += letter;
            await UniTask.Delay(TimeSpan.FromSeconds(1f / _letterPerSecond), cancellationToken: _token);
        }
        if (isClick)
        {
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _token);
            TextInvisible();
        }
    }

    public void TextInvisible()
    {
        _dialogText.text = "";
    }
}
