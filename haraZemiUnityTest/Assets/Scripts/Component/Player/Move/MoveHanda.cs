using UnityEngine;
using UnityEngine.InputSystem;

//はんだごての左右上下の移動

public class MoveHanda : MonoBehaviour
{
    [Tooltip("初期地点")] [SerializeField]
    Transform _startPoint;

    [Tooltip("位置のリセット関係")] [SerializeField]
    ResetPos _resetPos;

    [Tooltip("速度")] [Min(0)] [SerializeField]
    Vector2 _speed;

    [Tooltip("移動限界")] [Min(0)] [SerializeField]
    Vector2 _limit;

    [Tooltip("角度の書き換え処理関係")] [SerializeField]
    RotateMoving _rotateMoving;

    [SerializeField]
    AccelerationSensorInput _accelerationSensorInput;

    [SerializeField]
    RepeatedInput _repeatedInput;

    [SerializeField]
    Rigidbody _target;

    Vector3 _start;

    Vector2 _position;

    Vector2 _move;

    const float _moveFactor_Sensor = 4000;

    const int _callReset_RepeatedCount = 2;

    public Vector2 Position { get { return _position; } }

    public void MoveInput(InputAction.CallbackContext context)
    {
        Vector2 getInput = context.ReadValue<Vector2>();

        double moveDeltaX = getInput.x * _speed.x;
        double moveDeltaY = getInput.y * _speed.y;

        _move = new Vector2((float)moveDeltaX, (float)moveDeltaY);
    }

    private void Awake()
    {
        _rotateMoving.Awake(_target, _limit);

        _repeatedInput.OnRepeatedInputDown_Count += ResetPos;
    }

    void ResetPos(int repeatedCount)
    {
        if (repeatedCount != _callReset_RepeatedCount) return;

        _resetPos.ResetTrigger(_position);
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
            _move = MoveVecFromSensor();

            destination = _position + _move * Time.deltaTime;
        }

        UpdatePos(destination);
    }

    Vector2 MoveVecFromSensor()//加速度センサーから取得した値での移動量の計算
    {
        if (!_accelerationSensorInput.IsUsedSensor) return _move;//センサーが使われていないなら、処理をしない

        //加速度センサーからの入力を移動量に変換
        double moveDeltaX = _accelerationSensorInput.GyroZSubt * _speed.x  / _moveFactor_Sensor;
        double moveDeltaY = _accelerationSensorInput.GyroXSubt * _speed.y  / _moveFactor_Sensor;

        Vector2 move = new Vector2((float)moveDeltaX, (float)moveDeltaY);

        return move;
    }

    void UpdatePos(Vector2 destination)
    {
        //制限
        _position.x = Mathf.Clamp(destination.x, -_limit.x, _limit.x);
        _position.y = Mathf.Clamp(destination.y, -_limit.y, _limit.y);

        Transform parent = _target.transform.parent;

        _rotateMoving.MoveRotation(_position,parent);//角度の処理

        //移動処理
        Vector3 foo = parent.rotation * new Vector3(_position.x, _position.y, 0);
        Vector3 pos = _start + foo;

        _target.MovePosition(pos);
    }
}
