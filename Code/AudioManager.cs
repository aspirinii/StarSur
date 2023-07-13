using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClips;
    public float bgmVolume;
    AudioSource bgmPlayer;
    AudioHighPassFilter bgmHighPassFilter;
    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx { Dead, Hit, LevelUp=3, Lose, Melee, Range=7, Select, Win}

    private void Awake()
    {
        instance = this;
        Init();

    }

    void Init()
    {
        // 배경음 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        // bgmObjcet.transform.SetParent(transform);
        bgmObject.transform.parent = transform; // 현재객체(AudioManager) 의 자식으로 BgmObject를 넣는다.
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.clip = bgmClips;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.loop = true;
        // bgmHighPassFilter = bgmObject.AddComponent<AudioHighPassFilter>();  메인카메라에 있기때문에

        bgmHighPassFilter = Camera.main.GetComponent<AudioHighPassFilter>(); // 리스너 이펙트로 listener effect 카메라가 있는곳에만 생성된다. 카메라에서 생성하여 오디오 매니져로 가져오는 방식을 사용 


        //효과음 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        // bgmObjcet.transform.SetParent(transform);
        sfxObject.transform.parent = transform; // 현재객체(AudioManager) 의 자식으로 BgmObject를 넣는다.
        sfxPlayers = new AudioSource[channels];

        for (int index = 0 ; index < sfxPlayers.Length; index++){
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
            sfxPlayers[index].bypassListenerEffects = true;
        }

    }
    public void PlayBgm(bool isPlay)
    {
        if(isPlay){
            bgmPlayer.Play();
        }else{
            bgmPlayer.Stop();
        }
    }

    public void EffectBgm(bool isEffect)
    {
        bgmHighPassFilter.enabled = isEffect;
        // if(isEffect){
        //     bgmHighPassFilter.cutoffFrequency = 1000;
        // }else{
        //     bgmHighPassFilter.cutoffFrequency = 10;
        // }
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int index=0; index < sfxPlayers.Length; index++){ int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            int ranIndex = 0;
            if (sfx == Sfx.Hit || sfx == Sfx.Melee){
                ranIndex = Random.Range(0, 2);
            }
            if(!sfxPlayers[loopIndex].isPlaying){
                sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + ranIndex];
                sfxPlayers[loopIndex].Play();
                break;
            }
        }
    }
}
