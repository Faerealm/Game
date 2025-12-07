using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class obstacles : MonoBehaviour
{
    
    public float RotateSpeed = 100.0f;
    public Transform[] SpawnPoints;
    public float spawnTime = 2.5f;
    public float destroyTime = 5.0f;
    public GameObject Items ;
    private List<GameObject> spawnedItems = new List<GameObject>();
    void Start()
    {
        InvokeRepeating("Spawn_Obstacle", spawnTime, spawnTime);
        
    }
    void Update()
    {
        foreach (var item in spawnedItems)
        {
            if (item != null)
            {
                item.transform.Rotate(Vector3.up * Time.deltaTime * RotateSpeed, Space.World);
            }
        }
    }
    //…˙≥…’œ∞≠ŒÔ
    void Spawn_Obstacle()
    {
       
        int spawnIndex=Random.Range(0,SpawnPoints.Length);
        GameObject spawnItem = Instantiate(Items, SpawnPoints[spawnIndex].position, SpawnPoints[spawnIndex].rotation);
        spawnedItems.Add(spawnItem);
        Destroy(spawnItem, destroyTime);
      
    }

}
