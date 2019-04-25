using HoloToolkit.Unity.InputModule;
using UnityEngine;

[RequireComponent(typeof(ConsumeInputClickHandler))]
public class ChangeAnimation : MonoBehaviour
{
    private ConsumeInputClickHandler consumeInputClickHandler;
    private Animator animator;
    private int animatorNextTrigger = Animator.StringToHash("Next");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        consumeInputClickHandler = GetComponent<ConsumeInputClickHandler>();

        consumeInputClickHandler.OnInputClickedEvent += OnClicked;
    }

    private void OnDestroy()
    {
        consumeInputClickHandler.OnInputClickedEvent -= OnClicked;
    }

    private void OnClicked(InputClickedEventData eventData)
    {
        animator.SetTrigger(animatorNextTrigger);

        eventData.Use();

        Logger.Info("new animation");
    }
}
