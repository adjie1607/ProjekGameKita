using UnityEngine;

public class SimpleNPC : MonoBehaviour
{
    public Transform[] waypoints;

    public float speed = 2f;
    public float chaseSpeed = 4f;

    public float detectRange = 5f;
    public float attackRange = 1.5f;

    public float attackCooldown = 1.5f;
    public int damage = 20;

    private int currentPoint = 0;
    private Transform player;
    private Animator anim;
    private SpriteRenderer sr;

    private bool canAttack = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // ATTACK
        if (distance <= attackRange)
        {
            Attack();
        }

        // CHASE
        else if (distance <= detectRange)
        {
            ChasePlayer();
        }

        // PATROL
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        anim.SetBool("isRunning", true);
        anim.SetBool("isAttacking", false);

        Transform target = waypoints[currentPoint];

        MoveTo(target.position, speed);

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            currentPoint = (currentPoint + 1) % waypoints.Length;
        }
    }

    void ChasePlayer()
    {
        anim.SetBool("isRunning", true);
        anim.SetBool("isAttacking", false);

        MoveTo(player.position, chaseSpeed);
    }

    void Attack()
    {
        anim.SetBool("isRunning", false);

        if (canAttack)
        {
            canAttack = false;

            anim.SetTrigger("Attack");

            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    public void DealDamage()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            PlayerHealth playerHealth =
                player.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    void ResetAttack()
    {
        canAttack = true;
    }

    void MoveTo(Vector3 target, float moveSpeed)
    {
        Vector3 direction = (target - transform.position).normalized;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime
        );

        // Flip sprite
        if (direction.x > 0)
            sr.flipX = false;
        else if (direction.x < 0)
            sr.flipX = true;
    }
}