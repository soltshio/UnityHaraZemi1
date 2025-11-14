using System.Runtime.Remoting.Messaging;
using UnityEngine;

//加速度センサーの情報を使いやすい形で取得

public class AccelerationSensorInput : MonoBehaviour
{
    //デフォルトの値は静止している時の値
    //Subtは静止状態からの差分

    //ジャイロX
    int _gyroX;
    const int _defaultGyroX = 160;

    public int GyroX { get { return _isUsedSensor ? _gyroX : 0; } }
    public int GyroXSubt { get { return _isUsedSensor ? _gyroX - _defaultGyroX : 0; } }

    //ジャイロZ
    int _gyroZ;
    const int _defaultGyroZ = -70;

    public int GyroZ { get { return _isUsedSensor ? _gyroZ : 0; } }
    public int GyroZSubt { get { return _isUsedSensor ? _gyroZ - _defaultGyroZ : 0; } }


    bool _isUsedSensor = false;//加速度センサーを使っているか(一度もtrueに書き換えられなかったら使われていないということ)

    public bool IsUsedSensor {  get { return _isUsedSensor; } }


    //accはx,y,zの順で三つ分、gyroも同様
    public void UpdateSensorInfo(int gyroX, int gyroZ)
    {
        _gyroX = gyroX;
        _gyroZ = gyroZ;

        if (_isUsedSensor) return;

        _isUsedSensor = true;
    }
}
