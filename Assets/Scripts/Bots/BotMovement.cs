using UnityEngine;
using UnityEngine.AI;

public class BotMovement : MonoBehaviour
{
    public Transform[] waypoints;

    private int currentIndex = 0;
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentIndex].position);
        }
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        if (animator != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentIndex].position);
        }
    }
}