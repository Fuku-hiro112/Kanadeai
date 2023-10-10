using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] private int _letterPerSecond; // 1�����ӂ�̎���
    [SerializeField] private Text _dialogText;

    private CancellationToken _token;
    private void Start()
    {
        // this�t���Ȃ��ƃG���[���o�܂�
        _token = this.GetCancellationTokenOnDestroy();
    }

    /// <summary>
    /// �^�C�v�`���i�P�����Âj�ŕ�����\������
    /// </summary>
    public async UniTask TypeDialogAsync(string dialog, bool isClick = false)
    {
        _dialogText.text = "";
        foreach (char letter in dialog)
        {
            //�I����SE
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
            //�I����SE
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
