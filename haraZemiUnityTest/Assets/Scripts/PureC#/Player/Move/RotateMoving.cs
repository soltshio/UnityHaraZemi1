using UnityEngine;
using UnityEngine.UIElements;

//移動時の角度の変更

[System.Serializable]
public class RotateMoving
{
    [Tooltip("角度が変わり始める位置")] [Min(0)] [SerializeField]
    Vector2 _deadZone;

    [Tooltip("最大の角度")] [Min(0)] [SerializeField]
    Vector2 _maxAngle;

    Vector2 _limit;

    Rigidbody _target;

    public void Awake(Rigidbody target,Vector2 limit)
    {
        _target = target;
        _limit = limit;
    }

    public void MoveRotation(Vector2 position,Transform parent)
    {
        float deltaX = Mathf.Max(0, Mathf.Abs(position.x) - _deadZone.x);

        float deltaY = Mathf.Max(0, Mathf.Abs(position.y) - _deadZone.y);

        float tx = deltaX / (_limit.x - _deadZone.x);
        float ty = deltaY / (_limit.y - _deadZone.y);

        float angleX = Mathf.Lerp(0, _maxAngle.x, tx);
        float angleY = Mathf.Lerp(0, _maxAngle.y, ty);

        Quaternion rotationX = Quaternion.AngleAxis(Mathf.Sign(position.x) * angleX, Vector3.up);
        Quaternion rotationY = Quaternion.AngleAxis(Mathf.Sign(position.y) * angleY, Vector3.left);

        _target.MoveRotation(parent.rotation * rotationX * rotationY);
    }
}
