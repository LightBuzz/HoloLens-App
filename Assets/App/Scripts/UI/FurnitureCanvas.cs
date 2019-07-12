using UnityEngine;

public class FurnitureCanvas : MonoBehaviour
{
    void Update()
    {
        Camera camera = Camera.main;

        Vector3 lookAt = transform.position + camera.transform.rotation * Vector3.forward;
        lookAt.y = transform.position.y;

        transform.LookAt(lookAt);
    }
}
