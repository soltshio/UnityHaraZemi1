using UnityEngine;
using UnityEngine.InputSystem;

//はんだごての左右上下の移動

public class Moving : MonoBehaviour
{
    [Tooltip("初期地点")] [SerializeField]
    Transform _startPoint;

    [Tooltip("リセットにかける時間")] [Min(0)] [SerializeField]
    float _resetDuration;

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

    //位置リセット関係
    float _current;
    Vector2 _prePos;

    public bool IsReseting { get { return _current > 0; } }

    public void ResetPos(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        //リセット処理開始
        _current = _resetDuration;
        _prePos = _position;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _position = Vector2.zero;
        _start = _startPoint.position;
        _target.MovePosition(_start);
    }

    // Update is called once per frame
    void Update()
    {
        ResetMove();

        CalcMove();
    }

    void ResetMove()
    {
        if (!IsReseting) return;

        _current -= Time.deltaTime;
        float t = _current / _resetDuration;

        Vector2 destination;

        destination.x = Mathf.Lerp(0, _prePos.x, t);
        destination.y = Mathf.Lerp(0, _prePos.y, t);

        UpdatePos(destination);
    }

    void CalcMove()//移動量の計算
    {
        if (IsReseting) return;

        //加速度センサーからの入力を移動量に変換
        float moveDeltaX = (float)_accelerationSensorInput.GyroZSubt * Time.deltaTime * _speed.x / 40000;
        float moveDeltaY = (float)_accelerationSensorInput.GyroXSubt * Time.deltaTime * _speed.y / 40000;

        Vector2 move = new Vector2(-moveDeltaX, -moveDeltaY);

        UpdatePos(_position + move);
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
