using UnityEngine;
using UnityEngine.InputSystem;

//はんだごての左右上下の移動

public class Moving : MonoBehaviour
{
    [Tooltip("初期地点")] [SerializeField]
    Transform _startPoint;

    [Tooltip("リセットにかける時間")] [SerializeField]
    float _resetDuration;

    [Tooltip("速度")] [SerializeField]
    Vector2 _speed;

    [Tooltip("移動限界")] [SerializeField]
    Vector2 _limit;

    [Tooltip("角度が変わり始める位置")] [SerializeField]
    Vector2 _deadZone;

    [Tooltip("最大の角度")] [SerializeField]
    Vector2 _maxAngle;

    [SerializeField]
    SensorInput _sensorInput;

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

        Debug.Log(_prePos + ":" + destination);

        UpdatePos(destination);
    }

    void CalcMove()//移動量の計算
    {
        if (IsReseting) return;

        //加速度センサーからの入力を移動量に変換
        float moveDeltaX = (float)_sensorInput.GyroZSubt * Time.deltaTime * _speed.x / 40000;
        float moveDeltaY = (float)_sensorInput.GyroXSubt * Time.deltaTime * _speed.y / 40000;

        Vector2 move = new Vector2(-moveDeltaX, -moveDeltaY);

        UpdatePos(_position + move);
    }

    void UpdatePos(Vector2 destination)
    {
        //制限
        _position.x = Mathf.Clamp(destination.x, -_limit.x, _limit.x);
        _position.y = Mathf.Clamp(destination.y, -_limit.y, _limit.y);

        //角度の処理
        if (Mathf.Abs(_position.x) >= _deadZone.x)
        {
            //float deltaX= Mathf.Abs(_position.x)
        }

        if (Mathf.Abs(_position.y) >= _deadZone.y)
        {

        }

        //float angleX;
        //float angleY;

        //移動処理
        Vector3 pos = _start + new Vector3(_position.x, _position.y, 0);

        _target.MovePosition(pos);
    }
}
