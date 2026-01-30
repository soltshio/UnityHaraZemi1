using UnityEngine;

public class MeteorReleaser : MonoBehaviour
{
    [Tooltip("隕石のオブジェクトプール")] [SerializeField]
    MeteorPool _meteorPool;

    public void Release(GameObject element)
    {
        _meteorPool.Release(element);
    }
}
