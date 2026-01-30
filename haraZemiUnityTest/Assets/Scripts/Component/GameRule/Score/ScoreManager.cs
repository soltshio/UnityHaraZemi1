using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    float score = 0;

    public event Action<float> OnChangeScore;

    public float Score
    {
        get { return score; }
        set
        {
            score = value;

            score = Mathf.Max(score, 0f);

            OnChangeScore?.Invoke(score);
        }
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
