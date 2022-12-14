using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class CrossFade : MonoBehaviour
{
    public Animator anim;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        anim.SetTrigger("start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);
    }
}
