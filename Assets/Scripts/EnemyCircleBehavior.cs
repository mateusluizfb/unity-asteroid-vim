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

    private float rotationAmount;
    private float attackWaitTime;
    private float attackTimer;

    void Start()
    {
        attackWaitTime = 1;
        attackTimer = 0;
        currentState = EnemyCircleState.Idle;
    }

    void Update()
    {
        if (!target) return;

        if (currentState == EnemyCircleState.CircleTarget)
        {
            rotationAmount = 0;
            attackTimer = 0;
            currentState = EnemyCircleState.CirclingTarget;
            return;
        }

        if (currentState == EnemyCircleState.CirclingTarget)
        {
            float speed = 200 * Time.deltaTime;
            transform.RotateAround(target.transform.position, Vector3.forward, speed);
            rotationAmount += speed;

            if (rotationAmount >= 180)
            {
                currentState = EnemyCircleState.AttackTarget;
                return;
            }
        }

        if (currentState == EnemyCircleState.AttackTarget)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackWaitTime)
            {
                currentState = EnemyCircleState.FollowTarget;
            }

            return;
        }

        if (currentState != EnemyCircleState.CirclingTarget &&
            currentState != EnemyCircleState.CircleTarget &&
            Vector2.Distance(transform.position, target.transform.position) <= 5)
        {
            currentState = EnemyCircleState.CircleTarget;
            return;
        }

        if (currentState == EnemyCircleState.FollowTarget)
        {
            Vector2 targetPosition = target.transform.position;
            int speed = 3;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
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
}
