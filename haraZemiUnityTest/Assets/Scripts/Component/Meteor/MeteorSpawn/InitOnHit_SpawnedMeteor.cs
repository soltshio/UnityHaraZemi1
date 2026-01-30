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
        _meteorPool.OnSpawnAwake += SetDestroyOnDead;
    }

    private void OnDisable()
    {
        _meteorPool.OnSpawnAwake -= SetDestroyOnDead;
    }

    void SetDestroyOnDead(GameObject gObj)
    {
        var destroyOnDead = gObj.GetComponent<DestroyOnDead>();

        if (destroyOnDead == null) return;

        destroyOnDead.InitAwake(_meteorReleaser,_explosionPool,_meteorDestroySE);
    }
}
