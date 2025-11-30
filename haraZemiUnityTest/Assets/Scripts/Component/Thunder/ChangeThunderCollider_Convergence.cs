using UnityEngine;

[System.Serializable]
struct ThunderRaySetting
{
    [Tooltip("発射口の円の半径")] [Min(0)]
    public float startRadius;

    [Tooltip("円錐角度")] [Min(0)]
    public float coneAngle;

    [Tooltip("レイ本数")] [Min(0)]
    public int rayCount;

    [Tooltip("レイを飛ばす距離")]
    public float rayDistance;

    [Tooltip("レイを撃つインターバル")] [Min(0.01f)]
    public float shotInterval;
}

public class ChangeThunderCollider_Convergence : MonoBehaviour
{
    [SerializeField]
    ShotThunderRay _shotThunderRay;

    [SerializeField]
    ThunderRaySetting _conValue;

    [SerializeField]
    ThunderRaySetting _divValue;

    [SerializeField]
    ThunderConvergence _thunderConvergence;

    [SerializeField]
    ActivateThunder _activateThunder;

    private void Awake()
    {
        _activateThunder.OnChangedValue += ChangeActive;
        _thunderConvergence.OnChangedValue += ChangeThunderColliderSize;
    }

    void ChangeActive(bool isActive)
    {
        _shotThunderRay.enabled = isActive;
    }

    void ChangeThunderColliderSize(float convergenceRate)
    {
        _shotThunderRay.StartRadius = Mathf.Lerp(_divValue.startRadius, _conValue.startRadius, convergenceRate);

        _shotThunderRay.ConeAngle = Mathf.Lerp(_divValue.coneAngle, _conValue.coneAngle, convergenceRate);

        _shotThunderRay.RayCount = (int)Mathf.Lerp((float)_divValue.rayCount, (float)_conValue.rayCount, convergenceRate);

        _shotThunderRay.RayDistance = Mathf.Lerp(_divValue.rayDistance, _conValue.rayDistance, convergenceRate);

        _shotThunderRay.ShotInterval = Mathf.Lerp(_divValue.shotInterval, _conValue.shotInterval, convergenceRate);
    }
}
