using UnityEngine;

public class enemyAI : MonoBehaviour
{
    // Movement speed
    public float speed = 2f;

    void Update()
    {
        // Find the player object in the scene (replace "Player" with your player object's name)
        Transform player = GameObject.Find("Player").transform;

        // Ensure player object is found
        if (player != null)
        { 
            // Calculate direction based on player's current position
            Vector2 direction = (player.position - transform.position).normalized;

            // Move the enemy towards the player
            transform.Translate(direction * speed * Time.deltaTime);
        }

        else
        {   // Check if player object was found
            Debug.LogError("Player object not found!");
        }
    }
}
