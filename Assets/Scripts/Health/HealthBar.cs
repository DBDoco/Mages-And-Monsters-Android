using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health pHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        totalHealthBar.fillAmount = pHealth.currentHealth / 10;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = pHealth.currentHealth / 10;
    }
}
