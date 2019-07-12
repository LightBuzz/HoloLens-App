using UnityEngine;

public class PlaceFurnitureCanvas : MonoBehaviour
{
    [Tooltip("List of available furniture.")]
    [SerializeField] private FurnitureThumbnailPairList list;

    private static PlaceFurnitureCanvas Singleton;

    private bool removing = false;

    private void Awake()
    {
        if (Singleton != null)
            Destroy(Singleton.gameObject);

        Singleton = this;
    }

    public void Place(int index)
    {
        if (removing) return;

        GameObject go = Instantiate(list.Pairs[index].Model);
        go.transform.position = transform.position;

        CloseCanvas();
    }

    public void CloseCanvas()
    {
        Invoke("DestroyCanvas", 0.2f); // Destroy with delay to avoid spawning another one instantly because of hit in the mesh.

        removing = true;
    }

    private void DestroyCanvas()
    {
        CancelInvoke();

        Destroy(gameObject);
    }
}