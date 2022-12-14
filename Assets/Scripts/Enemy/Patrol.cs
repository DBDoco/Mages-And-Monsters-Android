using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform lEdge;
    [SerializeField] private Transform rEdge;
    [SerializeField] private Transform enemy;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float idleDuration;
    [SerializeField] private Animator anim;

    private Vector3 scale;
    private bool movingLeft;
    private float idleTimer;


    private void Awake()
    {
        scale = enemy.localScale;
    }
    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= lEdge.position.x)
                Move(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rEdge.position.x)
                Move(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    private void Move(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);

        enemy.localScale = new Vector3(Mathf.Abs(scale.x) * _direction,
            scale.y, scale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * movementSpeed,
            enemy.position.y, enemy.position.z);
    }
}