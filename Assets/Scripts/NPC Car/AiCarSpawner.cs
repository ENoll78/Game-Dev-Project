using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AiCarSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] carAIPrefabs;

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
            carAIPool[i] = Instantiate(carAIPrefabs[prefabIndex]);
            carAIPool[i].SetActive(false);

            prefabIndex++;

            if (prefabIndex > carAIPrefabs.Length - 1)
                prefabIndex = 0;
        }    
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

        Vector3 spawnPosition = new Vector3(0, playerCarTransform.transform.position.y + 100, 0);

        // check spawn location
        if (Physics.OverlapBoxNonAlloc(spawnPosition, Vector3.one * 2, overlappedCheckCollider, Quaternion.identity, otherCarsLayerMask) > 0)
            return;

        carToSpawn.transform.position = spawnPosition;
        carToSpawn.SetActive(true);

        timeLastCarSpawned = Time.time;
    }

    void CleanUpCarsBeyondView()
    {
        foreach (GameObject aiCar in carAIPool)
        {
            if (!aiCar.activeInHierarchy)
                continue;

            if (aiCar.transform.position.y - playerCarTransform.position.y > 200)
                aiCar.SetActive(false);

            if (aiCar.transform.position.y - playerCarTransform.position.y < -50)
                aiCar.SetActive(false);
        }
    }

}
