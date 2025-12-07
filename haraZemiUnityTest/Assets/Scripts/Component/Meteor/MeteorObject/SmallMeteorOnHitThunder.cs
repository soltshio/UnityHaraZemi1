using UnityEngine;

//¬è¦Î‚Ì“d‹C‚É“–‚½‚Á‚½‚Ìˆ—

public class SmallMeteorOnHitThunder : MonoBehaviour,IThunderHittable
{
    MeteorReleaser _meteorReleaser;

    public void InitAwake(MeteorReleaser meteorReleaser)
    {
        _meteorReleaser = meteorReleaser;
    }

    public void OnHit()
    {
        //—‹‚É“–‚½‚Á‚½‚ç‘¦Á‚¦‚é
        _meteorReleaser.Release(gameObject);
    }
}
