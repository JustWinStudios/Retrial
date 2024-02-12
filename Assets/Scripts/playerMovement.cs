using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // Movement Speed
    public float speed = 5f;

    // Rigidbody Reference
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get Movement Input (WASD KEYS)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Combine input into a movment vector:
        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        // Apply movement to Rigidbody
        rb.velocity = movement * speed;
    }
}
