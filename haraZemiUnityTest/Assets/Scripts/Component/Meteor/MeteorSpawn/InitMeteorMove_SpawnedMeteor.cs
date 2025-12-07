using UnityEngine;

//隕石の動きの初期化

public class InitMeteorMove_SpawnedMeteor : MonoBehaviour
{
    [Tooltip("隕石のオブジェクトプール")] [SerializeField]
    MeteorPool _meteorPool;

    [SerializeField]
    float _maxSpeed;

    [SerializeField]
    float _minSpeed;

    [SerializeField]
    Transform _target;

    private void Awake()
    {
        _meteorPool.OnSpawnEnable += SetMeteorMove;
    }

    void SetMeteorMove(GameObject gObj)
    {
        var meteorMove = gObj.GetComponent<MeteorMove>();

        if (meteorMove == null) return;

        float speed = Random.Range(_minSpeed, _maxSpeed);

        meteorMove.InitOnEnable(speed, _target);
    }
}
