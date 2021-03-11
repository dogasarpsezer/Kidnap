using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager current;

    private void Start()
    {
        if (current != null) Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            current = this;
            SetWhichToPlay(false, 0);
        }
    }

    public static int whichToPlay = -1;

    public void VolumeSet()
    {
        if(!playingA)
            a.volume = MenuManager.musicVol;
        else
            b.volume = MenuManager.musicVol;
    }

    public GameObject AudiPrefab;
    public AudioClip[] sounds;
    public GameObject AudioParent, MusicParent;

    public AudioClip[] musics;
    public static int musicChange = -1;
    public AudioSource a, b;
    public bool playingA = false;

    public int fadeFixedUpdates = 25;

    public static void SetWhichToPlay(bool sound,int set)//sound or music
    {
        if (sound)
        {
            current.OpenSound(set);
        }
        else
        {
            current.SetMusicChange(set);
        }
    }

    public static void SetSoundToPlay(AudioClip clip)//sound or music
    {
        current.OpenSound(clip);
    }

    public void OpenSound(int set)//sound
    {
        whichToPlay = set;
        a = Instantiate(AudiPrefab, AudioParent.transform).GetComponent<AudioSource>();
        a.clip = sounds[whichToPlay];
        a.Play();
    }

    public void OpenSound(AudioClip set)//sound by other things like dialogues
    {
        a = Instantiate(AudiPrefab, AudioParent.transform).GetComponent<AudioSource>();
        a.clip = set;
        a.volume = MenuManager.soundVol;
        a.Play();
    }

    public void SetMusicChange(int set)//music
    {
        musicChange = set;
        StartCoroutine(FadeChangeMusic());
    }

    IEnumerator FadeChangeMusic()
    {   
        float c = 0, d = 0, mmSv = MenuManager.musicVol;
        if (playingA)
        {
            c = 1;
            a.volume = mmSv;
            b.volume = 0;
            b.clip = musics[musicChange];
            b.Play();
            for (int i = 0; i < fadeFixedUpdates; i++)
            {
                c -= 1f / ((float)fadeFixedUpdates);
                d += 1f / ((float)fadeFixedUpdates);
                a.volume = c * mmSv;
                b.volume = d * mmSv;
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            d = 1;
            b.volume = mmSv;
            a.volume = 0;
            a.clip = musics[musicChange];
            a.Play();
            for (int i = 0; i < fadeFixedUpdates; i++)
            {
                c += 1f / ((float)fadeFixedUpdates);
                d -= 1f / ((float)fadeFixedUpdates);
                a.volume = c * mmSv;
                b.volume = d * mmSv;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
