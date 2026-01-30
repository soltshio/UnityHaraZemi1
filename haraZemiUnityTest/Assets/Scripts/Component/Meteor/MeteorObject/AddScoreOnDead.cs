using UnityEngine;

public class AddScoreOnDead : MonoBehaviour
{
    [SerializeField]
    float _addScoreValue = 10;

    [SerializeField]
    HitPoint _hitPoint;

    private void OnEnable()
    {
        _hitPoint.OnDead += AddScore;
    }

    private void OnDisable()
    {
        _hitPoint.OnDead -= AddScore;
    }

    void AddScore()
    {
        var instance = ScoreManager.Instance;

        if (instance == null) return;

        instance.Score += _addScoreValue;
    }
}
