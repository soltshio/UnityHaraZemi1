using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

//隕石を一定間隔でスポーンさせる機能

public class MeteorSpawner : MonoBehaviour
{
    [Tooltip("隕石のオブジェクトプール")] [SerializeField]
    MeteorPool _meteorPool;

    [Tooltip("隕石を生成するインターバル")] [SerializeField]
    float _spawnInterval;

    [Tooltip("一度のスポーンでどれくらいの量の隕石を生成するか")] [SerializeField]
    int _oneSpawnCount;

    [Tooltip("最初にスポーンしておく隕石の数")] [SerializeField]
    int _startSpawnCount;

    [Tooltip("最大で隕石がスポーンする数\nプールの最大量を超えないように設定してください")] [SerializeField]
    int _maxSpawnCount;

    float _current=0;

    void Start()
    {
        Spawn(_startSpawnCount);//最初に一気に生成しておく
    }

    void Update()
    {
        _current += Time.deltaTime;

        if (_current < _spawnInterval) return;

        _current = 0;
        //生成
        Spawn(_oneSpawnCount);

    }

    void Spawn(int spawnCount)
    {
        //生成量が限界値を超えないようにする
        int remainingSpawnableCount = _maxSpawnCount - _meteorPool.ActiveCount;
        spawnCount = Mathf.Clamp(spawnCount, 0, remainingSpawnableCount);

        //生成
        for (int i=0; i<spawnCount ;i++)
        {
            var meteor = _meteorPool.Get();
        }
    }
}
