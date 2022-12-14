using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health pHealth;
    private Scene m_Scene;
    private string sceneName;



    private void Awake()
    {
        pHealth = GetComponent<Health>();
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
    }

    public void PlayerRespawn()
    {
        pHealth.PlayerRespawn(); 
        if (sceneName=="BossScene")
            SceneManager.LoadScene("GameOver");
        else
            transform.position = currentCheckpoint.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("activated");
        }
    }
}