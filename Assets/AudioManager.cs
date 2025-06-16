using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip clip; // Assign your audio clip in the inspector
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound(string soundName)
    {
        clip = Resources.Load<AudioClip>(soundName);
        if (clip != null)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.Play();
            Destroy(audioSource, clip.length); // Remove the AudioSource after the clip finishes playing
        }
    }
}
