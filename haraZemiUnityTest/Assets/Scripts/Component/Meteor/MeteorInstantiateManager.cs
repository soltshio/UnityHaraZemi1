using UnityEngine;
using UnityEngine.Pool;

//隕石の生成のマネージャー

public class MeteorInstantiateManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private ObjectPool<GameObject> pool;

    private void Awake()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: CreateBullet,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyBullet,
            collectionCheck: false,
            defaultCapacity: 20,
            maxSize: 100
        );
    }

    private GameObject CreateBullet()
    {
        var b = Instantiate(bulletPrefab);
        // ← GameObject側にプールを渡す(後で自分で破棄出来るようにするため)
        return b;
    }

    private void OnGet(GameObject b)
    {
        b.gameObject.SetActive(true);
    }

    private void OnRelease(GameObject b)
    {
        b.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(GameObject b)
    {
        Destroy(b.gameObject);
    }

    public GameObject Get()
    {
        return pool.Get();
    }
}
