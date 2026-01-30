using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _scoreText;

    private void OnEnable()
    {
        var instance = ScoreManager.Instance;

        if (instance == null) return;

        instance.OnChangeScore += RefreshUI;
    }

    private void OnDisable()
    {
        var instance = ScoreManager.Instance;

        if (instance == null) return;

        instance.OnChangeScore -= RefreshUI;
    }

    void RefreshUI(float score)
    {
        _scoreText.text = score.ToString();
    }
}
