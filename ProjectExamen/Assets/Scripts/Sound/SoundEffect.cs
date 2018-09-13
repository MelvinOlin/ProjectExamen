using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    private Player player;

    public AudioClip musicClip;
    public AudioClip lastLevelMusicCLip;

    private AudioSource musicSource;
    private AudioSource lastLevelMusicSource;


    private void Awake()
    {
        musicSource = AddAudio(musicClip, true, true, 0.4f);
        lastLevelMusicSource = AddAudio(lastLevelMusicCLip, true, true, 0.4f);
    }
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {

    }

    AudioSource AddAudio(AudioClip audioClip, bool loop, bool playAwake, float vol)
    {
        var newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = audioClip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        return newAudio;
    }
    public void PlayLevelMusic()
    {
        musicSource.Play();
    }
    
}
