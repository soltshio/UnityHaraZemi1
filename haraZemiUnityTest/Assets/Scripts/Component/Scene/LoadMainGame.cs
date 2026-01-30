using UnityEngine;
using UnityEngine.SceneManagement;

//インゲームのシーンに移行

public class LoadMainGame : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

}
