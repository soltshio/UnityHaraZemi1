using UnityEngine;
using UnityEngine.Pool;
using System;

//隕石の生成のマネージャー

public class MeteorPool : MonoBehaviour
{
    [SerializeField] GameObject _meteorPrefab;

    [SerializeField]
    int _maxCount=200;

    [SerializeField]
    int _defaultCapacity = 60;

    private ObjectPool<GameObject> _pool;
    public event Action<GameObject> OnSpawnAwake;//最初のInstantiateで生成される時のみ(1度だけ)呼び出される処理
    public event Action<GameObject> OnSpawnEnable;//生成される度に呼び出される処理

    public int ActiveCount { get { return _pool.CountActive; } }//アクティブ状態になっている隕石の数を取得

    public GameObject Get()//生成
    {
        return _pool.Get();
    }

    public void Release(GameObject element)
    {
        _pool.Release(element);
    }

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: OnCreate,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyBullet,
            collectionCheck: false,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxCount
        );
    }

    private GameObject OnCreate()
    {
        var g = Instantiate(_meteorPrefab);
        g.SetActive(false);
        OnSpawnAwake?.Invoke(g);//1度しか呼ばれない初期化処理
        return g;
    }

    private void OnGet(GameObject g)
    {
        OnSpawnEnable?.Invoke(g);//生成される度に行う初期化処理

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
