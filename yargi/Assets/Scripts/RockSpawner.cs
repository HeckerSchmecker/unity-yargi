using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] GameObject rockPrefab;
    private float zSpawn = 20f;
    private float minXSpawn = -15f;
    private float maxXSpawn = 15f;

    private float minXScale = 3f;
    private float minYScale = 3f;
    private float minZScale = 3f;

    private float maxXScale = 5f;
    private float maxYScale = 3f;
    private float maxZScale = 5f;


    private float time;
    private float spawnTimer;
    private float minSpawnTime = 0.2f;
    private float maxSpawnTime = 3f;


    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        spawnTimer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= spawnTimer)
        {
            spawnRock();
            time = 0f;
            spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    void spawnRock()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(minXSpawn,maxXSpawn), 1.5f, zSpawn);
        GameObject rockObject = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);

        Vector3 randomScale = new Vector3(Random.Range(minXScale, maxXScale), Random.Range(minYScale, maxYScale), Random.Range(minZScale, maxZScale));
        rockObject.transform.localScale = randomScale;
    }

}
