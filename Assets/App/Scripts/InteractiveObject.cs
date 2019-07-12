using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class InteractiveObject : MonoBehaviour, IFocusable, IInputClickHandler
{
    private bool hasFocus = false;
    private float rotationSpeed = 2f;
    private float scaleModifier = 1.025f;

    private void Update()
    {
        if (hasFocus)
        {
            transform.Rotate(new Vector3(0f, rotationSpeed, 0f), Space.World);
        }
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
        Vector3 currentScale = transform.localScale;
        currentScale *= scaleModifier;
        transform.localScale = currentScale;
    }
}
