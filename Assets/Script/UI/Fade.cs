using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Fade
{
    private byte _maxClearValue = 255;
    /// <summary>
    /// Image�̃t�F�[�h�A�E�g
    /// </summary>
    /// <returns>UniTask</returns>
    public async UniTask FadeOut(int alpha, int frame, Image image, CancellationToken token)
    {
        int a = alpha;
        alpha = 255;
        while (image.color.a > 0)
        {
            //frame�Ԃ�t���[����҂�
            await UniTask.DelayFrame(frame, cancellationToken: token);
            Color32 newColor = image.color;
            newColor.a = (byte)alpha;// �����x��alfa�ɂ���
            image.color = newColor;// ���
            alpha -= a;// alfa�̒l�����炷
            if (0 >= alpha)// �����x��0�ȉ��ɂȂ�����
            {
                alpha = 0;
            }
        }
    }
    public async UniTask FadeOut(byte alpha, int ms, SpriteRenderer spriteRenderer, CancellationToken token)
    {
        // alpha
        byte a = _maxClearValue;
        for (int i = alpha; i < _maxClearValue; i+=alpha)
        {
            await UniTask.DelayFrame(ms, cancellationToken: token);
            a -= alpha;
            //Debug.Log(a);
            spriteRenderer.material.color = new Color32(255, 255, 255, a);
        }
        spriteRenderer.material.color = new Color32(255, 255, 255, 0);
    }
    /// <summary>
    /// Image�̃t�F�[�h�C��
    /// </summary>
    /// <returns>UniTask</returns>
    public async UniTask FadeIn(int alpha, int frame, Image image, CancellationToken token)
    {
        // a �ł͂Ȃ����O�����悤
        int a = alpha;
        while (image.color.a < 1)
        {
            await UniTask.DelayFrame(frame, cancellationToken: token);
            Color32 new_color = image.color;
            new_color.a = (byte)alpha;
            image.color = new_color;
            alpha += a;
            if (255 <= alpha)//255�ȏ�ɂȂ��Clor�����Z�b�g����Ė������[�v��h��
            {
                alpha = 255;
            }
        }
    }
    public async UniTask FadeIn(int alpha, int frame, int finAlpha, Image image, CancellationToken token)
    {//finAlpa�̒l�܂ŏ���������
        // a �ł͂Ȃ����O�����悤
        int a = alpha;
        while (image.color.a < 1)
        {
            Debug.Log(image.color.a);
            await UniTask.DelayFrame(frame, cancellationToken: token);
            Color32 new_color = image.color;
            new_color.a = (byte)alpha;
            image.color = new_color;
            alpha += a;
            if (finAlpha <= alpha)//255�ȏ�ɂȂ��Clor�����Z�b�g����Ė������[�v��h��
            {
                alpha = finAlpha;
                return;
            }
        }
    }
    /// <summary>
    /// �t�F�[�h�C���̃I�[�o�[���[�h�@�G�̏ꍇ�̂݃}�e���A���ł��������Ȃ������@�@�����ł������C�����܂������A�F�B�ɒ����Ă�������̂œ��������͈Ⴂ�܂�
    /// </summary>
    /// <param name="alpha">�����x��������l</param>
    /// <param name="ms">���~���b��</param>
    /// <param name="spriteRenderer">���̃X�v���C�g��</param>
    /// <param name="token">�L�����Z���p�̃g�[�N��</param>
    /// <returns></returns>
    public async UniTask FadeIn(byte alpha, int ms, SpriteRenderer spriteRenderer, CancellationToken token)//Material
    {
        byte a = _maxClearValue;
        for (int i = alpha; i < _maxClearValue; i += alpha)
        {
            await UniTask.DelayFrame(ms, cancellationToken: token);
            a += alpha;
            spriteRenderer.material.color = new Color32(255, 255, 255, a);
        }
        spriteRenderer.material.color = new Color32(255, 255, 255, 255);
    }
}
