using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public Player player;
    public LevelController levelController;

    public AudioClip jumpClip;
    public AudioClip runningClip;
    public AudioClip collectDiamonClip;
    public AudioClip winClip;
    public AudioClip gameOverClip;
    public AudioClip levelMusicClip;
    public AudioClip mainMenuClip;

    private AudioSource jumpSource;
    private AudioSource runningSource;
    private AudioSource collectDiamondSource;
    private AudioSource winSource;
    private AudioSource gameOverSource;
    private AudioSource levelMusicSource;
    private AudioSource mainMenuSource;

    void Start()
    {
        player = levelController.player;
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && player.grounded || Input.GetButtonDown("Jump") && player.wallJump)
        {
            jumpSource.Play();
        }
    }
    void AddAudio()
    {

    }
 //   function AddAudio(clip:AudioClip, loop: boolean, playAwake: boolean, vol: float): AudioSource {
 //  var newAudio = gameObject.AddComponent(AudioSource);
 //   newAudio.clip = clip;
 //  newAudio.loop = loop;
 //  newAudio.playOnAwake = playAwake;
 //  newAudio.volume = vol;
 //  return newAudio;
 //}
}
