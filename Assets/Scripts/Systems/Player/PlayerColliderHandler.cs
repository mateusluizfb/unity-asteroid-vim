using UnityEngine;

public class PlayerColliderHandler : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
    }
}
