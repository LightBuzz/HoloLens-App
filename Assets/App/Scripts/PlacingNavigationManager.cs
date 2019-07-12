using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.AI;

public class PlacingNavigationManager : PlacingManager
{
    public override void PlaceObject()
    {
        Vector3 positionToPlace;
        Transform camTransform = Camera.main.transform;
        Vector3 lookBack = camTransform.rotation.eulerAngles;
        lookBack.x = lookBack.z = 0f;
        lookBack.y += 180f;

        if (GetLookAtPosition(camTransform.position, camTransform.forward, out positionToPlace))
        {
            NavMeshHit hit;
            float maxDistance = 2f;
            // Get closest position in the navigation mesh.
            if(NavMesh.SamplePosition(positionToPlace, out hit, maxDistance, NavMesh.AllAreas))
            {
                if (InputManager.Instance != null)
                    InputManager.Instance.PopFallbackInputHandler();
                Destroy(gameObject);

                InstantiateObject(hit.position, lookBack);
            }
            else
            {
                Logger.Warning("No position in navigation found");
            }
        }
        else
        {
            // Used for debug
            if (Application.isEditor)
            {
                Logger.Warning("No hit found");
            }
        }
    }
}