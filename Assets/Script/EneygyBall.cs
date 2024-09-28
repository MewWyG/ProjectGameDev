using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public float speed = 3f;  // ความเร็วของลูกพลังงาน
    
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
        // ตรวจสอบว่า tag ของวัตถุที่ชนคือ "Player" หรือ "Object"
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Object"))
        {
            // ทำลายลูกพลังงานเมื่อชนกับวัตถุที่ตรงกับ tag ที่กำหนด
            Destroy(gameObject);
        }
        // ถ้าไม่ใช่ "Player" หรือ "Object" จะปล่อยให้ทะลุไปโดยไม่ทำอะไร
    }
}
