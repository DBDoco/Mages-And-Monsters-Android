using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float attCooldown;
    [SerializeField] private AudioClip magicAttackSound;

    private float timer = 100000000f;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && timer > attCooldown)
            Attack();

        timer += Time.deltaTime;
    }

    public void Attack()
    {
        SoundManager.instance.PlaySound(magicAttackSound);
        anim.SetTrigger("attack");
        timer = 0;

        Instantiate(projectile, firepoint.position, firepoint.rotation);
    }
}
