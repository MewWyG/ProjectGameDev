using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;     // Prefab ของศัตรูที่จะ spawn
    public Transform[] spawnPoints;    // จุดที่ศัตรูจะ spawn
    public float spawnInterval = 1f;   // ระยะเวลาห่างกันระหว่างการ spawn แต่ละครั้ง
    public int maxEnemies = 10;        // จำนวนศัตรูสูงสุดที่จะ spawn ได้

    private int currentEnemyCount = 0; // จำนวนศัตรูที่ถูก spawn ในปัจจุบัน

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
            // ตรวจสอบว่า spawn ศัตรูถึงจำนวนที่กำหนดหรือยัง
            if (currentEnemyCount < maxEnemies)
            {
                SpawnEnemy();  // เรียกใช้ฟังก์ชันสำหรับการ spawn ศัตรู
            }
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

        // เพิ่มจำนวนศัตรูที่ถูก spawn
        currentEnemyCount++;
    }

    // ฟังก์ชันลดจำนวนศัตรูเมื่อศัตรูถูกทำลาย
    public void EnemyDestroyed()
    {
        currentEnemyCount--;
    }
}
