using UnityEngine;

public class BossMeteorOnHitThunder : MonoBehaviour,IThunderHittable
{
    [SerializeField]
    InvincibleTime _invincibleTime;

    [SerializeField]
    HitPoint _hitPoint;

    public void OnHit(float damage)
    {
        if (_invincibleTime.IsInvincible) return;

        _hitPoint.HP -= damage;
        _invincibleTime.StartInvincible();
    }
}
