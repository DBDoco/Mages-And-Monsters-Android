using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLastScene : MonoBehaviour
{
    public void LoadNewScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LastScene");
    }
}