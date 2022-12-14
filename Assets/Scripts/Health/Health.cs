using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float startHealth;

    [SerializeField] private float invulnerableTime;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private Behaviour[] components;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;



    private SpriteRenderer sprite;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private Scene m_Scene;
    private string sceneName;

    private void Awake()
    {
        currentHealth = startHealth;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        sceneName = m_Scene.name;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startHealth);
        if (currentHealth > 0)
        {
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

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startHealth);
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

    public void PlayerRespawn()
    {
        dead = false;
        AddHealth(startHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invulnerability());
        if (sceneName == "1 Level" || sceneName == "2 Level")
            ScoreManager.instance.RemoveScore();

        foreach (Behaviour component in components)
            component.enabled = true;
    }
}
