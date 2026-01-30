using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ShowPinch : MonoBehaviour
{
    [SerializeField] Volume _volume;

    [SerializeField] 
    float _minIntensity;

    [SerializeField]
    float _maxIntensity;

    private Vignette _vignette;

    private void Awake()
    {
        if(!_volume.profile.TryGet<Vignette>(out _vignette))
        {
            Debug.Log("éÊìæÇ…é∏îs");
        }
    }

    void Start()
    {
        SetPinch(0f);
    }

    public void SetPinch(float rate)//rateÇÕ0Å`1
    {
        rate=Mathf.Clamp01(rate);

        _vignette.intensity.value = Mathf.Lerp(_minIntensity,_maxIntensity,rate);
    }
}
