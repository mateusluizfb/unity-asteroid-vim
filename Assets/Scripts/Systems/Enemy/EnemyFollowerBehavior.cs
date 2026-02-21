using UnityEngine;

public enum EnemyFollowerState
{
    Idle,
    FollowTarget,
}

public class EnemyFollowerBehavior : MonoBehaviour
{
    public EnemyFollowerState currentState;
    public GameObject target;

    [SerializeField] private int followSpeed = 2;
    [SerializeField] private int detectionRange = 4;


    void Start()
    {
        currentState = EnemyFollowerState.Idle;
    }

    void Update()
    {
        if (!target) return;

        switch (currentState)
        {
            case EnemyFollowerState.FollowTarget: FollowTarget(); break;
        }

        transform.rotation = FaceTargetService.GetSmoothRotation(transform, target.transform);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player") return;

        currentState = EnemyFollowerState.FollowTarget;
        target = collision.gameObject;
    }

    private void FollowTarget()
    {
        Vector2 targetPosition = target.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
