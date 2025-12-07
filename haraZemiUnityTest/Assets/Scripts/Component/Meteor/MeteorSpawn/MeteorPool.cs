using UnityEngine;
using UnityEngine.Pool;
using System;

//隕石の生成のマネージャー

public class MeteorPool : MonoBehaviour
{
    [SerializeField] GameObject _meteorPrefab;

    [SerializeField]
    int _maxCount;

    private ObjectPool<GameObject> _pool;
    public event Action<GameObject> OnSpawn;

    public int ActiveCount { get { return _pool.CountActive; } }//アクティブ状態になっている隕石の数を取得

    public GameObject Get()//生成
    {
        return _pool.Get();
    }

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: OnCreate,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyBullet,
            collectionCheck: false,
            defaultCapacity: 20,
            maxSize: _maxCount
        );
    }

    private GameObject OnCreate()
    {
        var g = Instantiate(_meteorPrefab);
        g.SetActive(false);
        // ← GameObject側にプールを渡す(後で自分で破棄出来るようにするため)
        return g;
    }

    private void OnGet(GameObject g)
    {
        OnSpawn?.Invoke(g);//初期化処理

        g.gameObject.SetActive(true);
    }

    private void OnRelease(GameObject g)
    {
        g.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(GameObject g)
    {
        Destroy(g.gameObject);
    }
}
