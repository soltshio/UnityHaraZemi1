using UnityEngine;
using UnityEngine.InputSystem;

//はんだごての左右上下の移動

public class Moving : MonoBehaviour
{
    [Tooltip("初期地点")] [SerializeField]
    Transform _startPoint;

    [Tooltip("位置のリセット関係")] [SerializeField]
    ResetPos _resetPos;

    [Tooltip("速度")] [Min(0)] [SerializeField]
    Vector2 _speed;

    [Tooltip("移動限界")] [Min(0)] [SerializeField]
    Vector2 _limit;

    [Tooltip("角度が変わり始める位置")] [Min(0)] [SerializeField]
    Vector2 _deadZone;

    [Tooltip("最大の角度")] [Min(0)] [SerializeField]
    Vector2 _maxAngle;

    [SerializeField]
    AccelerationSensorInput _accelerationSensorInput;

    [SerializeField]
    Rigidbody _target;

    Vector3 _start;

    Vector2 _position;

    Vector2 _move;

    public void ResetPos(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        _resetPos.OnPushedResetButton(_position);
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        Vector2 getInput = context.ReadValue<Vector2>();

        float moveDeltaX = getInput.x * Time.deltaTime * _speed.x / 10;
        float moveDeltaY = getInput.y * Time.deltaTime * _speed.y / 10;

        _move = new Vector2(moveDeltaX, moveDeltaY);
    }

    void Start()
    {
        _position = Vector2.zero;
        _move = Vector2.zero;
        _start = _startPoint.position;
        _target.MovePosition(_start);
    }

    void Update()
    {
        Vector2 destination;

        if (_resetPos.IsReseting)//リスタート中
        {
            destination = _resetPos.DestinationOnReset();
        }
        else//移動中
        {
            _move = CalcMoveFromSensor();

            destination = _position + _move;
        }

        UpdatePos(destination);
    }

    Vector2 CalcMoveFromSensor()//加速度センサーから取得した値での移動量の計算
    {
        if (!_accelerationSensorInput.IsUsedSensor) return _move;//センサーが使われていないなら、処理をしない

        //加速度センサーからの入力を移動量に変換
        float moveDeltaX = (float)_accelerationSensorInput.GyroZSubt * Time.deltaTime * _speed.x / 40000;
        float moveDeltaY = (float)_accelerationSensorInput.GyroXSubt * Time.deltaTime * _speed.y / 40000;

        Vector2 move = new Vector2(-moveDeltaX, -moveDeltaY);

        return move;
    }

    void UpdatePos(Vector2 destination)
    {
        //制限
        _position.x = Mathf.Clamp(destination.x, -_limit.x, _limit.x);
        _position.y = Mathf.Clamp(destination.y, -_limit.y, _limit.y);

        //角度の処理
        float deltaX = Mathf.Max(0, Mathf.Abs(_position.x) - _deadZone.x);

        float deltaY = Mathf.Max(0, Mathf.Abs(_position.y) - _deadZone.y);

        float tx = deltaX / (_limit.x - _deadZone.x);
        float ty = deltaY / (_limit.y - _deadZone.y);

        float angleX = Mathf.Lerp(0, _maxAngle.x, tx);
        float angleY = Mathf.Lerp(0, _maxAngle.y, ty);

        Transform parent = _target.transform.parent;

        Quaternion rotationX = Quaternion.AngleAxis(Mathf.Sign(_position.x) * angleX,Vector3.up);
        Quaternion rotationY = Quaternion.AngleAxis(Mathf.Sign(_position.y) * angleY, Vector3.left);

        _target.MoveRotation(parent.rotation * rotationX * rotationY);

        //移動処理
        Vector3 foo = parent.rotation * new Vector3(_position.x, _position.y, 0);

        Vector3 pos = _start + foo;

        _target.MovePosition(pos);
    }
}
