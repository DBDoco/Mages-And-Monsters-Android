using UnityEngine;
using System.Collections;


public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float attCooldown;
    [SerializeField] private AudioClip magicAttackSound;

    private float timer = 100000000f;
    private Animator anim;
    bool canShoot = true;
    private Health health;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        health=GetComponent<Health>();
    }


  public void OnClick () {
        if (canShoot && !health.dead)
        {
            StartCoroutine(Attack());
        }
    }

    public IEnumerator Attack()
    {
        SoundManager.instance.PlaySound(magicAttackSound);
        anim.SetTrigger("attack");

        canShoot = false;
        Instantiate(projectile, firepoint.position, firepoint.rotation);
        yield return new WaitForSeconds(attCooldown);
        canShoot = true;

    }
}
