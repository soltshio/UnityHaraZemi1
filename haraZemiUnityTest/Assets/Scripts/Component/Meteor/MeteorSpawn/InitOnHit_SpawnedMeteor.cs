using UnityEngine;

public class InitOnHit_SpawnedMeteor : MonoBehaviour
{
    [SerializeField]
    MeteorReleaser _meteorReleaser;

    [SerializeField]
    MeteorDestroySE _meteorDestroySE;

    [SerializeField]
    ExplosionPool _explosionPool;

    [SerializeField]
    MeteorPool _meteorPool;

    private void OnEnable()
    {
        _meteorPool.OnSpawnAwake += SetOnHit;
    }

    private void OnDisable()
    {
        _meteorPool.OnSpawnAwake -= SetOnHit;
    }

    void SetOnHit(GameObject gObj)
    {
        var onHit = gObj.GetComponent<SmallMeteorOnHitThunder>();

        if (onHit == null) return;

        onHit.InitAwake(_meteorReleaser,_explosionPool,_meteorDestroySE);
    }
}
