using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;

    private int currentHealth;

    private Animator anim;

    void Start()
    {
        currentHealth = maxHealth;

        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy kena damage");

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        anim.SetTrigger("Die");

        Destroy(gameObject, 1.5f);
    }
}