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

    // Event to signal enemy death
    public event Action OnEnemyDeath;

    void Start()
    {
        currentHealth = maxHealth;
        // Find the player by tag, ensure your player GameObject is tagged as "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        {
            // Log an error if the player object was not found (helpful for debugging)
            Debug.LogError("Player object not found!");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // Trigger the OnEnemyDeath event before destroying the enemy
            OnEnemyDeath?.Invoke();
            // Destroy the enemy if health is depleted
            Destroy(gameObject);
        }
    }
}
