using UnityEngine;

//¬è¦Î‚Ì“d‹C‚É“–‚½‚Á‚½‚Ìˆ—

public class SmallMeteorOnHitThunder : MonoBehaviour,IThunderHittable
{
    [SerializeField]
    HitPoint _hitPoint;

    public void OnHit(float damage)
    {
        _hitPoint.HP -= damage;
    }
}
