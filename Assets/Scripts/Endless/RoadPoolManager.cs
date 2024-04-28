using System.Collections.Generic;
using System.Collections;
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

        for(int i = 0; i < roadPool.Length; i++)
        {
            roadPool[i] = Instantiate(roadPrefabs[prefabIndex]);
            roadPool[i].SetActive(false);
            prefabIndex++;

            if (prefabIndex > roadPrefabs.Length - 1)
                prefabIndex = 0;
        }

        // Starting sections
        for (int i = 0; i < sections.Length; i++)
        {
                GameObject randomSection = GetRandomSectionFromPool();

                // Move road position and activate
                randomSection.transform.position = new Vector3(roadPool[i].transform.position.x, i * sectionLength, 1);
                randomSection.SetActive(true);

                sections[i] = randomSection;
        }

        StartCoroutine(UpdateLessOftenCO());
    }

    IEnumerator UpdateLessOftenCO()
    {
        while (true)
        {
            yield return waitFor100ms;
        }
    }

    void UpdateRoadPositions()
    {
        for (int i = 0; i <sectionLength; i++)
        {
            if(sections[i].transform.position.y - playerCarTransform.position.y < - sectionLength)
            {
                Vector3 lastSectionPosition = sections[i].transform.position;
                sections[i].SetActive(false);

                sections[i] = GetRandomSectionFromPool();

                sections[i].transform.position = new Vector3(lastSectionPosition.x, lastSectionPosition.y + sectionLength *sections.Length, 1);
                sections[i].SetActive(true);
            }
        }
    }

    GameObject GetRandomSectionFromPool()
    {
        int randomIndex = Random.Range(0, roadPool.Length);

        bool isNewSectionFound = false;

        while(!isNewSectionFound)
        {
            if(!roadPool[randomIndex].activeInHierarchy)
                isNewSectionFound = true;
            else
            {
                randomIndex++;

                //validate
                if(randomIndex > roadPool.Length - 1)
                    randomIndex = 0;
            }
        }
    
    return roadPool[randomIndex];
    }
}