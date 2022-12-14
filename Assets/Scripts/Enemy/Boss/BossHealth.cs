using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BossHealth : MonoBehaviour
{
    [SerializeField] private float startHealth;
    [SerializeField] private float invulnerableTime;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private Behaviour[] components;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    public bool isInvulnerable = false;
    private SpriteRenderer sprite;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private bool isCoroutineExecuting = false;

    private void Awake()
    {
        currentHealth = startHealth;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (isInvulnerable)
            return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startHealth);
        if (currentHealth > 0)
        {
            if (currentHealth <= 5)
                anim.SetBool("IsRage", true);
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {

                foreach (Behaviour comp in components)
                    comp.enabled = false;

                anim.SetBool("grounded", true);
                anim.SetTrigger("die");
                dead = true;

                SoundManager.instance.PlaySound(deathSound);
            }

        }
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            yield return new WaitForSeconds(invulnerableTime/(numberOfFlashes*2));
            yield return new WaitForSeconds(invulnerableTime / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
