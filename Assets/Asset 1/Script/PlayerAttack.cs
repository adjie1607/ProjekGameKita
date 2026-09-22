using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;

    public Collider attackCollider;

    public float attackCooldown = 0.5f;

    private bool canAttack = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
        }
    }

    void Attack()
    {
        canAttack = false;

        anim.SetTrigger("Attack");

        Invoke(nameof(ResetAttack), attackCooldown);
    }

    void ResetAttack()
    {
        canAttack = true;
    }

    // Animation Event
    public void EnableHitbox()
    {
        attackCollider.enabled = true;
    }

    // Animation Event
    public void DisableHitbox()
    {
        attackCollider.enabled = false;
    }
}