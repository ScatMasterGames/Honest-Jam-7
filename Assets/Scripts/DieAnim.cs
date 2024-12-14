using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DieAnim : MonoBehaviour
{
    [SerializeField] AnimationCurve movementCurve;
    [SerializeField] float duration = 1f;
    [SerializeField] float scale = 1f;
    Vector2 startPos;
    
    public void PlayAnimation()
    {
        var camera = FindObjectOfType<CinemachineVirtualCamera>();
        //Set the cinemachine camera to follow a new static transfrom to prevent the camera from following the player
        camera.Follow = null;
        startPos = transform.position;
        StartCoroutine(PlayDieAnim());
    }

    private IEnumerator PlayDieAnim()
    {
        float time = 0;
        while (time < duration)
        {
            transform.position = startPos + (Vector2.up * movementCurve.Evaluate(time / duration)*scale);
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
