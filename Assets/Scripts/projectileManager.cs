using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public float lifespan = 10f; // Time after which the projectile will be destroyed
    public int damage = 10; // Damage dealt by the projectile

    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
        Destroy(gameObject, lifespan); // Automatically destroy the projectile after its lifespan
    }

    void Update()
    {
        if (Vector2.Distance(startPosition, transform.position) >= 10f)
        {
            Destroy(gameObject); // Destroy the projectile if it moves 10 units from its starting point
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Ensure your enemy GameObject has the tag "Enemy"
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroy the projectile upon hitting an enemy
        }
    }
}