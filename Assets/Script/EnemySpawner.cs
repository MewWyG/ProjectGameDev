using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     // Prefab ของศัตรูที่จะ spawn
    public Transform[] spawnPoints;  // จุดที่ศัตรูจะ spawn
    public float spawnInterval = 1f;  // ระยะเวลาห่างกันระหว่างการ spawn แต่ละครั้ง
    public int maxEnemies = 5;       // จำนวนศัตรูสูงสุด

    private int currentEnemyCount = 0; // จำนวนศัตรูที่ spawn แล้ว

    void Start()
    {
        StartCoroutine(SpawnEnemyContinuously());
    }

    IEnumerator SpawnEnemyContinuously()
    {
        while (true)
        {
            if (currentEnemyCount < maxEnemies) 
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval); 
        }
    }

    void SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        currentEnemyCount++;
    }
}