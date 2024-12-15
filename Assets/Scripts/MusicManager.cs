using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSource2;

    AudioSource currentAudioSource;
    [SerializeField] AudioMixerGroup _mixerGroup;
    public static MusicManager Instance;
    private float currentClipLength;
    private float changeSongTime;
    [SerializeField] float volume = 0.2f;
    private float _volume = 0.5f;

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
        _mixerGroup.audioMixer.SetFloat("Volume", _volume);
    }

    public void PlaySound(AudioClip clip)
    {
        //Fade out current audio source and fade in the new audio clip at the same time
        audioSource2.clip = clip;
        audioSource2.Play();
        currentClipLength = audioSource2.clip.length;
        changeSongTime = currentClipLength + Time.time-2f;
        StartCoroutine(FadeOut(currentAudioSource));
    }

    private void Update()
    {
        if(Time.time > changeSongTime)
        {
            PlaySound(currentAudioSource.clip);
        }
    }

    private IEnumerator FadeOut(AudioSource currentAudioSource1)
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            currentAudioSource1.volume = volume - ( time * volume);
            audioSource2.volume = time * volume;
            yield return null;
        }
        audioSource2.volume = volume;
        currentAudioSource1.volume = 0;

        currentAudioSource1.Stop();
        (currentAudioSource, audioSource2) = (audioSource2, currentAudioSource);
    }
    
    

    public static void PopulateInstance()
    {
        if(Instance != null)
            return;
        MusicManager newMusicManager = new GameObject("MusicManager").AddComponent<MusicManager>();
        var source1 = newMusicManager.AddComponent<AudioSource>();
        var source2 = newMusicManager.AddComponent<AudioSource>();
        newMusicManager.audioSource = source1;
        newMusicManager.audioSource2 = source2;
        newMusicManager.currentAudioSource = source1;
        
        newMusicManager.audioSource.clip = Instance.audioSource.clip;
    }

    public void IncreasVolume()
    {
        _volume += 1f;
        _mixerGroup.audioMixer.SetFloat("Volume", _volume);
    }
    public void DecreaseVolume()
    {
        _volume -= 1f;
        _mixerGroup.audioMixer.SetFloat("Volume", _volume);
    }
}