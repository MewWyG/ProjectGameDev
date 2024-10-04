using UnityEngine;
<<<<<<< HEAD
using System.Collections;

=======
>>>>>>> Map

public class EnemyChase : MonoBehaviour
{
    public Transform player;          // ตัวแปรเก็บตำแหน่งของ player
    public float detectionRange = 100f; // ระยะตรวจจับผู้เล่น
    public float speed = 5f;          // ความเร็วของศัตรู
<<<<<<< HEAD
    public float rotationSpeed = 5f;  // ความเร็วในการหมุนของศัตรู
    public float chaseDelay = 5f;     // ระยะเวลาหน่วงก่อนจะไล่ผู้เล่น

    private Animator animator;
    private bool isMoving = false;
    private bool isChasing = false;   // ตัวแปรตรวจสอบว่าเริ่มไล่ตามหรือยัง
    private bool playerDetected = false; // ตรวจจับผู้เล่น
=======
    public float rotationSpeed = 5f;   // ความเร็วในการหมุนของศัตรู

    private Animator animator;
    private bool isMoving = false;
>>>>>>> Map

    void Start()
    {
        // ดึง Component Animator มาเก็บไว้ในตัวแปร animator
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        // คำนวณระยะห่างระหว่างศัตรูกับผู้เล่น
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

<<<<<<< HEAD
        // ถ้าผู้เล่นอยู่ในระยะตรวจจับและยังไม่เริ่มไล่ตาม
        if (distanceToPlayer <= detectionRange && !playerDetected)
        {
            playerDetected = true;
            StartCoroutine(ChasePlayerAfterDelay());
        }

        // ถ้าเริ่มไล่ตามแล้ว
        if (isChasing)
        {
            isMoving = true;

            // คำนวณทิศทางที่ต้องการให้ศัตรูหมุนไปทางผู้เล่น
=======
        // ถ้าผู้เล่นอยู่ในระยะตรวจจับ
        if (distanceToPlayer <= detectionRange)
        {
            isMoving = true;

            // คำนวณทิศทางที่ต้องการให้ศัตรุหมุนไปทางผู้เล่น
>>>>>>> Map
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

<<<<<<< HEAD
    // Coroutine สำหรับหน่วงเวลาหลังจากตรวจจับผู้เล่น
    IEnumerator ChasePlayerAfterDelay()
    {
        // รอเป็นเวลา chaseDelay วินาทีก่อนจะเริ่มไล่ตามผู้เล่น
        yield return new WaitForSeconds(chaseDelay);
        isChasing = true;
    }

=======
>>>>>>> Map
    // ฟังก์ชันสำหรับแสดงระยะตรวจจับใน editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> Map
