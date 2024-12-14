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
    [SerializeField] GameObject activeObject;
    [SerializeField] GameObject inactiveObject;
    
    public void DestroyBox()
    {
        if (numberOfCoins <= 0)
            return;
        
        if (ObectToSpawn != null)
        {
            GameObject spawnedObect =Instantiate(ObectToSpawn, transform.position, Quaternion.identity);
            StartCoroutine(MoveSpawnedObjectUp(spawnedObect));
        }
        numberOfCoins--;
        GameManager.Instance.AddScore(coinValue);
        
        if(animation != null)
            animation.Play();

        if (numberOfCoins <= 0)
        {
            OnBoxDestroyed.Invoke();
            foreach (var obj in obectToDestoy)
            {
                obj.SetActive(false);
            }
            activeObject.SetActive(false);
            inactiveObject.SetActive(true);
            
        }
        
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
