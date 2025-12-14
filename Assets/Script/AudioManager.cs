using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource audioSource;

    public AudioClip loseClip;
    public AudioClip collideClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoseSound()
    {
        if (loseClip != null)
        {
            audioSource.clip = loseClip;
            audioSource.Play();
        }
    }
    public void CollideSound()
    {
        if (collideClip != null)
        {
            audioSource.clip = collideClip;
            audioSource.Play();
        }
    }
}
