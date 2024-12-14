using UnityEngine;

public class MusicSource : MonoBehaviour
{
    [SerializeField] AudioClip musicClip;
    [SerializeField] bool playOnStart = true;
    void Start()
    {
       if(playOnStart)
           PlayMusic();
    }
    
    public void PlayMusic()
    {
        if (MusicManager.Instance == null)
        {
            MusicManager.PopulateInstance();
        }
        MusicManager.Instance.PlaySound(musicClip);
    }
}