using UnityEngine;

public class HideOnPlayerEnter : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent(out PlayerMovement _))
        {
            spriteRenderer.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent(out PlayerMovement _))
        {
            spriteRenderer.enabled = true;
        }
    }
}
