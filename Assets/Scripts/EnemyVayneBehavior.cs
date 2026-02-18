using UnityEngine;

public enum EnemyVayneState
{
    Idle,
    FollowTarget,
    StartRoll,
    Rolling,
    AttackTarget
}

public class EnemyVayneBehavior : MonoBehaviour
{
    public EnemyVayneState currentState;
    public GameObject target;

    public float attackWaitTime = 2;
    private float attackTimer;

    public int rollDistance = 3;
    private float rolledDistance;
    private Vector2 rollTarget;


    void Start()
    {
        currentState = EnemyVayneState.Idle;

        ResetStateMutation();
    }

    void Update()
    {
        if (!target) return;

        if (currentState == EnemyVayneState.AttackTarget)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackWaitTime)
            {
                currentState = EnemyVayneState.FollowTarget;
                ResetStateMutation();
            }

            return;
        }

        if (currentState == EnemyVayneState.Rolling)
        {
            int speed = 5;
            transform.position = Vector2.MoveTowards(transform.position, rollTarget, speed * Time.deltaTime);

            rolledDistance = rolledDistance + (rollDistance * Time.deltaTime);

            if (rolledDistance >= rollDistance)
            {
                currentState = EnemyVayneState.AttackTarget;
            }

            return;
        }

        if (currentState == EnemyVayneState.StartRoll)
        {
            Vector2 direction = (transform.position - target.transform.position).normalized;
            
            float rollRadians;
            if (direction.x <= 0)
            {
                // if is left side of the target, goes right
                rollRadians = Mathf.Deg2Rad * Random.Range(-25, 26);
            } else
            {
                // if is right side of the target, goes left
                rollRadians = Mathf.Deg2Rad * Random.Range(-205, -156);
            }
            
            Vector2 distanceCoord = new Vector2(transform.position.x + rollDistance, transform.position.y);

            rollTarget = new Vector2(
                (distanceCoord.x * Mathf.Cos(rollRadians)) - (distanceCoord.y * Mathf.Sin(rollRadians)),
                (distanceCoord.x * Mathf.Sin(rollRadians)) + (distanceCoord.y * Mathf.Cos(rollRadians))
            );

            currentState = EnemyVayneState.Rolling;

            return;
        }

        if (Vector2.Distance(transform.position, target.transform.position) <= 4)
        {
            currentState = EnemyVayneState.StartRoll;
            return;
        }

        if (currentState == EnemyVayneState.FollowTarget)
        {
            Vector2 targetPosition = target.transform.position;
            int speed = 3;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player") return;

        currentState = EnemyVayneState.FollowTarget;
        target = collision.gameObject;
    }

    private void ResetStateMutation()
    {
        attackTimer = 0;
        rolledDistance = 0;
    }
}
