using UnityEngine;

public class MusicSource : MonoBehaviour
{
    [SerializeField] AudioClip musicClip;
    void Start()
    {
        MusicManager.Instance.PlaySound(musicClip);
    }
}
