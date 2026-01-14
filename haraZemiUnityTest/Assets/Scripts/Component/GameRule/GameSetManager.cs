using UnityEngine;
using UnityEngine.SceneManagement;

//ゲーム終了処理のマネージャー
public class GameSetManager : MonoBehaviour
{
    [SerializeField]
    HitPoint _bossHP;

    [SerializeField]
    GameOverConditionChecker _gameOverConditionChecker;

    bool _isGameSet = false;

    private void OnEnable()
    {
        _bossHP.OnDead += ToClear;
        _gameOverConditionChecker.OnSetGameOver += ToGameOver;
    }

    private void OnDisable()
    {
        _bossHP.OnDead -= ToClear;
        _gameOverConditionChecker.OnSetGameOver -= ToGameOver;
    }

    void ToGameOver()
    {
        if (_isGameSet) return;

        _isGameSet = true;
        SceneManager.LoadScene("GameOverScene");
    }

    void ToClear()
    {
        if (_isGameSet) return;

        _isGameSet = true;
        SceneManager.LoadScene("ClearScene");
    }
}
