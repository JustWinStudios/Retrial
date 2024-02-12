using UnityEngine;

public class playerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootingDirection = (mousePosition - (Vector2)transform.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = shootingDirection * projectileSpeed;
    }
}
