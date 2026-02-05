using UnityEngine;

public class RotateCenter : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public bool enableRotation = true;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enableRotation) return;

        rb.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
