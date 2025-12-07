using UnityEngine;

//隕石への初期化

public class InitPos_SpawnedMeteor : MonoBehaviour
{
    [Tooltip("隕石のオブジェクトプール")] [SerializeField]
    MeteorPool _meteorPool;

    [Tooltip("隕石のスポーン場所を決める機能")] [SerializeField]
    RandomPositionsInBoxes _getSpawnPosition;

    private void Awake()
    {
        _meteorPool.OnSpawn += SetSpawnedMeteorPos;
    }

    void SetSpawnedMeteorPos(GameObject gobj)
    {
        Vector3 spawnPos = _getSpawnPosition.GetRandomPos();

        gobj.transform.position = spawnPos;
    }
}
