using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private float attCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private Transform magicPoint;
    [SerializeField] private GameObject[] magicBalls;
    [SerializeField] private float colliderDist;
    [SerializeField] private BoxCollider2D bCollider;
    [SerializeField] private LayerMask pLayer;
    [SerializeField] private AudioClip magicAttackSound;


    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    private Patrol patrol;

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
            if (cooldownTimer >= attCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
            }
        }

        if (patrol != null)
            patrol.enabled = !PlayerInSight();
    }

    private void RangedAttack()
    {
        SoundManager.instance.PlaySound(magicAttackSound);

        cooldownTimer = 0;
        magicBalls[FindMagicBall()].transform.position = magicPoint.position;
        magicBalls[FindMagicBall()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    private int FindMagicBall()
    {
        for (int i = 0; i < magicBalls.Length; i++)
        {
            if (!magicBalls[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(bCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDist,
            new Vector3(bCollider.bounds.size.x * range, bCollider.bounds.size.y, bCollider.bounds.size.z),
            0, Vector2.left, 0, pLayer);

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDist,
            new Vector3(bCollider.bounds.size.x * range, bCollider.bounds.size.y, bCollider.bounds.size.z));
    }
}