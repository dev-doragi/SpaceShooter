using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("#SFX_1")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    // 기본 볼륨이 상이한 관계로 비슷한 크기의 볼륨을 가진 오디오끼리 따로 플레이하기 위해 sfxPlayer를 두개 만들었음.
    [Header("#SFX_2")]
    public AudioClip[] sfxClips_2;
    public float sfxVolume_2;
    AudioSource[] sfxPlayers_2;
    int channelIndex_2;

    public enum Sfx_1 { PlayerLaser, EnemyLaser, BossLaser, LaserImpact, Explosion, Warning, GameOver }
    public enum Sfx_2 { PowerUp, LifeUp, BossExplosion, ShieldBreak }

    private void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];
        sfxPlayers_2 = new AudioSource[channels];

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
        }

        for (int i = 0; i < sfxPlayers_2.Length; i++)
        {
            sfxPlayers_2[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers_2[i].playOnAwake = false;
            sfxPlayers_2[i].volume = sfxVolume_2;
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void PlaySfx(Sfx_1 sfx)
    {
        for (int i=0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    public void PlaySfx(Sfx_2 sfx)
    {
        for (int i = 0; i < sfxPlayers_2.Length; i++)
        {
            int loopIndex = (i + channelIndex_2) % sfxPlayers_2.Length;

            if (sfxPlayers_2[loopIndex].isPlaying)
                continue;

            channelIndex_2 = loopIndex;
            sfxPlayers_2[loopIndex].clip = sfxClips_2[(int)sfx];
            sfxPlayers_2[loopIndex].Play();
            break;
        }
    }

    public void StopSfx()
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i].mute = true;
        }

        for (int i = 0; i < sfxPlayers_2.Length; i++)
        {
            sfxPlayers_2[i].mute = true;
        }
    }
}
