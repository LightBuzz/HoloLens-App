using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.AI;

public class CodemanAgent : MonoBehaviour, IInputClickHandler
{
    private NavMeshAgent agent;
    private Animator animator;

    private readonly int animatorForward = Animator.StringToHash("Forward");
    private readonly int animatorTurn = Animator.StringToHash("Turn");
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    private void Start()
    {
        InputManager.Instance.PushFallbackInputHandler(gameObject);
    }

    private void Update()
    {
        UpdateAnimator();
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Vector3 hitPoint;
        Transform camTransform = Camera.main.transform;
        if (PlacingManager.GetLookAtPosition(camTransform.position, camTransform.forward, out hitPoint))
        {
            agent.destination = hitPoint;
            agent.isStopped = false;
        }
        else
        {
            Logger.Warning("Could not set destination");
        }
    }

    private void UpdateAnimator()
    {
        animator.SetFloat(animatorForward, agent.desiredVelocity.magnitude / agent.speed);
    }

    private void OnAnimatorMove()
    {
        transform.position = agent.nextPosition;

        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(agent.velocity.x, 0f, agent.velocity.z));
        }
    }
}