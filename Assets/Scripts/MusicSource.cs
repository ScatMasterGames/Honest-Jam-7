using UnityEngine;

public class MusicSource : MonoBehaviour
{
    [SerializeField] AudioClip musicClip;
    void Start()
    {
        if (MusicManager.Instance == null)
        {
            MusicManager.PopulateInstance();
        }
        MusicManager.Instance.PlaySound(musicClip);
    }
}
