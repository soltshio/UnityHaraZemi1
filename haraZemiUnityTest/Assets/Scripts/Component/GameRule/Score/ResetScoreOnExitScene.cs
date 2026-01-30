using UnityEngine;

public class ResetScoreOnExitScene : MonoBehaviour
{
    public void DestroyScoreInstance()
    {
        var scoreInstance = ScoreManager.Instance;

        if(scoreInstance==null) return;

        Destroy(scoreInstance.gameObject);
    }
}
