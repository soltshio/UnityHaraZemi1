using UnityEngine;

//収束率によって、ビームと電気の音を混ぜる

public class ThunderSE : MonoBehaviour
{
    [SerializeField]
    ThunderConvergence _thunderConvergence;

    [SerializeField]
    ActivateThunder _activateThunder;

    [Tooltip("電気音")] [SerializeField]
    AudioVolumeChange _thunder;

    [Tooltip("ビーム音")] [SerializeField]
    AudioVolumeChange _beam;

    private void Awake()
    {
        _thunderConvergence.OnChangedValue += OnChangeConvergence;
        _activateThunder.OnChangedValue += OnActivate;
    }

    void OnActivate(bool isActivate)
    {
        _thunder.SwitchPlay(isActivate);
        _beam.SwitchPlay(isActivate);
    }

    void OnChangeConvergence(float convergenceRate)
    {
        _thunder.ChangeVolume(1-convergenceRate);
        _beam.ChangeVolume(convergenceRate);
    }
}
