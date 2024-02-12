using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    [SerializeField] private Slider healthBar; // Now visible in the Inspector due to SerializeField
    [SerializeField] private CanvasGroup healthBarCanvasGroup; // Now visible in the Inspector due to SerializeField
    private bool isHealthBarVisible = false;
    private Coroutine fadeCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        UpdateHealthUI();
        healthBarCanvasGroup.alpha = 0; // Initially hide the health bar
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

    void UpdateHealthUI()
    {
        if (healthBar != null) // Check if the health bar slider is assigned
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            // Player defeat logic here
            Debug.Log("Player defeated");
        }

        // Show health bar when taking damage
        if (!isHealthBarVisible)
        {
            isHealthBarVisible = true;
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine); // Stop the ongoing fade coroutine if any
            }
            fadeCoroutine = StartCoroutine(FadeHealthBar(1, 0.5f)); // Fade in quickly
        }
    }

    IEnumerator FadeHealthBar(float targetAlpha, float duration)
    {
        float startAlpha = healthBarCanvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            healthBarCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        healthBarCanvasGroup.alpha = targetAlpha; // Ensure the target alpha is set

        if (targetAlpha == 1)
        {
            // Wait for a moment before starting to fade out
            yield return new WaitForSeconds(2); // Adjust the wait time as needed
            fadeCoroutine = StartCoroutine(FadeHealthBar(0, 1.5f)); // Fade out slowly
        }
        else
        {
            isHealthBarVisible = false;
        }
    }
}
