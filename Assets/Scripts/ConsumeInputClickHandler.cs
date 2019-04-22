using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class ConsumeInputClickHandler : MonoBehaviour, IInputClickHandler
{
    public delegate void OnInputClickedDelegate(InputClickedEventData eventData);
    public event OnInputClickedDelegate OnInputClickedEvent;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if(OnInputClickedEvent != null)
            OnInputClickedEvent.Invoke(eventData);

        eventData.Use();
    }
}