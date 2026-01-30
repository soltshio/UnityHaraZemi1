using UnityEngine;
using System.Collections;
using System;

//雷の当たり判定
//発射口が円状の太い円錐状の当たり判定

public class ShotThunderRay : MonoBehaviour
{
    [Tooltip("発射中心点\n青矢印方向に向かってレイを撃つ")] [SerializeField]
    Transform _startCenterPoint;

    [Tooltip("発射口の円の半径")] [SerializeField] [Min(0)] 
    float _startRadius = 0.5f;

    [Tooltip("円錐角度")] [SerializeField] [Min(0)]
    float _coneAngle = 30f;

    [Tooltip("レイ本数")] [SerializeField] [Min(0)]
    int _rayCount = 200;

    [Tooltip("レイを飛ばす距離")] [SerializeField]
    float _rayDistance = 20f;

    [Tooltip("レイを見えるようにするか\nSceneビュー限定で可視化")] [SerializeField]
    bool _isShowDebugRay;

    [Tooltip("レイが当たるレイヤー")] [SerializeField]
    LayerMask _hitLayer;

    [Tooltip("レイを撃つインターバル")] [SerializeField] [Min(0.01f)]
    float _shotInterval = 0.1f;

    private Coroutine _shootRoutine;

    public event Action<RaycastHit> OnHit;


    public float StartRadius
    {
        get { return _startRadius; }
        set { _startRadius = value; }
    }

    public float ConeAngle
    {
        get { return _coneAngle; }
        set { _coneAngle = value; }
    }

    public int RayCount
    {
        get { return _rayCount; }
        set { _rayCount = value; }
    }

    public float RayDistance
    { 
        get { return _rayDistance; }
        set { _rayDistance = value; }
    }

    public float ShotInterval
    {
        get { return _shotInterval; }
        set { _shotInterval = value; }
    }


    void OnEnable()
    {
        // enabled が true になったときに撃ち始める
        _shootRoutine = StartCoroutine(ShootLoop());
    }

    void OnDisable()
    {
        // enabled が false になったら止める
        if (_shootRoutine != null)
        {
            StopCoroutine(_shootRoutine);
            _shootRoutine = null;
        }
    }

    IEnumerator ShootLoop()
    {
        while (true)
        {
            ShootConeRays();
            yield return new WaitForSeconds(_shotInterval);
        }
    }

    void ShootConeRays()
    {
        for (int i = 0; i < _rayCount; i++)
        {
            //発射口（円）上のランダム位置
            Vector3 startPos = MathfExtension.RandomPointInCircle(_startRadius);

            //ローカル → ワールド
            startPos = _startCenterPoint.TransformPoint(startPos);

            //円錐方向のランダム方向
            Vector3 dir = MathfExtension.RandomDirectionInCone(_startCenterPoint.forward, _coneAngle);

            //レイの可視化
            if(_isShowDebugRay) Debug.DrawLine(startPos, startPos+dir* _rayDistance, Color.red,0.5f);
            
            if (!Physics.Raycast(startPos, dir, out RaycastHit hit, _rayDistance, _hitLayer)) continue;

            OnHit?.Invoke(hit);
        }
    }
}
