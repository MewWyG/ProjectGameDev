using UnityEngine;
using UnityEngine.UI; // สำหรับใช้งาน UI

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;         // เลือดสูงสุดของผู้เล่น
    private int currentHealth;          // เลือดปัจจุบันของผู้เล่น

    public Slider heartBar;             // อ้างอิงถึง UI Slider สำหรับ HeartBar

    void Start()
    {
        // ตรวจสอบว่า HeartBar ถูกตั้งค่าใน Inspector หรือไม่
        if (heartBar == null)
        {
            // ถ้าไม่ถูกตั้งค่า ค้นหา HeartBar จาก Scene โดยอัตโนมัติ
            heartBar = FindObjectOfType<Slider>(); // ค้นหา Slider ใน Scene
            if (heartBar == null)
            {
                Debug.LogError("HeartBar is not assigned and no Slider was found in the scene!");
                return;
            }
        }

        // ตั้งค่าเลือดเริ่มต้น
        currentHealth = maxHealth;

        // ตั้งค่า Slider
        heartBar.maxValue = maxHealth;
        heartBar.value = currentHealth;

        // อัพเดต UI
        UpdateHeartBar();
    }

    // ฟังก์ชันที่ถูกเรียกเมื่อผู้เล่นได้รับความเสียหาย
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;    // ลดจำนวนเลือดตามความเสียหายที่ได้รับ
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ไม่ให้เลือดต่ำกว่า 0

        Debug.Log("Player takes " + damage + " damage. Current health: " + currentHealth);

        // อัพเดต HeartBar UI
        UpdateHeartBar();

        // ตรวจสอบว่าผู้เล่นตายหรือไม่
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ฟังก์ชันสำหรับอัพเดต UI ของ HeartBar
    void UpdateHeartBar()
    {
        if (heartBar != null)
        {
            // ตั้งค่า value ของ Slider ตามค่าเลือดปัจจุบัน
            heartBar.value = currentHealth;
        }
    }

    // ฟังก์ชันสำหรับจัดการกรณีผู้เล่นตาย
    void Die()
    {
        Debug.Log("Player has died!");
        // อาจเพิ่มฟังก์ชันการ restart level หรือแสดงหน้าจอ game over
    }
}
