using UnityEngine;

//¬è¦Î‚Ì“d‹C‚É“–‚½‚Á‚½‚Ìˆ—

public class SmallMeteorOnHitThunder : MonoBehaviour,IThunderHittable
{
    MeteorReleaser _meteorReleaser;
    ExplosionPool _explosionPool;

    public void InitAwake(MeteorReleaser meteorReleaser,ExplosionPool explosionPool)
    {
        _meteorReleaser = meteorReleaser;
        _explosionPool = explosionPool;
    }

    public void OnHit(float damage)
    {
        //—‹‚É“–‚½‚Á‚½‚ç‘¦Á‚¦‚é
        _meteorReleaser.Release(gameObject);
        //”š”­‚à‹N‚±‚·
        _explosionPool.Spawn(transform.position);
    }
}
