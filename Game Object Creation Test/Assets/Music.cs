using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour
{

    public AudioSource audio;
    public AudioClip[] myMusic = new AudioClip[3];
    // Use this for initialization
    void Awake()
    {
        audio.clip = myMusic[0] as AudioClip;
    }
    void Start()
    {
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
            playRandomMusic();
    }
    void playRandomMusic()
    {
        audio.clip = myMusic[Random.Range(0, myMusic.Length)] as AudioClip;
        audio.Play();
    }
}