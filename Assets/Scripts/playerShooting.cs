using UnityEngine;

public class playerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public KeyCode fireKey = KeyCode.Space;

    private void Update()
    {
        // Check for input each frame
        if (Input.GetKeyDown(fireKey))
        {
            Debug.Log("Fire key pressed"); // Confirm that the key press is detected

            // Instantiate the projectile at the player's position with no rotation
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Try to get the Rigidbody2D component from the instantiated projectile
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Apply force to the projectile's Rigidbody2D
                rb.AddForce(transform.right * projectileSpeed, ForceMode2D.Impulse);
                Debug.Log("Force applied to projectile");
            }
            else
            {
                Debug.LogError("Projectile prefab does not have a Rigidbody2D component attached.");
            }
        }
    }
}
