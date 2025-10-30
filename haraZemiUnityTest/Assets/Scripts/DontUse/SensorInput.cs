using System.Runtime.Remoting.Messaging;
using UnityEngine;

//センサーの情報を使いやすい形で取得
//スイッチはセンサーの情報が変わったら通知

public class SensorInput : MonoBehaviour
{
    //デフォルトの値は静止している時の値
    //Subtは静止状態からの差分

    //X方向の加速度
    int _accX;
    const int _defaultAccX=656;

    public int AccX { get { return _accX; } }
    public int AccXSubt { get { return _accX - _defaultAccX; } }

    //Y方向の加速度
    int _accY;
    const int _defaultAccY = -360;

    public int AccY { get { return _accY; } }
    public int AccYSubt { get { return _accY - _defaultAccY; } }

    //Z方向の加速度
    int _accZ;
    const int _defaultAccZ = 16410;

    public int AccZ { get { return _accZ; } }
    public int AccZSubt { get { return _accZ - _defaultAccZ; } }

    //ジャイロX
    int _gyroX;
    const int _defaultGyroX = 160;

    public int GyroX { get { return _gyroX; } }
    public int GyroXSubt { get { return _gyroX - _defaultGyroX; } }

    //ジャイロY
    int _gyroY;
    const int _defaultGyroY = 110;

    public int GyroY { get { return _gyroY; } }
    public int GyroYSubt { get { return _gyroY - _defaultGyroY; } }

    //ジャイロZ
    int _gyroZ;
    const int _defaultGyroZ = -70;

    public int GyroZ { get { return _gyroZ; } }
    public int GyroZSubt { get { return _gyroZ - _defaultGyroZ; } }

    //accはx,y,zの順で三つ分、gyroも同様
    public void UpdateSensorInfo(int[] acc, int[] gyro)
    {
        _accX = acc[0];
        _accY = acc[1];
        _accZ = acc[2];
        _gyroX = gyro[0];
        _gyroY = gyro[1];
        _gyroZ = gyro[2];
    }
}
