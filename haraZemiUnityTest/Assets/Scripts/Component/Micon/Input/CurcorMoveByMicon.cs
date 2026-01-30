using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

//マイコン操作によるカーソル移動

public class CurcorMoveByMicon : MonoBehaviour
{
    [SerializeField]
    AccelerationSensorInput _accelerationSensorInput;

    [SerializeField]
    Vector2 _speed;

    //センサー操作関係の係数
    const float _moveFactor_Sensor = 0.02f;

    bool _enableControl = true;

    void Update()
    {
        // 新InputSystem版：ESCキー判定
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            _enableControl = !_enableControl;
        }

        if (!_enableControl) return;
        if (!_accelerationSensorInput.IsUsedSensor) return;
        if (Mouse.current == null) return;

        Vector2 currentPos = Mouse.current.position.ReadValue();

        float moveDeltaX = (float)(_accelerationSensorInput.GyroZSubt * _speed.x * _moveFactor_Sensor);
        float moveDeltaY = (float)(_accelerationSensorInput.GyroXSubt * _speed.y * _moveFactor_Sensor);

        moveDeltaX *= Time.deltaTime;
        moveDeltaY *= Time.deltaTime;

        Vector2 newPos = currentPos + new Vector2(moveDeltaX, moveDeltaY);

        newPos.x = Mathf.Clamp(newPos.x, 0, Screen.width);
        newPos.y = Mathf.Clamp(newPos.y, 0, Screen.height);

        // Unityのマウス入力を更新（UI用）
        InputSystem.QueueStateEvent(Mouse.current, new MouseState
        {
            position = newPos
        });
        InputSystem.Update();

        // 見た目のカーソルも動かす
        Mouse.current.WarpCursorPosition(newPos);
    }
}
