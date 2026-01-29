using TMPro;
using UnityEngine;

public class ResultScoreUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _scoreText;

    private void Start()
    {
        var instance = ScoreManager.Instance;

        if (instance == null) return;

        _scoreText.text = instance.Score.ToString();
    }
}
