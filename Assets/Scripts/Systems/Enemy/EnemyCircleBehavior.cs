using UnityEngine;

public enum EnemyCircleState
{
    Idle,
    FollowTarget,
    CircleTarget,
    CirclingTarget,
    AttackTarget
}

public class EnemyCircleBehavior : MonoBehaviour
{
    public EnemyCircleState currentState;
    public GameObject target;

    [SerializeField] float rotationSpeed = 200;
    [SerializeField] float rotationDeg = 180;

    private float rotationAmount;
    private float attackWaitTime;
    private float attackTimer;

    void Start()
    {
        attackWaitTime = 1;
        currentState = EnemyCircleState.Idle;

        ResetStateMutation();
    }

    void Update()
    {
        if (!target) return;

        switch (currentState)
        {
            case EnemyCircleState.CircleTarget: HandleCircleTarget(); break;
            case EnemyCircleState.CirclingTarget: StartTargetCircling(); break;
            case EnemyCircleState.AttackTarget: AttackTarget(); break;
            case EnemyCircleState.FollowTarget: FollowTarget(); break;
        }

        transform.rotation = FaceTargetService.GetSmoothRotation(transform, target.transform);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Enemy collision detected with: " + collision.gameObject.name);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player") return;

        currentState = EnemyCircleState.FollowTarget;
        target = collision.gameObject;
    }

    private void ResetStateMutation()
    {
        rotationAmount = 0;
        attackTimer = 0;
    }

    private void HandleCircleTarget()
    {
        ResetStateMutation();
        currentState = EnemyCircleState.CirclingTarget;
    }
    private void StartTargetCircling()
    {
        float speed = rotationSpeed * Time.deltaTime;
        transform.RotateAround(target.transform.position, Vector3.forward, speed);
        rotationAmount += speed;

        if (rotationAmount >= rotationDeg)
        {
            currentState = EnemyCircleState.AttackTarget;
        }
    }

    private void AttackTarget()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackWaitTime)
        {
            currentState = EnemyCircleState.FollowTarget;
        }
    }

    private void FollowTarget()
    {
        if (Vector2.Distance(transform.position, target.transform.position) <= 5)
        {
            currentState = EnemyCircleState.CircleTarget;
            return;
        }

        Vector2 targetPosition = target.transform.position;
        int speed = 3;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
