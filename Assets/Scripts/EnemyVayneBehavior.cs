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
    
    private float attackTimer;
    private float rolledDistance;
    private Vector2 rollTarget;

    [SerializeField] private float attackWaitTime = 2;

    [SerializeField] private int rollDistance = 3;
    [SerializeField] private int rollSpeed = 5;
    [SerializeField] private int followSpeed = 3;
    [SerializeField] private int detectionRange = 4;


    void Start()
    {
        currentState = EnemyVayneState.Idle;

        ResetStateMutation();
    }

    void Update()
    {
        if (!target) return;

        switch (currentState)
        {
            case EnemyVayneState.AttackTarget: AttackTarget(); break;
            case EnemyVayneState.Rolling: PerformRolling(); break;
            case EnemyVayneState.StartRoll: StartRoll(); break;
            case EnemyVayneState.FollowTarget: FollowTarget(); break;
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

    private void AttackTarget()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackWaitTime)
        {
            currentState = EnemyVayneState.FollowTarget;
            ResetStateMutation();
        }
    }
    
    private void PerformRolling()
    {
        transform.position = Vector2.MoveTowards(transform.position, rollTarget, rollSpeed * Time.deltaTime);

        rolledDistance = rolledDistance + (rollSpeed * Time.deltaTime);

        if (rolledDistance >= rollDistance)
        {
            currentState = EnemyVayneState.AttackTarget;
        }
    }
    
    private void StartRoll()
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

        Vector2 rollDirection = new Vector2(Mathf.Cos(rollRadians), Mathf.Sin(rollRadians));
        rollTarget = (Vector2)transform.position + rollDirection * rollDistance;
        
        currentState = EnemyVayneState.Rolling;
    }

    private void FollowTarget()
    {
        if (Vector2.Distance(transform.position, target.transform.position) <= detectionRange)
        {
            currentState = EnemyVayneState.StartRoll;
            return;
        }

        Vector2 targetPosition = target.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
