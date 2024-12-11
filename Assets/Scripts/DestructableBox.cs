using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestructableBox : MonoBehaviour,IHitable
{
    public UnityEvent OnBoxDestroyed;
    [SerializeField] protected List<GameObject> obectToDestoy;
    [SerializeField] protected GameObject ObectToSpawn;
    [SerializeField] private Animation animation;
    [SerializeField] Vector2 knockbackForce = new Vector2(0,-50);
    
    public void DestroyBox()
    {
        OnBoxDestroyed.Invoke();
        foreach (var obj in obectToDestoy)
        {
            obj.SetActive(false);
        }
        if (ObectToSpawn != null)
        {
            GameObject spawnedObect =Instantiate(ObectToSpawn, transform.position, Quaternion.identity);
            StartCoroutine(MoveSpawnedObjectUp(spawnedObect));
        }
        if(animation != null)
            animation.Play();
        
    }
    IEnumerator MoveSpawnedObjectUp(GameObject spawnedObect)
    {
        float time = 0;
        while (time < 1)
        {
            spawnedObect.transform.position += Vector3.up * Time.deltaTime;
            time += Time.deltaTime;
            yield return null;
        }
    }

    public Vector2 GetKnockbackForce()
    {
        return knockbackForce;
    }

    public void Hit()
    {
        DestroyBox();
    }
}
