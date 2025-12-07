using UnityEngine;

public class BossMeteorOnHitThunder : MonoBehaviour,IThunderHittable
{
    [SerializeField]
    float _damage;

    [SerializeField]
    InvincibleTime _invincibleTime;

    [SerializeField]
    HitPoint _hitPoint;

    public void OnHit()
    {
        if (_invincibleTime.IsInvincible) return;

        _hitPoint.HP -= _damage;
        _invincibleTime.StartInvincible();
    }
}
