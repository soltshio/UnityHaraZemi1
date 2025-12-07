using System.Collections;
using UnityEngine;

//隕石の動き

public class MeteorMove : MonoBehaviour
{
    [SerializeField]
    Rigidbody _rb;

    [SerializeField]
    float _speed = 5f;

    [SerializeField]
    Transform _target;

    public void Init(float speed,Transform target)
    {
        _speed = speed;
        _target = target;
    }

    private void OnEnable()
    {
        SetLook();
    }

    void Update()
    {
        //移動
        Vector3 newPosition = transform.position + (transform.forward * _speed * Time.deltaTime);
        _rb.MovePosition(newPosition);
    }

    void SetLook()
    {
        if (_target == null) return;

        //向きを決める(ターゲットの方向に向くようにする)
        Vector3 toTarget = _target.position - transform.position;

        Quaternion lookTarget = Quaternion.LookRotation(toTarget);

        transform.rotation = lookTarget;
    }
}
