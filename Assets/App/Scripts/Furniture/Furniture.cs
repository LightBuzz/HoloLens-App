using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    private FurnitureIndicator indicator;

    private HashSet<string> collisions;

    private bool showIndicator = true;

    private const int uiLayer = 5;

    private void Awake()
    {
        indicator = GetComponentInChildren<FurnitureIndicator>();

        collisions = new HashSet<string>();
    }

    private void Start()
    {
        ShowIndicator(true);
    }

    /// <summary>
    /// Adds collision from a colliding object.
    /// </summary>
    /// <param name="objName"></param>
    public void AddCollision(string objName)
    {
        collisions.Add(objName);

        UpdateIndicator();
    }

    /// <summary>
    /// Removes collision from a colliding object.
    /// </summary>
    /// <param name="wallName"></param>
    public void RemoveCollision(string wallName)
    {
        collisions.Remove(wallName);

        UpdateIndicator();
    }

    private void OnTriggerEnter(Collider other)
    {
        // UI layer
        if (other.gameObject.layer == uiLayer)
            return;

        //Debug.Log("collision with " + other.name + " in layer " + other.gameObject.layer);

        AddCollision(other.name);
        // the other object will update this
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == uiLayer)
            return;

        //Debug.Log("exitted collision with " + other.name + " in layer " + other.gameObject.layer);

        RemoveCollision(other.name);
    }

    public void Place()
    {
        SetLayerRecursively(LayerMask.NameToLayer("Furniture"));

        Debug.Log("Placed " + gameObject.name);
    }

    private void SetLayerRecursively(int layerIndex)
    {
        gameObject.layer = layerIndex;

        Transform[] transforms = gameObject.GetComponentsInChildren<Transform>(true);

        for (int i = 0; i < transforms.Length; i++)
            transforms[i].gameObject.layer = layerIndex;
    }

    public void ShowIndicator(bool show)
    {
        showIndicator = show;

        UpdateIndicator();
    }

    private void UpdateIndicator()
    {
        if (showIndicator)
        {
            if (collisions.Count > 0)
                indicator.ShowCollision();
            else
                indicator.ShowPlacement();
        }
        else
        {
            indicator.Hide();
        }
    }

    public bool IsColliding()
    {
        return collisions.Count > 0;
    }
}