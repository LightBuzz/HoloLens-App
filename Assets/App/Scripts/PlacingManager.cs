using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using UnityEngine;

public class PlacingManager : MonoBehaviour, IInputClickHandler
{
    [SerializeField] private GameObject objectToPlace;

    [SerializeField] private bool lookBackAtInstantiation;

    private void Start()
    {
        // Don't forget to remove it when done
        InputManager.Instance.PushFallbackInputHandler(gameObject);
    }

    /// <summary>
    /// Checks if an object exists in front of the camera and fills the hitPoint with the position.
    /// </summary>
    /// <param name="headPosition"></param>
    /// <param name="gazeDirection"></param>
    /// <param name="hitPoint"></param>
    /// <returns>True if an object exists in front of the camera.</returns>
    public static bool GetLookAtPosition(Vector3 headPosition, Vector3 gazeDirection, out Vector3 hitPoint)
    {
        float maxDistance = 10f;

        if (SpatialMappingManager.Instance == null)
            throw new System.Exception("Missing spatial mapping manager");

        RaycastHit hit;
        if (Physics.Raycast(headPosition, gazeDirection, out hit, maxDistance, SpatialMappingManager.Instance.LayerMask))
        {
            hitPoint = hit.point;
            return true;
        }

        hitPoint = Vector3.zero;
        return false;
    }

    public virtual void PlaceObject()
    {
        Vector3 positionToPlace;
        Transform camTransform = Camera.main.transform;
        Vector3 lookBackRotation = camTransform.rotation.eulerAngles;
        lookBackRotation.x = lookBackRotation.z = 0f;
        if (lookBackAtInstantiation)
            lookBackRotation.y += 180f;

        if (GetLookAtPosition(camTransform.position, camTransform.forward, out positionToPlace))
        {
            InstantiateObject(positionToPlace, lookBackRotation);
        }
        else
        {
            // Used for debug
            if (Application.isEditor)
            {
                Logger.Warning("No hit found");

                positionToPlace = camTransform.position + camTransform.forward * 2f;

                InstantiateObject(positionToPlace, lookBackRotation);
            }
        }
    }

    protected void InstantiateObject(Vector3 position, Vector3 rotation)
    {
        Instantiate(objectToPlace, position, Quaternion.Euler(rotation));
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        PlaceObject();
    }
}