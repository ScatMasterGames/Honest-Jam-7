using UnityEngine;

internal interface IHitable
{
    public Vector2 GetKnockbackForce();
    public void Hit();
}