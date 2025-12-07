using UnityEngine;

public class InitOnHit_SpawnedMeteor : MonoBehaviour
{
    [SerializeField]
    MeteorReleaser _meteorReleaser;

    [SerializeField]
    MeteorPool _meteorPool;

    private void Awake()
    {
        _meteorPool.OnSpawnAwake += SetOnHit;
    }

    void SetOnHit(GameObject gObj)
    {
        var onHit = gObj.GetComponent<SmallMeteorOnHitThunder>();

        if (onHit == null) return;

        onHit.InitAwake(_meteorReleaser);
    }
}
