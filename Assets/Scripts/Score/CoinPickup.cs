using UnityEngine;
using UnityEngine.UI;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Coin")
        {
            SoundManager.instance.PlaySound(coinPickupSound);
            Destroy(collision.gameObject);
            ScoreManager.instance.AddPoint();
        }
    }
    private void ResetScore()
    {
        ScoreManager.instance.RemoveScore();
    }
}
