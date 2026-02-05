using UnityEngine;

public class OrbitateBehavior : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        // get parent transform as target if none assigned
        if (target == null && transform.parent != null)
        {
            target = transform.parent;
        }   
    }

    void Update()
    {
        if (target != null)
        {
            Debug.DrawLine(transform.position, target.position, Color.green);

            transform.RotateAround(target.position, Vector3.forward, 20 * Time.deltaTime);
        }
    }
}
