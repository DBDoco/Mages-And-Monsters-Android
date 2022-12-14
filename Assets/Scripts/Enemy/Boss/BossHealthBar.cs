using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private BossHealth bHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        totalHealthBar.fillAmount = bHealth.currentHealth / 10;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = bHealth.currentHealth / 10;
    }
}
