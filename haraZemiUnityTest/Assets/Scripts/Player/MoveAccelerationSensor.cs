using UnityEngine;

//加速度センサーの傾きに合わせてはんだごての位置を動かす

public class MoveAccelerationSensor : MonoBehaviour
{
    [SerializeField]
    float _xSpeed;

    [SerializeField]
    float _ySpeed;

    [SerializeField]
    SensorInput _sensorInput;

    [SerializeField]
    Transform _target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float foo = (float)_sensorInput.GyroZSubt / 40000;
        float bar = (float)_sensorInput.GyroXSubt / 40000;

        Vector3 move=new Vector3(-foo*_xSpeed, -bar*_ySpeed, 0);
        
        Vector3 pos=_target.position;

        _target.position = pos+move;
    }
}
