using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer main;

    public AudioSource audio;

    private void Awake()
    {
        main = this;
        main.audio.Play();
    }

    // Update is called once per frame
    public static void Play(AudioClip clip)
    {
        if (main == null || main.audio == null || clip == null)
            return;

        Debug.Log(clip);
        main.audio.PlayOneShot(clip);
    }
}
