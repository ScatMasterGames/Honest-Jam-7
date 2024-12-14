using System;
using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSource2;

    AudioSource currentAudioSource;

    public static MusicManager Instance;

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


        currentAudioSource = audioSource;
    }

    public void PlaySound(AudioClip clip)
    {
        //Fade out current audio source and fade in the new audio clip at the same time
        audioSource2.clip = clip;
        audioSource2.Play();
        StartCoroutine(FadeOut(currentAudioSource));
    }

    private IEnumerator FadeOut(AudioSource currentAudioSource1)
    {
        while (currentAudioSource1.volume > 0)
        {
            currentAudioSource1.volume -= Time.deltaTime;
            audioSource2.volume += Time.deltaTime;
            yield return null;
        }

        currentAudioSource1.Stop();
        var temp = currentAudioSource;
        currentAudioSource = audioSource2;
        audioSource2 = temp;
    }
}