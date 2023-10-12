using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Fade
{
    private byte _maxClearValue = 255;
    /// <summary>
    /// Imageのフェードアウト
    /// </summary>
    /// <returns>UniTask</returns>
    public async UniTask FadeOut(int alpha, int frame, Image image, CancellationToken token)
    {
        int a = alpha;
        alpha = 255;
        while (image.color.a > 0)
        {
            //frameぶんフレームを待つ
            await UniTask.DelayFrame(frame, cancellationToken: token);
            Color32 newColor = image.color;
            newColor.a = (byte)alpha;// 透明度をalfaにする
            image.color = newColor;// 代入
            alpha -= a;// alfaの値を減らす
            if (0 >= alpha)// 透明度が0以下になったら
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
    /// Imageのフェードイン
    /// </summary>
    /// <returns>UniTask</returns>
    public async UniTask FadeIn(int alpha, int frame, Image image, CancellationToken token)
    {
        // a ではない名前をつけよう
        int a = alpha;
        while (image.color.a < 1)
        {
            await UniTask.DelayFrame(frame, cancellationToken: token);
            Color32 new_color = image.color;
            new_color.a = (byte)alpha;
            image.color = new_color;
            alpha += a;
            if (255 <= alpha)//255以上になるとClorがリセットされて無限ループを防ぐ
            {
                alpha = 255;
            }
        }
    }
    public async UniTask FadeIn(int alpha, int frame, int finAlpha, Image image, CancellationToken token)
    {//finAlpaの値まで処理をする
        // a ではない名前をつけよう
        int a = alpha;
        while (image.color.a < 1)
        {
            Debug.Log(image.color.a);
            await UniTask.DelayFrame(frame, cancellationToken: token);
            Color32 new_color = image.color;
            new_color.a = (byte)alpha;
            image.color = new_color;
            alpha += a;
            if (finAlpha <= alpha)//255以上になるとClorがリセットされて無限ループを防ぐ
            {
                alpha = finAlpha;
                return;
            }
        }
    }
    /// <summary>
    /// フェードインのオーバーロード　敵の場合のみマテリアルでしか動かなかった　　自分でも少し修正しましたが、友達に直してもらったので内部処理は違います
    /// </summary>
    /// <param name="alpha">透明度を下げる値</param>
    /// <param name="ms">何ミリ秒で</param>
    /// <param name="spriteRenderer">このスプライトを</param>
    /// <param name="token">キャンセル用のトークン</param>
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
