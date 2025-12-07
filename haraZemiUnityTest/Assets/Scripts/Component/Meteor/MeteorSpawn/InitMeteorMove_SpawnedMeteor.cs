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
        _meteorPool.OnSpawn += SetMeteorMove;
    }

    void SetMeteorMove(GameObject gobj)
    {
        var meteorMove = gobj.GetComponent<MeteorMove>();

        if (meteorMove == null) return;

        float speed = Random.Range(_minSpeed, _maxSpeed);

        meteorMove.Init(speed, _target);
    }
}
