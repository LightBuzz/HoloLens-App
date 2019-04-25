using HoloToolkit.Unity.SpatialMapping;
using System;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBuilder : MonoBehaviour
{
    [SerializeField] private SpatialMappingObserver spatialMappingObserver;

    [SerializeField] private NavMeshSurface navMeshSurface;

    private const float waitToBuildNavigation = 0.5f;

    private long buildNavMeshAt = long.MaxValue;

    private void Awake()
    {
        spatialMappingObserver.SurfaceAdded += OnSurfaceAdded;
        spatialMappingObserver.SurfaceUpdated += OnSurfaceUpdated;
        spatialMappingObserver.SurfaceRemoved += OnSurfaceRemoved;
    }

    private void Start()
    {
        ScheduleNavMeshUpdate();
    }

    private void OnDestroy()
    {
        spatialMappingObserver.SurfaceUpdated -= OnSurfaceUpdated;
    }

    private void Update()
    {
        if(DateTime.UtcNow.Ticks > buildNavMeshAt)
            UpdateNavigationMesh();
    }

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

    private void ScheduleNavMeshUpdate()
    {
        // Avoid multiple updates
        if (waitToBuildNavigation < DateTime.UtcNow.Ticks)
            buildNavMeshAt = DateTime.UtcNow.Ticks + TimeSpan.FromSeconds(waitToBuildNavigation).Ticks;
    }

    private void OnSurfaceRemoved(object sender, DataEventArgs<SpatialMappingSource.SurfaceObject> e)
    {
        ScheduleNavMeshUpdate();
    }
}
