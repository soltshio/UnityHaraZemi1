using UnityEngine;

//小隕石の電気に当たった時の処理

public class SmallMeteorOnHitThunder : MonoBehaviour,IThunderHittable
{
    MeteorReleaser _meteorReleaser;
    MeteorDestroySE _meteorDestroySE;
    ExplosionPool _explosionPool;

    public void InitAwake(MeteorReleaser meteorReleaser,ExplosionPool explosionPool,MeteorDestroySE meteorDestroySE)
    {
        _meteorReleaser = meteorReleaser;
        _explosionPool = explosionPool;
        _meteorDestroySE = meteorDestroySE;
    }

    public void OnHit(float damage)
    {
        //雷に当たったら即消える
        _meteorReleaser.Release(gameObject);
        //爆発と効果音(エフェクト)
        _explosionPool.Spawn(transform.position);
        _meteorDestroySE.PlayOneShot();
    }
}
