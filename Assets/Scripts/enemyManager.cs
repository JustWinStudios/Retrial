using UnityEngine;
using System; // Needed for the Action delegate

public class enemyManager : MonoBehaviour
{
    // Health-related properties
    public int maxHealth = 100;
    public int currentHealth;

    // Movement-related properties
    public float moveSpeed = 2f;
    private Transform player;
    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    // Event to signal enemy death
    public event Action OnEnemyDeath;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void FixedUpdate() // Use FixedUpdate for physics-based updates
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            // Use Rigidbody2D to move the enemy towards the player
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            OnEnemyDeath?.Invoke(); // Trigger the OnEnemyDeath event
            Destroy(gameObject); // Destroy the enemy
        }
    }
}
