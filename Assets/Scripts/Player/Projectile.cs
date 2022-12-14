using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private Rigidbody2D rBody;
    [SerializeField] private GameObject inpactEffect;


    private BoxCollider2D bCollider;
    private bool hit;
    private float lifeTime;

    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        bCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit)
            return;

        lifeTime += Time.deltaTime;
        if (lifeTime > 5)
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        rBody.velocity = transform.right * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;

        bCollider.enabled = false;

        if (collision.tag == "Enemy")
            collision.GetComponent<Health>().TakeDamage(1);
        if (collision.tag == "Boss")
            collision.GetComponent<BossHealth>().TakeDamage(1);

        Instantiate(inpactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void Disable()
    {
        Destroy(gameObject);
    }
}
