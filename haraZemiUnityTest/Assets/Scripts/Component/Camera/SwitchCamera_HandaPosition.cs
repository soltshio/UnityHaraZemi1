using Unity.Cinemachine;
using UnityEngine;

//はんだの位置によってカメラを切り替える

public class SwitchCamera_HandaPosition : MonoBehaviour
{
    [Tooltip("カメラ切り替えを行う境界")] [Min(0)] [SerializeField]
    Vector2 _threshold;

    [Tooltip("中心側のカメラ")] [SerializeField]
    CinemachineCamera _centerVCam;

    [Tooltip("はんだを追跡する方のカメラ")] [SerializeField]
    CinemachineCamera _trackVCam;

    [SerializeField]
    MoveHanda _moveHanda;

    bool _prePlayerIsCenter=true;//前のフレームでプレイヤーが中心側にいたか

    void Start()
    {
        SwitchCamera(true);
    }

    void Update()
    {
        bool playerIsCenter = PlayerIsCenter();

        if(playerIsCenter != _prePlayerIsCenter)
        {
            SwitchCamera(playerIsCenter);
        }

        _prePlayerIsCenter = playerIsCenter;
    }

    void SwitchCamera(bool isActivateCenterVCam)
    {
        _centerVCam.gameObject.SetActive(isActivateCenterVCam);
        _trackVCam.gameObject.SetActive(!isActivateCenterVCam);
    }

    bool PlayerIsCenter()
    {
        Vector2 position = _moveHanda.Position;

        bool isCenterX = Mathf.Abs(position.x) < _threshold.x;

        bool isCenterY = Mathf.Abs(position.y) < _threshold.y;

        return isCenterX && isCenterY;
    }
}
