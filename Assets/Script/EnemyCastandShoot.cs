using UnityEngine;
using System.Collections;

public class EnemyCastAndShoot : MonoBehaviour
{
    public Transform player;                  // ตัวแปรเก็บตำแหน่งของผู้เล่น
    public GameObject energyBallPrefab;       // Prefab ของลูกพลังงาน
    public Transform castPoint;               // จุดที่ลูกพลังงานจะถูกยิงออกไป
    public float detectionRange = 15f;        // ระยะที่ศัตรูจะเริ่มทำการร่ายเวท
    public float castTime = 2f;               // ระยะเวลาการยืนร่าย
    public float projectileSpeed = 10f;       // ความเร็วของลูกพลังงาน
    public float cooldownTime = 5f;           // เวลา cooldown หลังจากยิงพลังงาน
    public float rotationSpeed = 2f;          // ความเร็วในการหมุนของศัตรูให้หันหาผู้เล่น

    private Animator animator;
    private bool isCasting = false;           // ตรวจสอบว่ากำลังร่ายเวทอยู่หรือไม่
    private bool canShoot = true;             // ตรวจสอบว่าสามารถยิงพลังงานได้หรือไม่

    void Start()
    {
        // ดึง Component Animator มาเก็บไว้ในตัวแปร animator
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // คำนวณระยะห่างระหว่างศัตรูกับผู้เล่น
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ตรวจสอบว่าผู้เล่นอยู่ในระยะที่กำหนดและศัตรูสามารถร่ายเวทได้
        if (distanceToPlayer <= detectionRange && canShoot && !isCasting)
        {
            StartCoroutine(CastAndShoot());
        }
    }

    // Coroutine สำหรับทำการยืนร่ายเวทและยิงพลังงาน
    IEnumerator CastAndShoot()
    {
        isCasting = true;  // เริ่มต้นการร่ายเวท

        // เล่นแอนิเมชันการร่ายเวท
        if (animator != null)
        {
            animator.SetTrigger("Cast");
        }

        // รอจนกว่าจะร่ายเวทเสร็จ
        yield return new WaitForSeconds(castTime);

        // สร้างลูกพลังงานและยิงออกไป
        ShootEnergyBall();

        // เริ่ม cooldown ก่อนที่จะสามารถยิงได้อีกครั้ง
        yield return new WaitForSeconds(cooldownTime);

        isCasting = false;
        canShoot = true;  // สามารถยิงได้อีกครั้ง
    }

    // ฟังก์ชันสำหรับยิงลูกพลังงานออกไป
    void ShootEnergyBall()
    {
        // ตรวจสอบว่ามี Prefab ของลูกพลังงานและจุดยิงพลังงานหรือไม่
        if (energyBallPrefab != null && castPoint != null)
        {
            // สร้างลูกพลังงานที่ตำแหน่งของ castPoint
            GameObject energyBall = Instantiate(energyBallPrefab, castPoint.position, castPoint.rotation);

            // คำนวณทิศทางที่ศัตรูกำลังหันไป (ทิศทางหาผู้เล่น)
            Vector3 direction = (player.position - castPoint.position).normalized;

            // เพิ่มความเร็วให้กับลูกพลังงาน
            Rigidbody rb = energyBall.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
            }
        }

        // ตั้งค่าให้ไม่สามารถยิงได้อีกจนกว่าจะ cooldown เสร็จ
        canShoot = false;
    }

    // ฟังก์ชันสำหรับหมุนศัตรูให้หันหน้าหาผู้เล่นในขณะที่ร่ายเวท
    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    // ฟังก์ชันสำหรับแสดงระยะตรวจจับใน editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
