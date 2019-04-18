using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class ChangeAnimation : MonoBehaviour, IFocusable, IInputClickHandler
{
    private bool hasFocus = false;
    private Animator animator;
    private int animatorNextTrigger = Animator.StringToHash("Next");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnFocusEnter()
    {
        hasFocus = true;
    }

    public void OnFocusExit()
    {
        hasFocus = false;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if(hasFocus)
            animator.SetTrigger(animatorNextTrigger);
    }
}
