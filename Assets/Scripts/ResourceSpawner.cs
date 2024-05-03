using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public GameObject PrefabToSpawn;
    private GameObject navMeshSurfaceObject;
    private NavMeshSurface navMeshSurface;
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshSurfaceObject = GameObject.FindGameObjectWithTag("NavMeshSurface");
        navMeshSurface = navMeshSurfaceObject.GetComponent<NavMeshSurface>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
