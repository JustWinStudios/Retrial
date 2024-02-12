using UnityEngine;
using UnityEngine.UI; // Required for UI elements like the health bar

public class playerManager : MonoBehaviour
{
    // Movement properties
    public float speed = 5f;
    private Rigidbody2D rb;

    // Shooting properties
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;

    // Health properties
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthBar; // Assign a UI Slider in the Inspector for the health bar

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        rb.velocity = movement * speed;
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shootingDirection = (mousePosition - (Vector2)transform.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.velocity = shootingDirection * projectileSpeed;
        }
    }

    // Method to update the health UI
    void UpdateHealthUI()
    {
        if (healthBar != null) // Check if the health bar slider is assigned
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }

    // Method for the player to take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            // Player defeat logic here, such as triggering a game over screen
            Debug.Log("Player defeated");
        }
    }
}
