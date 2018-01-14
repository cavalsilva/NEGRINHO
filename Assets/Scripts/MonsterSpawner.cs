using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    Candle candle;
    PlayerMovement player;

    public GameObject monsterPrefab;

    public float spawnTime;
    float spawnTimer;

    void Start()
    {
        candle = FindObjectOfType<Candle>();
        player = FindObjectOfType<PlayerMovement>();

        spawnTimer = spawnTime;
    }

    void Update()
    {
        float minDistance = candle.radius;
        float maxDistance = minDistance + 5;

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnMonster(minDistance, maxDistance);
            spawnTimer = spawnTime;
        }

    }

    void SpawnMonster(float minDistance, float maxDistance)
    {
        Vector2 randomCircle = Random.insideUnitCircle.normalized * Random.Range(minDistance, maxDistance);
        Vector3 spawnPoint = new Vector3(randomCircle.x, 0.5f, randomCircle.y);
        spawnPoint += player.transform.position;

        GameObject newMonster = Instantiate(monsterPrefab);
        newMonster.transform.position = spawnPoint;
    }
}
