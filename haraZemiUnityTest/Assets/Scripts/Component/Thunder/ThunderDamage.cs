using UnityEngine;

//電気のダメージ(収束率によって変更)

public class ThunderDamage : MonoBehaviour
{
    [Tooltip("拡散時のダメージ")] [SerializeField]
    float _divDamage;

    [Tooltip("収束時のダメージ")] [SerializeField]
    float _conDamage;

    [SerializeField]
    ThunderConvergence _thunderConvergence;

    float _damage;

    public float Damage { get { return _damage; } }

    private void Awake()
    {
        _damage = _divDamage;

        _thunderConvergence.OnChangedValue += OnChangeConvergence;
    }

    void OnChangeConvergence(float convergenceRate)
    {
        _damage = Mathf.Lerp(_divDamage, _conDamage, convergenceRate);
    }
}
