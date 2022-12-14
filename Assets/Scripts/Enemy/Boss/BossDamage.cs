using UnityEngine;

public class BossDamage : MonoBehaviour
{
    [SerializeField] private BoxCollider2D bCollider;
    [SerializeField] private float attCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private int rageDamage;
    [SerializeField] private float colliderDistance;
    [SerializeField] private LayerMask pLayer;
    [SerializeField] private AudioClip attackSound;


    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    private Patrol patrol;
    private Health pHealth;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        patrol = GetComponentInParent<Patrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer >= attCooldown && pHealth.currentHealth > 0)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
                SoundManager.instance.PlaySound(attackSound);
            }
        }

        if (patrol != null)
            patrol.enabled = !PlayerInSight();
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(bCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(bCollider.bounds.size.x * range, bCollider.bounds.size.y, bCollider.bounds.size.z),
            0, Vector2.left, 0, pLayer);

        if (hit.collider != null)
            pHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(bCollider.bounds.size.x * range, bCollider.bounds.size.y, bCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
            pHealth.TakeDamage(damage);
    }

    private void RageDamagePlayer()
    {
        if (PlayerInSight())
            pHealth.TakeDamage(rageDamage);
    }
}