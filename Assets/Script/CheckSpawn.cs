using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckSpawn : MonoBehaviour
{
    public GameObject gameWinUI; // อ้างอิงถึง UI ที่จะแสดงเมื่อผู้เล่นชนะ
    private GameObject[] enemies; // สำหรับเก็บรายการศัตรูที่มีในแมพ

    void Start()
    {
        if (gameWinUI != null)
        {
        gameWinUI.SetActive(false); // ซ่อน UI ตอนเริ่มเกม
        }
    }


    void Update()
    {
        // ค้นหาทุก object ที่มี tag เป็น "Enemy"
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Debug จำนวนศัตรูที่เหลืออยู่
        Debug.Log("Remaining Enemies: " + enemies.Length);

        // ถ้าไม่มีศัตรูเหลืออยู่ในแมพ
        if (enemies.Length == 0)
        {
            // เรียกฟังก์ชันแสดงหน้าจอชนะ
            GameWin();
        }
    }


    // ฟังก์ชันแสดงหน้าจอชนะ
        void GameWin()
    {
        // เปิดใช้งาน UI ชนะ
        if (gameWinUI != null)
        {
            gameWinUI.SetActive(true);
            Debug.Log("GameWin UI Shown!");
        }
        else
        {
            Debug.Log("gameWinUI is not assigned!");
        }

        // หยุดเกมโดยการหยุดเวลา
        Time.timeScale = 0f;

        // แสดงข้อความในคอนโซล
        Debug.Log("You Win! All enemies are defeated!");
    }
}
