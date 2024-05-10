using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class ResourceSpawner : MonoBehaviour
{
    public GameObject PrefabToSpawn;
    public float maxResourceCount;
    public float baseSpawnRate;
    public float spawnRateRandomizer;
    public string nameOfResourceQueue;
    public float heightOffset;
    
    private GameObject navMeshSurfaceObject;
    private NavMeshSurface navMeshSurface;
    private BoxCollider spawnArea;
    private Bounds bounds;
    // Start is called before the first frame update
    void Start()
    {
        spawnArea = GetComponent<BoxCollider>();
        navMeshSurfaceObject = GameObject.FindGameObjectWithTag("NavMeshSurface");
        navMeshSurface = navMeshSurfaceObject.GetComponent<NavMeshSurface>();
        bounds = spawnArea.bounds;
        // topRight = new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y, bounds.center.z + bounds.extents.z);
        // bottomLeft = new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y, bounds.center.z - bounds.extents.z);
        // topRight = new Vector3(bounds.max.x, bounds.center.y, bounds.max.z);
        // bottomLeft = new Vector3(bounds.min.x, bounds.center.y, bounds.min.z);
        StartCoroutine(SpawnResource());
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private IEnumerator SpawnResource(){
        if(PrefabToSpawn == null){
            Debug.Log("Prefab is null");
            yield break;
        }
        while(true){
            yield return new WaitForSeconds(baseSpawnRate + Random.Range(-spawnRateRandomizer, spawnRateRandomizer));
            if(GWorld.Instance.GetQueue(nameOfResourceQueue).Queue.Count < maxResourceCount){
                Vector3 spawnPoint = RandomPoint();
                NavMeshHit hit;
                if(NavMesh.SamplePosition(spawnPoint, out hit, 1.0f, NavMesh.AllAreas)){
                    spawnPoint = hit.position;
                    spawnPoint.y += heightOffset;
                    GameObject spawnedResource = Instantiate(PrefabToSpawn, spawnPoint, Quaternion.identity);
                    spawnedResource.transform.parent = navMeshSurfaceObject.transform;
                    GWorld.Instance.GetQueue(nameOfResourceQueue).AddResource(spawnedResource);
                    navMeshSurface.BuildNavMesh();
                }
                
            }
            
            
        }
    }
    
    private Vector3 RandomPoint(){
        Vector3 randomPoint = new Vector3(Random.Range(bounds.min.x, bounds.max.x), bounds.center.y, Random.Range(bounds.min.z, bounds.max.z));
        return randomPoint;
    }
}
