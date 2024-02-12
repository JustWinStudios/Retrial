using UnityEngine;

public class enemyManager : MonoBehaviour
{
    // Health-related properties
    public int maxHealth = 100;
    public int currentHealth;

    // Movement-related properties
    public float moveSpeed = 2f;
    private Transform player;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag, ensure your player GameObject is tagged as "Player"
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        // Ensure the player object is found
        if (player != null)
        {
            // Calculate direction based on player's current position
            Vector2 direction = (player.position - transform.position).normalized;

            // Move the enemy towards the player
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
        else
        {   // Log an error if the player object was not found (helpful for debugging)
            Debug.LogError("Player object not found!");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Destroy the enemy if health is depleted
        }
    }
}
