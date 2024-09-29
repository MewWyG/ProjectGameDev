using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public float speed = 3f;          // ความเร็วของลูกพลังงาน
    public int damage = 20;           // ความเสียหายที่ลูกพลังงานจะทำต่อผู้เล่น
    
    private Rigidbody rb;

    void Start()
    {
        // ดึง Component Rigidbody มาเก็บไว้
        rb = GetComponent<Rigidbody>();

        // ทำให้ลูกพลังงานพุ่งไปข้างหน้า
        rb.velocity = transform.forward * speed;
    }

    // ฟังก์ชันตรวจจับการชนกับ Collider
    void OnCollisionEnter(Collision collision)
    {
        // ตรวจสอบว่าชนกับวัตถุที่มี tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // ดึง PlayerHealth ของผู้เล่น
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            // ถ้ามี component PlayerHealth ให้ทำการสร้างความเสียหาย
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);  // ทำดาเมจต่อผู้เล่น
            }

            // ทำลายลูกพลังงานเมื่อชนกับผู้เล่น
            Destroy(gameObject);
        }
        else
        {
            // ทำลายลูกพลังงานเมื่อชนกับวัตถุอื่นๆ เช่นกำแพงหรือสิ่งกีดขวาง
            Destroy(gameObject);
        }
    }
}
