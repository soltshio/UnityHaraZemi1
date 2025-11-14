using System.Runtime.Remoting.Messaging;
using UnityEngine;

//加速度センサーの情報を使いやすい形で取得
//スイッチはセンサーの情報が変わったら通知

public class AccelerationSensorInput : MonoBehaviour
{
    //デフォルトの値は静止している時の値
    //Subtは静止状態からの差分

    //X方向の加速度
    int _accX;
    const int _defaultAccX=656;

    public int AccX { get { return _isUsedSensor ? _accX : 0; } }
    public int AccXSubt { get { return _isUsedSensor ? _accX - _defaultAccX : 0; } }

    //Y方向の加速度
    int _accY;
    const int _defaultAccY = -360;

    public int AccY { get { return _isUsedSensor ? _accY : 0; } }
    public int AccYSubt { get { return _isUsedSensor ? _accY - _defaultAccY : 0; } }

    //Z方向の加速度
    int _accZ;
    const int _defaultAccZ = 16410;

    public int AccZ { get { return _isUsedSensor ? _accZ : 0; } }
    public int AccZSubt { get { return _isUsedSensor ? _accZ - _defaultAccZ : 0; } }

    //ジャイロX
    int _gyroX;
    const int _defaultGyroX = 160;

    public int GyroX { get { return _isUsedSensor ? _gyroX : 0; } }
    public int GyroXSubt { get { return _isUsedSensor ? _gyroX - _defaultGyroX : 0; } }

    //ジャイロY
    int _gyroY;
    const int _defaultGyroY = 110;

    public int GyroY { get { return _isUsedSensor ? _gyroY : 0; } }
    public int GyroYSubt { get { return _isUsedSensor ? _gyroY - _defaultGyroY : 0; } }

    //ジャイロZ
    int _gyroZ;
    const int _defaultGyroZ = -70;

    public int GyroZ { get { return _isUsedSensor ? _gyroZ : 0; } }
    public int GyroZSubt { get { return _isUsedSensor ? _gyroZ - _defaultGyroZ : 0; } }


    bool _isUsedSensor = false;//加速度センサーを使っているか(一度もtrueに書き換えられなかったら使われていないということ)


    //accはx,y,zの順で三つ分、gyroも同様
    public void UpdateSensorInfo(int[] acc, int[] gyro)
    {
        _accX = acc[0];
        _accY = acc[1];
        _accZ = acc[2];
        _gyroX = gyro[0];
        _gyroY = gyro[1];
        _gyroZ = gyro[2];

        if (_isUsedSensor) return;

        _isUsedSensor = true;
    }
}
