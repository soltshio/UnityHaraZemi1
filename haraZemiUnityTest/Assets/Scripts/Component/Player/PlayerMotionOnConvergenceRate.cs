using UnityEngine;

//収束率に合わせてモーションのブレンド率を変更

public class PlayerMotionOnConvergenceRate : MonoBehaviour
{
    [SerializeField]
    Animator _animator;

    [SerializeField]
    ThunderConvergence _thunderConvergence;

    const string _blendParameterName = "Blend";

    private void Awake()
    {
        _thunderConvergence.OnChangedValue += OnChangeConvergence;
    }

    void OnChangeConvergence(float convergenceRate)
    {
        _animator.SetFloat(_blendParameterName, convergenceRate);
    }
}
