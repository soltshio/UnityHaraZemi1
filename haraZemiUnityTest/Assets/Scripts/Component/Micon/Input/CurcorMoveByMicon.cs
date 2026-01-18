using UnityEngine;

//マイコン操作によるカーソル移動

public class CurcorMoveByMicon : MonoBehaviour
{
    [SerializeField]
    AccelerationSensorInput _accelerationSensorInput;

    [SerializeField]
    Vector2 _speed;

    //センサー操作関係の係数
    const float _moveFactor_Sensor = 0.00025f;

    // Update is called once per frame
    void Update()
    {
        if (!_accelerationSensorInput.IsUsedSensor) return;//センサーが使われていないなら、処理をしない

        if (!MouseCursorHandler.TryGetPosition(out int x, out int y)) return;

        //加速度センサーからの入力を移動量に変換
        double moveDeltaX = _accelerationSensorInput.GyroZSubt * _speed.x * _moveFactor_Sensor;
        double moveDeltaY = _accelerationSensorInput.GyroXSubt * _speed.y * _moveFactor_Sensor;

        int newX = x + (int)moveDeltaX;
        int newY = y + (int)moveDeltaY;

        MouseCursorHandler.Move(newX, newY);
    }
}
