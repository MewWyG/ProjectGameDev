using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public float speed = 3f;          // ความเร็วของลูกพลังงาน
    
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
        // เมื่อชนกับสิ่งกีดขวางใด ๆ จะทำลายลูกพลังงาน
        Destroy(gameObject);
    }
}
