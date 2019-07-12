using HoloToolkit.Unity.InputModule;
using UnityEngine;

[RequireComponent(typeof(ConsumeInputClickHandler))]
public class FurnitureInput : MonoBehaviour, IFocusable
{
    private bool isRotating = false;
    private bool holding = false;
    private bool hasFocus = false;
    
    private const float rotationMultiplier = 3000f;

    private ConsumeInputClickHandler consumeInputClickHandler;

    private void Awake()
    {
        consumeInputClickHandler = GetComponent<ConsumeInputClickHandler>();

        consumeInputClickHandler.OnInputClickedEvent += OnInputClicked;
    }

    private void Update()
    {
        if (holding)
        {
            if (isRotating)
            {
                if(hasFocus)
                    transform.Rotate(new Vector3(0f, Time.deltaTime * 50f, 0f), Space.World);
            }
            else
            {
                // placing
                Vector3 positionToPlace;
                Transform camTransform = Camera.main.transform;

                if (PlacingManager.GetLookAtPosition(camTransform.position, camTransform.forward, out positionToPlace))
                    transform.position = positionToPlace;
            }
        }
    }

    public void DestroyFurniture()
    {
        Invoke("DestroyObject", .2f);
    }

    public void OnMoveChanged(bool isOn)
    {
        if (isOn)
            isRotating = false;
    }

    public void OnRotateChanged(bool isOn)
    {
        if (isOn)
            isRotating = true;
    }

    private void DestroyObject()
    {
        CancelInvoke();

        Destroy(gameObject);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        holding = !holding;

        Vector3 result;
        //Debug.Log(eventData.InputSource.TryGetGripPosition(eventData.SourceId, out result));

        eventData.Use();
    }

    public void OnFocusEnter()
    {
        hasFocus = true;
    }

    public void OnFocusExit()
    {
        hasFocus = false;
    }
}
