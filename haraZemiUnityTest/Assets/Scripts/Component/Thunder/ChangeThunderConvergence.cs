using UnityEngine;

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

public class ChangeThunderConvergence : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _thunderParticle;

    [SerializeField]
    ParticleChangeValue _conValue;

    [SerializeField]
    ParticleChangeValue _divValue;

    float _convergenceRate;

    public float ConvergenceRate
    {
        get { return _convergenceRate; }
        set
        {
            _convergenceRate = Mathf.Clamp01(value);
            ChangeParticle();
        }
    }

    private void Awake()
    {
        _convergenceRate = 0f;
    }

    private void Start()
    {
        ConvergenceRate = 0f;
    }

    void ChangeParticle()
    {
        var main = _thunderParticle.main;

        main.startSpeed = Mathf.Lerp(_divValue.startSpeed, _conValue.startSpeed, _convergenceRate);

        main.startSize = Mathf.Lerp(_divValue.startSize, _conValue.startSize, _convergenceRate);

        var lifetime = main.startLifetime;
        lifetime.constantMin = Mathf.Lerp(_divValue.startLifeTimeMin, _conValue.startLifeTimeMin,_convergenceRate);
        lifetime.constantMax = Mathf.Lerp(_divValue.startLifeTimeMax, _conValue.startLifeTimeMax, _convergenceRate);
        main.startLifetime = lifetime;

        main.simulationSpeed = Mathf.Lerp(_divValue.simulationSpeed, _conValue.simulationSpeed, _convergenceRate);

        var emission = _thunderParticle.emission;
        emission.rateOverTime = Mathf.Lerp(_divValue.rateOverTime,_conValue.rateOverTime,_convergenceRate);

        var shape = _thunderParticle.shape;
        shape.angle = Mathf.Lerp(_divValue.angle,_conValue.angle,_convergenceRate);

        var noise = _thunderParticle.noise;
        noise.strength = Mathf.Lerp(_divValue.strength,_conValue.strength,_convergenceRate);

        var trails = _thunderParticle.trails;
        trails.colorOverTrail = Color.Lerp(_divValue.colorOverTime,_conValue.colorOverTime,_convergenceRate);
    }
}
