using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Resume()
    {
        SceneManager.LoadScene("BossScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
