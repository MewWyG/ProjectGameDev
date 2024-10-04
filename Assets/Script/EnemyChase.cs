using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;          // ตัวแปรเก็บตำแหน่งของ player
    public float detectionRange = 100f; // ระยะตรวจจับผู้เล่น
    public float speed = 5f;          // ความเร็วของศัตรู
    public float rotationSpeed = 5f;   // ความเร็วในการหมุนของศัตรู

    private Animator animator;
    private bool isMoving = false;

    void Start()
    {
        // ดึง Component Animator มาเก็บไว้ในตัวแปร animator
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        // คำนวณระยะห่างระหว่างศัตรูกับผู้เล่น
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ถ้าผู้เล่นอยู่ในระยะตรวจจับ
        if (distanceToPlayer <= detectionRange)
        {
            isMoving = true;

            // คำนวณทิศทางที่ต้องการให้ศัตรุหมุนไปทางผู้เล่น
            Vector3 direction = (player.position - transform.position).normalized;

            // หมุนศัตรูให้หันหน้าหาผู้เล่น
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            // เคลื่อนที่เข้าหาผู้เล่น
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            isMoving = false;
        }

        // อัพเดต Animator parameter "isMoving"
        animator.SetBool("isMoving", isMoving); 
    }

    // ฟังก์ชันสำหรับแสดงระยะตรวจจับใน editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}