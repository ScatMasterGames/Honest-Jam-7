using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeChanger : MonoBehaviour
{
    public void VolumeUp()
    {
        if (MusicManager.Instance == null)
        {
            MusicManager.PopulateInstance();
        }
        MusicManager.Instance.IncreasVolume();
    }
    
    public void VolumeDown()
    {
        if (MusicManager.Instance == null)
        {
            MusicManager.PopulateInstance();
        }
        MusicManager.Instance.DecreaseVolume();
    }
}
