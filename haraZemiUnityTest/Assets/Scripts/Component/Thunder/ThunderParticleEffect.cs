using UnityEngine;
using System;

//ìdãCÇÃé˚ë©ó¶ÇïœçX

[System.Serializable]
struct ParticleChangeValue
{
    public float startSpeed;

    public float startSize;

    public float startLifeTimeMin;
    public float startLifeTimeMax;

    public float simulationSpeed;

    public float rateOverTime;
    public float angle;

    public float strength;
    public Color colorOverTime;
}

public class ThunderParticleEffect : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _thunderParticle;

    [SerializeField]
    ParticleChangeValue _conValue;

    [SerializeField]
    ParticleChangeValue _divValue;

    [SerializeField]
    ThunderConvergence _thunderConvergence;

    [SerializeField]
    ActivateThunder _activateThunder;

    private void OnEnable()
    {
        _thunderConvergence.OnChangedValue += ChangeParticle;
        _activateThunder.OnChangedValue += ChangeActive;
    }

    private void OnDisable()
    {
        _thunderConvergence.OnChangedValue -= ChangeParticle;
        _activateThunder.OnChangedValue -= ChangeActive;
    }

    private void Start()
    {
        _thunderParticle.Stop();
    }

    void ChangeActive(bool isActive)
    {
        if(isActive)
        {
            _thunderParticle.Play();
        }
        else
        {
            _thunderParticle.Stop();
        }
    }

    void ChangeParticle(float convergenceRate)
    {
        var main = _thunderParticle.main;

        main.startSpeed = Mathf.Lerp(_divValue.startSpeed, _conValue.startSpeed, convergenceRate);

        main.startSize = Mathf.Lerp(_divValue.startSize, _conValue.startSize, convergenceRate);

        var lifetime = main.startLifetime;
        lifetime.constantMin = Mathf.Lerp(_divValue.startLifeTimeMin, _conValue.startLifeTimeMin,convergenceRate);
        lifetime.constantMax = Mathf.Lerp(_divValue.startLifeTimeMax, _conValue.startLifeTimeMax, convergenceRate);
        main.startLifetime = lifetime;

        main.simulationSpeed = Mathf.Lerp(_divValue.simulationSpeed, _conValue.simulationSpeed, convergenceRate);

        var emission = _thunderParticle.emission;
        emission.rateOverTime = Mathf.Lerp(_divValue.rateOverTime,_conValue.rateOverTime,convergenceRate);

        var shape = _thunderParticle.shape;
        shape.angle = Mathf.Lerp(_divValue.angle,_conValue.angle,convergenceRate);

        var noise = _thunderParticle.noise;
        noise.strength = Mathf.Lerp(_divValue.strength,_conValue.strength,convergenceRate);

        var trails = _thunderParticle.trails;
        trails.colorOverTrail = Color.Lerp(_divValue.colorOverTime,_conValue.colorOverTime,convergenceRate);
    }
}
