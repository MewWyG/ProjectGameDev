using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;    // Prefab ของศัตรูที่จะ spawn
    public Transform[] spawnPoints;   // จุดที่ศัตรูจะ spawn
    public float spawnInterval = 1f;  // ระยะเวลาห่างกันระหว่างการ spawn แต่ละครั้ง

    void Start()
    {
        // เริ่มการ spawn ศัตรู
        StartCoroutine(SpawnEnemyContinuously());
    }

    // Coroutine สำหรับการ spawn ศัตรูอย่างต่อเนื่อง
    IEnumerator SpawnEnemyContinuously()
    {
        while (true)
        {
            SpawnEnemy();  // เรียกใช้ฟังก์ชันสำหรับการ spawn ศัตรู
            yield return new WaitForSeconds(spawnInterval);  // รอเวลาตามที่กำหนด
        }
    }

    // ฟังก์ชันสำหรับการ spawn ศัตรู
    void SpawnEnemy()
    {
        // เลือกจุด spawn แบบสุ่มจาก spawnPoints
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // สร้างศัตรูที่ตำแหน่งของ spawnPoint
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}