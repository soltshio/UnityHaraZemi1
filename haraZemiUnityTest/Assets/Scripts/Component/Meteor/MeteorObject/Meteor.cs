using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField]
    Rigidbody _rb;

    float _speed = 5f;
    Transform _target;

    public void Init(float speed,Transform target)
    {
        _speed = speed;
        _target = target;
    }

    private void OnEnable()
    {
        //向きを決める(ターゲットの方向に向くようにする)
        Vector3 toTarget=_target.position-transform.position;

        Quaternion lookTarget = Quaternion.LookRotation(toTarget);

        transform.rotation = lookTarget;
    }

    void Update()
    {
        //移動
        
    }
}
