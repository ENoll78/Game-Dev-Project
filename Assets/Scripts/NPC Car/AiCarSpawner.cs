using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AiCarSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] carAIPrefabs;
    [SerializeField] private float[] lanePositions;
    GameObject[] carAIPool = new GameObject[20];

    Transform playerCarTransform;
    WaitForSeconds wait = new WaitForSeconds(0.5f);
    float timeLastCarSpawned = 0;

    // Checking spawn positions
    [SerializeField]
    LayerMask otherCarsLayerMask;

    Collider[] overlappedCheckCollider = new Collider[1];

    // Start is called before the first frame update
    void Start()
    {
        playerCarTransform = GameObject.FindGameObjectWithTag("Player").transform;

        int prefabIndex = 0;

        for (int i = 0; i < carAIPool.Length; i++)
        {
            carAIPool[i] = Instantiate(carAIPrefabs[prefabIndex], transform);
            
            Debug.Log($"Instantiated NPC car at position: {carAIPool[i].transform.position}");

            carAIPool[i].transform.position = new Vector3(carAIPool[i].transform.position.x, carAIPool[i].transform.position.y, -0.1f);
            carAIPool[i].SetActive(false);
            prefabIndex = (prefabIndex + 1) % carAIPrefabs.Length;
        }

        StartCoroutine(UpdateLessOftenCO());
    }

    IEnumerator UpdateLessOftenCO()
    {
        while (true)
        {
            CleanUpCarsBeyondView();
            SpawnNewCars();
            
            yield return wait;
        }
    }

    void SpawnNewCars()
    {
        if (Time.time - timeLastCarSpawned < 2)
            return;

        GameObject carToSpawn = null;

        foreach (GameObject aiCar in carAIPool)
        {
            if (aiCar.activeInHierarchy)
                continue;
        
            carToSpawn = aiCar;
            break;
        }

        if (carToSpawn == null)
            return;

        int chosenLane = UnityEngine.Random.Range(0, lanePositions.Length);
        bool isOppositeDirection = chosenLane <= 1; 
        Vector3 spawnPosition = new Vector3(lanePositions[chosenLane], playerCarTransform.transform.position.y + 50, -0.1f);

        // check spawn location
        if (Physics.OverlapBoxNonAlloc(spawnPosition, Vector3.one * 2, overlappedCheckCollider, Quaternion.identity, otherCarsLayerMask) > 0)
            return;

        carToSpawn.transform.position = spawnPosition;
        NPCar npcCarComponent = carToSpawn.GetComponent<NPCar>();
        
        if (npcCarComponent != null)
        {
            npcCarComponent.SetInitialLane(lanePositions[chosenLane], isOppositeDirection);
        }
        carToSpawn.SetActive(true);
        carToSpawn.transform.position = new Vector3(carToSpawn.transform.position.x, carToSpawn.transform.position.y, -0.1f);
        Debug.Log($"Spawned NPC car at position: {carToSpawn.transform.position}");

        timeLastCarSpawned = Time.time;
    }

    void CleanUpCarsBeyondView()
    {
        foreach (GameObject aiCar in carAIPool)
        {
            if (!aiCar.activeInHierarchy)
                continue;

            if (aiCar.transform.position.y - playerCarTransform.position.y < -50)
                aiCar.SetActive(false);
        }
    }

}
