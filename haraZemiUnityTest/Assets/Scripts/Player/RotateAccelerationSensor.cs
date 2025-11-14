using UnityEngine;

//加速度センサーの傾きに合わせて回転させる

public class RotateAccelerationSensor : MonoBehaviour
{
    [SerializeField]
    float _xSpeed;

    [SerializeField]
    float _ySpeed;

    [SerializeField]
    float _zSpeed;

    [SerializeField]
    AccelerationSensorInput _sensorInput;

    [SerializeField]
    Transform _parent;

    [SerializeField]
    Transform _target;

    // Update is called once per frame
    void Update()
    {
        float foo = (float)_sensorInput.GyroXSubt / 4000;
        float angleX = foo * _xSpeed;

        float bar = (float)_sensorInput.GyroYSubt / 4000;
        float angleY = bar * _ySpeed;

        float baz = (float)_sensorInput.GyroZSubt / 4000;
        float angleZ = baz * _zSpeed;

        //ワールドのx軸を軸に回す
        Quaternion rotX = Quaternion.AngleAxis(angleX,_parent.right);
        Quaternion rotY = Quaternion.AngleAxis(angleY,-_parent.up);
        Quaternion rotZ = Quaternion.AngleAxis(angleZ,-_parent.forward);

        _target.rotation = _target.rotation*rotX*rotY*rotZ;
    }
}
