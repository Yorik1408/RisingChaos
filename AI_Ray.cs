using UnityEngine;
using UnityEngine.AI;

public class AI_Ray : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent navMeshAgent;

    // Патрульные точки для монстра
    public Transform[] patrolPoints;
    private int currentPatrolIndex;
    private int patrolLoopCount = 0;
    public int maxPatrolLoops = 1; // Максимальное количество обходов по точкам патрулирования

    // Радиус преследования
    public float chaseRadius = 15f;

    // Скорость в разных состояниях
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;

    // Состояния монстра
    private enum State { Patrolling, Chasing }
    private State currentState = State.Patrolling;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (patrolPoints.Length > 0)
        {
            currentPatrolIndex = 0;
            navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrolling:
                Patrolling();
                if (distanceToPlayer <= chaseRadius)
                {
                    currentState = State.Chasing;
                }
                break;

            case State.Chasing:
                Chasing();
                if (distanceToPlayer > chaseRadius)
                {
                    currentState = State.Patrolling;
                    navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
                }
                break;
        }
    }

    void Patrolling()
    {
        navMeshAgent.speed = patrolSpeed;
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            if (currentPatrolIndex == 0) // Если достигнута последняя точка патрулирования
            {
                patrolLoopCount++;
                if (patrolLoopCount >= maxPatrolLoops) // Проверяем, достигнуто ли максимальное количество обходов
                {
                    // Завершаем патрулирование
                    currentState = State.Chasing;
                    navMeshAgent.SetDestination(player.position);
                    return;
                }
            }
            navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void Chasing()
    {
        navMeshAgent.speed = chaseSpeed;
        navMeshAgent.SetDestination(player.position);
    }

    void OnDrawGizmosSelected()
    {
        // Рисуем радиус преследования для удобства настройки
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
