using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestructableBox : MonoBehaviour,IHitable
{
    public UnityEvent OnBoxDestroyed;
    [SerializeField] private int numberOfCoins = 1;
    [SerializeField] private int coinValue = 10;
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
        while (time < 0.5f)
        {
            spawnedObect.transform.position += Vector3.up * (Time.deltaTime * 20);
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(spawnedObect);
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
