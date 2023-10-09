using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private float _fadeDuration;
    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource seAudioSource;

    [SerializeField] List<BGMSoundData> bgmSoundDatas;
    [SerializeField] List<SESoundData> seSoundDatas;

    [SerializeField, Range(0, 1)] private float masterVolume = 1;
    [SerializeField, Range(0, 1)] private float bgmMasterVolume = 1;
    [SerializeField, Range(0, 1)] private float seMasterVolume = 1;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(BGMSoundData.BGM bgm, bool isFade = false)
    {
        BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
        bgmAudioSource.clip = data.audioClip;
        bgmAudioSource.volume = data.volume * bgmMasterVolume * masterVolume;
        if (isFade)
        {
            float originalVolume = bgmAudioSource.volume;
            bgmAudioSource.volume = 0;
            bgmAudioSource.Play();
            bgmAudioSource.DOFade(originalVolume, _fadeDuration);
        }
        else
        {
            bgmAudioSource.Play();
        }
    }
    public void StopBGM(bool isFadeOut = false)
    {
        if (isFadeOut)
        {
            bgmAudioSource.DOFade(0, _fadeDuration);
        }
        else
        {
            bgmAudioSource.Stop();
        }
    }


    public void PlaySE(SESoundData.SE se, bool isFade = false)
    {
        SESoundData data = seSoundDatas.Find(data => data.se == se);
        seAudioSource.volume = data.volume * seMasterVolume * masterVolume;
        if(isFade)
        {
            float originalVolume = seAudioSource.volume;
            seAudioSource.volume = 0;// フェードインの為一旦０に
            seAudioSource.PlayOneShot(data.audioClip);
            seAudioSource.DOFade(originalVolume, _fadeDuration);
        }
        else
        {
            seAudioSource.PlayOneShot(data.audioClip);
        }
        
    }
    public void StopSE(bool isFadeOut = false)
    {
        if (isFadeOut)
        {
            seAudioSource.DOFade(0, _fadeDuration);
        }
        else
        {
            seAudioSource.Stop();
        }
    }

}
[Serializable]
public class BGMSoundData
{
    public enum BGM
    {
        Title,
        Play,
         // これがラベルになる
    }

    public BGM bgm;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}

[Serializable]
public class SESoundData
{
    public enum SE
    {
        GetItem,
        Click,
        OpenDoor,
        CloseDoor,
        StairsMove,
        Death
    }

    public SE se;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}