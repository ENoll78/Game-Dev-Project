using System.Collections.Generic;
using UnityEngine;

public class RoadPoolManager : MonoBehaviour
{
    public GameObject roadPrefab;
    public int poolSize = 5;
    private Queue<GameObject> roadQueue = new Queue<GameObject>();
    private GameObject lastRoadSection;
    void Start()
    {
        Vector3 startPos = new Vector3(0, 0, 0);
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(roadPrefab, startPos, Quaternion.identity);
            obj.SetActive(false);
            roadQueue.Enqueue(obj);
            lastRoadSection = obj;
            startPos.y -= obj.GetComponent<SpriteRenderer>().bounds.size.y;
        }
    }

    public GameObject GetRoad()
    {
        GameObject obj = (roadQueue.Count > 0) ? roadQueue.Dequeue() : Instantiate(roadPrefab);
        obj.SetActive(true);
        PositionRoad(obj);
        return obj;
    }

private void PositionRoad(GameObject road)
{
    SpriteRenderer spriteRenderer = road.GetComponent<SpriteRenderer>();
    if (spriteRenderer != null)
    {
        if (lastRoadSection != null)
        {
            float roadHeight = spriteRenderer.bounds.size.y;
            road.transform.position = new Vector3(5, lastRoadSection.transform.position.y + roadHeight, 1);
        }
        lastRoadSection = road;
    }
    else
    {
        Debug.LogError("No SpriteRenderer found on the road prefab!");
    }
}

    public void ReturnRoad(GameObject road)
    {
        road.SetActive(false);
        roadQueue.Enqueue(road);
    }
}