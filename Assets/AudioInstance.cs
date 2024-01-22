using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInstance : MonoBehaviour
{
    private AudioSource audioSource;
    public static AudioInstance AudioInstance1;
    public AudioClip enemyfire; 
    public AudioClip enemyhit;
    public AudioClip explosion;
    public AudioClip fire;
    public AudioClip hit;
    public AudioClip death;
    public AudioClip jump;
    public AudioClip land;
    public AudioClip walk;
    void Awake()
    {
            AudioInstance1 = this;
    }

        void Start()
        {
            // Get the AudioSource component attached to the GameObject
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            // You can keep the existing commands or modify them as needed.
            // For simplicity, I'll focus on the method for playing specific audio clips.
        }

        // Play a specific audio clip
        public void PlaySpecificAudioClip(AudioClip clip)
        {
            // Check if audio is not already playing
            if (!audioSource.isPlaying)
            {
                // Set the AudioClip and play it
                audioSource.clip = clip;
                audioSource.Play();
                Debug.Log("Playing specific audio clip: " + clip.name);
            }
            else 
            {
                audioSource.Stop();
            // Set the AudioClip and play it
                audioSource.clip = clip;
                audioSource.Play();
                Debug.Log("Playing specific audio clip: " + clip.name);
                Debug.Log("Audio is already playing. Cannot play specific clip.");
            }
        }
    
}
