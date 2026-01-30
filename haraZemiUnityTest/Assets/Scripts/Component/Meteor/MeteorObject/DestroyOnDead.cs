using UnityEngine;

//HPが尽きた時に自分を破壊する

public class DestroyOnDead : MonoBehaviour
{
    [SerializeField]
    HitPoint _hitPoint;

    MeteorReleaser _meteorReleaser;
    MeteorDestroySE _meteorDestroySE;
    ExplosionPool _explosionPool;

    public void InitAwake(MeteorReleaser meteorReleaser, ExplosionPool explosionPool, MeteorDestroySE meteorDestroySE)//初期化処理
    {
        _meteorReleaser = meteorReleaser;
        _explosionPool = explosionPool;
        _meteorDestroySE = meteorDestroySE;
    }

    private void OnEnable()
    {
        _hitPoint.OnDead += DestroyMe;
    }

    private void OnDisable()
    {
        _hitPoint.OnDead -= DestroyMe;
    }

    void DestroyMe()
    {
        //雷に当たったら即消える
        _meteorReleaser.Release(gameObject);

        //爆発と効果音(エフェクト)
        _explosionPool.Spawn(transform.position);
        _meteorDestroySE.PlayOneShot();
    }
}
