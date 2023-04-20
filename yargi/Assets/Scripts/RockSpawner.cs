using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] GameObject rockPrefab;
    private float zSpawn = 10f;
    private float minXSpawn = -5f;
    private float maxXSpawn = 5f;

    private float time;
    private float spawnTimer = 3f;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= spawnTimer)
        {
            spawnRock();
            time = 0f;
        }
    }

    void spawnRock()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(minXSpawn,maxXSpawn), 1.5f, zSpawn);
        GameObject rockObject = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);

        Rigidbody rbRock = rockObject.GetComponent<Rigidbody>();

        rbRock.velocity = new Vector3(0f, 0f, -10f);
    }

}
