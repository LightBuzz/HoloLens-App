using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.AI;

public class CodemanAgent : MonoBehaviour
{
    ///<summary>Handles the pathfinding for the GameObject.</summary>
    private NavMeshAgent agent;
    ///<summary>Updates animation.</summary>
    private Animator animator;

    private ConsumeInputClickHandler clickHandler;

    private readonly int animatorForward = Animator.StringToHash("Forward");

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        clickHandler = GetComponent<ConsumeInputClickHandler>();
        clickHandler.OnInputClickedEvent += OnClicked;

        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    /// <summary>
    /// Listen to any input that isn't consumed by other scripts.
    /// </summary>
    private void Start()
    {
        InputManager.Instance.PushFallbackInputHandler(gameObject);
    }

    private void Update()
    {
        UpdateAnimatorParameters();
    }

    /// <summary>
    /// Callback for processing animation movements for modifying root motion.
    /// This callback will be invoked at each frame after the state machines and the animations have been evaluated, but before OnAnimatorIK.
    /// </summary>
    private void OnAnimatorMove()
    {
        transform.position = agent.nextPosition;

        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(agent.velocity.x, 0f, agent.velocity.z));
        }
    }

    private void OnDestroy()
    {
        clickHandler.OnInputClickedEvent -= OnClicked;
    }

    public void OnClicked(InputClickedEventData eventData)
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

    /// <summary>
    /// Updates real time animator parameters
    /// </summary>
    private void UpdateAnimatorParameters()
    {
        animator.SetFloat(animatorForward, agent.desiredVelocity.magnitude / agent.speed);
    }
}