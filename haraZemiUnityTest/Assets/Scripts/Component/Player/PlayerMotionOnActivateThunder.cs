using UnityEngine;

//電気の発生状態に合わせて、モーションを変更

public class PlayerMotionOnActivateThunder : MonoBehaviour
{
    [SerializeField]
    Animator _animator;

    [SerializeField]
    ActivateThunder _activateThunder;

    const string _blendParameterName = "Sparking";

    private void OnEnable()
    {
        _activateThunder.OnChangedValue += OnChangeConvergence;
    }

    private void OnDisable()
    {
        _activateThunder.OnChangedValue -= OnChangeConvergence;
    }

    void OnChangeConvergence(bool isActivate)
    {
        _animator.SetBool(_blendParameterName, isActivate);
    }
}
