using UnityEngine;

public static class FaceTargetService
{

  public static Quaternion GetRotation(Transform transform, Transform target)
  {
    if (target == null) return transform.rotation;

    Vector3 direction = (target.position - transform.position).normalized;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    Debug.DrawLine(transform.position, target.position, Color.green);

    Debug.Log("Rotation angle: " + angle);

    return Quaternion.Euler(new Vector3(0, 0, angle - 90f));
  }

  public static Quaternion GetSmoothRotation(Transform transform, Transform target, float rate = 2f)
  {
    if (target == null) return transform.rotation;

    Vector3 direction = (target.position - transform.position).normalized;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    Debug.DrawLine(transform.position, target.position, Color.green);

    Debug.Log("Rotation angle: " + angle);

    Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

    return Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
  }
}
