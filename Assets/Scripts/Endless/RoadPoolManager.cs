using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPoolManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] roadPrefabs;

    GameObject[] roadPool = new GameObject[20];
    GameObject[] sections = new GameObject[20];
    Transform playerCarTransform;
    WaitForSeconds waitFor100ms = new WaitForSeconds(0.1f);
    const float sectionLength = 40;

    void Start()
    {
        playerCarTransform = GameObject.FindGameObjectWithTag("Player").transform;
        int prefabIndex = 0;

        for (int i = 0; i < roadPool.Length; i++)
        {
            roadPool[i] = Instantiate(roadPrefabs[prefabIndex]);
            roadPool[i].SetActive(false);
            if (++prefabIndex >= roadPrefabs.Length)
                prefabIndex = 0;
        }

        // Starting sections
        for (int i = 0; i < sections.Length; i++)
        {
            GameObject randomSection = GetRandomSectionFromPool();
            randomSection.transform.position = new Vector3(0, i * sectionLength, 1); // Standardized X position
            randomSection.SetActive(true);
            sections[i] = randomSection;
        }

        StartCoroutine(UpdateLessOftenCO());
    }

    IEnumerator UpdateLessOftenCO()
    {
        while (true)
        {
            UpdateRoadPositions();
            yield return waitFor100ms;
        }
    }

    void UpdateRoadPositions()
    {
        for (int i = 0; i < sections.Length; i++)
        {
            if (playerCarTransform.position.y - sections[i].transform.position.y > sectionLength)
            {
                sections[i].SetActive(false);
                sections[i] = GetRandomSectionFromPool();
                sections[i].transform.position = new Vector3(0, sections[i].transform.position.y + sectionLength * sections.Length, 1);
                sections[i].SetActive(true);
            }
        }
    }

    GameObject GetRandomSectionFromPool()
    {
        int randomIndex = Random.Range(0, roadPool.Length);
        while (roadPool[randomIndex].activeInHierarchy)
        {
            if (++randomIndex >= roadPool.Length)
                randomIndex = 0;
        }
        return roadPool[randomIndex];
    }
}
