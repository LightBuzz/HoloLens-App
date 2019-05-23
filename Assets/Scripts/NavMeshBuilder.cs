using HoloToolkit.Unity.SpatialMapping;
using System;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBuilder : MonoBehaviour
{
    [SerializeField] private SpatialMappingObserver spatialMappingObserver;

    [SerializeField] private NavMeshSurface navMeshSurface;

    ///<summary>Time to wait before building the navigation mesh.</summary>
    private const float waitToBuildNavigation = 0.5f;
    ///<summary>Timestamp to build the navigation mesh.</summary>
    private long buildNavMeshAt = long.MaxValue;

    private void Awake()
    {
        // Add listeners for mesh changes.
        spatialMappingObserver.SurfaceAdded += OnSurfaceAdded;
        spatialMappingObserver.SurfaceUpdated += OnSurfaceUpdated;
        spatialMappingObserver.SurfaceRemoved += OnSurfaceRemoved;
    }

    private void Start()
    {
        ScheduleNavMeshUpdate();
    }

    private void Update()
    {
        if (DateTime.UtcNow.Ticks > buildNavMeshAt)
            UpdateNavigationMesh();
    }

    private void OnDestroy()
    {
        spatialMappingObserver.SurfaceAdded -= OnSurfaceAdded;
        spatialMappingObserver.SurfaceUpdated -= OnSurfaceUpdated;
        spatialMappingObserver.SurfaceRemoved -= OnSurfaceRemoved;
    }

    /// <summary>
    /// Creates the navigation mesh.
    /// </summary>
    private void UpdateNavigationMesh()
    {
        navMeshSurface.buildHeightMesh = true;
        navMeshSurface.BuildNavMesh();

        buildNavMeshAt = long.MaxValue;

        //Logger.Info("Navigation built");
    }

    private void OnSurfaceAdded(object sender, DataEventArgs<SpatialMappingSource.SurfaceObject> e)
    {
        ScheduleNavMeshUpdate();
    }

    private void OnSurfaceUpdated(object sender, DataEventArgs<SpatialMappingSource.SurfaceUpdate> e)
    {
        ScheduleNavMeshUpdate();
    }

    private void OnSurfaceRemoved(object sender, DataEventArgs<SpatialMappingSource.SurfaceObject> e)
    {
        ScheduleNavMeshUpdate();
    }

    /// <summary>
    /// Schedules a navigation mesh rebuild. It doesn't update it instaly in order to avoid multiple invocations.
    /// </summary>
    private void ScheduleNavMeshUpdate()
    {
        // Avoid multiple updates
        if (waitToBuildNavigation < DateTime.UtcNow.Ticks)
            buildNavMeshAt = DateTime.UtcNow.Ticks + TimeSpan.FromSeconds(waitToBuildNavigation).Ticks;
    }
}