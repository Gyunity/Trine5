using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public enum EMainEftType
    {
        MAIN_MANU
    }
    public enum EPlayerEftType
    {
        PLAYER_WALK,
        PLAYER_ROLLING,
        PLAYER_RUN,
        PLAYER_JUMP,
        PLAYER_CHANGE,
        PLAYER_ARCHER_ROPEMOVE,
        PLAYER_ARCHER_DAMAGE,
        PLAYER_ARCHER_ROPEEFT,
        PLAYER_ARCHER_DEAD,
        PLAYER_ARCHER_ARROWHIT,
        PLAYER_ARCHER_ARROWLONGSHOOT,
        PLAYER_ARCHER_ARROWREADY,
        PLAYER_ARCHER_ARROWSHORTSHOOT,
        PLAYER_WIZARD_DAMAGE,
        PLAYER_WIZARD_RECALL,
        PLAYER_WIZARD_WARRIOR_DEAD,
        PLAYER_WARRIOR_THROWSWORD,
        PLAYER_WARRIOR_DASH,
        PLAYER_WARRIOR_HIT,
        PLAYER_WARRIOR_DAMAGE,
        PLAYER_WARRIOR_SLASH

    }
    public enum EStoryEftType
    {
        STORY_MINIBOSS_FIREBALL,
        STORY_MINIBOSS_CANNONATTACK,
        STORY_MUSHROOM_RESPAWN,
        STORY_MUSHROOM_BOOM,
        STORY_DOOR_OPEN,
        STORY_ITEMGET,
        STORY_CABAGGE_BOOM
    }
    public enum EBossEftType
    {
        BOSS_HIT1,
        BOSS_HIT2,
        BOSS_HIT3,
        BOSS_LASERSTART,
        BOSS_LASERING,
        BOSS_CANNON,
        BOSS_TRAPHIT
    }
    public enum EBgmType
    {
        BGM_OPENING,
        BGM_STORY,
        BGM_BOSS
    }

    public static SoundManager instance;

    //audioSource
    public AudioSource eftAudio;
    public AudioSource bgmAudio;

    //effect audio clip을 여러게 담아 놓을 변수
    public AudioClip[] mainEftAudios;
    public AudioClip[] playerEftAudios;
    public AudioClip[] storyEftAudios;
    public AudioClip[] bossEftAudios;
    public AudioClip[] bgmAudios;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //씬이 전환이 돼도 게임오브젝트를 바괴하고 싶지 않다
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void PlayMainEftSound(EMainEftType idx)
    {
        int audioIdx = (int)idx;
        eftAudio.PlayOneShot(mainEftAudios[audioIdx]);
    }
    public void PlayPlayerEftSound(EPlayerEftType idx)
    {
        int audioIdx = (int)idx;
        eftAudio.PlayOneShot(playerEftAudios[audioIdx]);
    }
    public void PlayStoryEftSound(EStoryEftType idx)
    {
        int audioIdx = (int)idx;
        eftAudio.PlayOneShot(storyEftAudios[audioIdx]);
    }
    public void PlayBossEftSound(EBossEftType idx)
    {
        int audioIdx = (int)idx;
        eftAudio.PlayOneShot(bossEftAudios[audioIdx]);
    }
    public void PlayBgmSound(EBgmType idx)
    {
        int audioIdx = (int)idx;

        bgmAudio.clip = bgmAudios[audioIdx];

        bgmAudio.Play();
    }
}
