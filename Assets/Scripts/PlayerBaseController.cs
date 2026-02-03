using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 150f;
    public float boostMultiplier = 2f;
    private Vector2 moveDirection = Vector2.zero;
    private bool isBoosting = false;

    public ParticleSystem boostParticles;
    private Rigidbody2D rb;

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 inputValue = context.ReadValue<Vector2>();
        
        // Undo normalization to allow for discrete input values
        moveDirection.x = inputValue.x == 0 ? 0 : Mathf.Sign(inputValue.x);
        moveDirection.y = inputValue.y == 0 ? 0 : Mathf.Sign(inputValue.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isBoosting = true;
            if (boostParticles != null)
                boostParticles.Play();
        }
        else if (context.canceled)
        {
            isBoosting = false;
            if (boostParticles != null)
                boostParticles.Stop();
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            Debug.LogError("PlayerBaseController requires a Rigidbody2D component!");

        if (boostParticles != null)
            boostParticles.Stop();
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        float currentSpeed = isBoosting ? speed * boostMultiplier : speed;

        // Move forward/backward
        Vector2 movement = transform.up * moveDirection.y * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
        
        // Rotate
        float rotation = -moveDirection.x * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotation);
    }
}
