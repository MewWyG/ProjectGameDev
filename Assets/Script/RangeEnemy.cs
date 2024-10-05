using UnityEngine;
using System.Collections;
using UnityEngine.AI; // เพิ่มการใช้ NavMesh

public class RangeEnemy : MonoBehaviour
{
    public Transform player;          // ตัวแปรเก็บตำแหน่งของ player
    public float detectionRange = 100f; // ระยะตรวจจับผู้เล่น
    public float stoppingDistance = 5f; // ระยะที่ศัตรูจะหยุดเดินเมื่อเข้าใกล้ผู้เล่น
    public float chaseDelay = 5f;     // ระยะเวลาหน่วงก่อนจะไล่ผู้เล่น
    public float spellCastCooldown = 3f; // ระยะเวลาคูลดาวน์ระหว่างการร่ายเวท

    private Animator animator;
    private NavMeshAgent agent; // ตัวแปร NavMeshAgent
    private bool isMoving = false;
    private bool isChasing = false;   // ตัวแปรตรวจสอบว่าเริ่มไล่ตามหรือยัง
    private bool playerDetected = false; // ตรวจจับผู้เล่น
    private bool isCastingSpell = false; // ตรวจสอบว่ากำลังร่ายเวทอยู่หรือไม่

    void Start()
    {
        // ดึง Component Animator มาเก็บไว้ในตัวแปร animator
        animator = GetComponent<Animator>(); 
        // ดึง Component NavMeshAgent มาเก็บไว้ในตัวแปร agent
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // คำนวณระยะห่างระหว่างศัตรูกับผู้เล่น
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ถ้าผู้เล่นอยู่ในระยะตรวจจับและยังไม่เริ่มไล่ตาม
        if (distanceToPlayer <= detectionRange && !playerDetected)
        {
            playerDetected = true;
            StartCoroutine(ChasePlayerAfterDelay());
        }

        // ถ้าเริ่มไล่ตามแล้ว
        if (isChasing && !isCastingSpell)
        {
            // ถ้าผู้เล่นอยู่ไกลเกินไป, ให้เดินไปหาผู้เล่น
            if (distanceToPlayer > stoppingDistance)
            {
                isMoving = true;
                agent.SetDestination(player.position); // ใช้ NavMeshAgent ตั้งเป้าหมายไปที่ผู้เล่น
            }
            else
            {
                // หยุดเคลื่อนที่เมื่อเข้าใกล้ผู้เล่นในระยะที่กำหนด
                isMoving = false;
                agent.ResetPath(); // เคลียร์เส้นทางของ NavMeshAgent
                
                // เริ่มการร่ายเวทโจมตี
                StartCoroutine(CastSpell());
            }
        }
        else
        {
            isMoving = false;
            agent.ResetPath(); // เคลียร์เส้นทางของ NavMeshAgent ถ้าไม่ไล่ตาม
        }

        // อัพเดต Animator parameter "isMoving"
        animator.SetBool("isMoving", isMoving); 
    }

    // Coroutine สำหรับหน่วงเวลาหลังจากตรวจจับผู้เล่น
    IEnumerator ChasePlayerAfterDelay()
    {
        // รอเป็นเวลา chaseDelay วินาทีก่อนจะเริ่มไล่ตามผู้เล่น
        yield return new WaitForSeconds(chaseDelay);
        isChasing = true;
    }

    // Coroutine สำหรับร่ายเวทโจมตีผู้เล่น
    IEnumerator CastSpell()
    {
        isCastingSpell = true;

        // เล่นแอนิเมชันร่ายเวท (สมมติว่า parameter ใน Animator ชื่อว่า "isCasting")
        if (animator != null)
        {
            animator.SetBool("isCasting", true);
        }

        // รอเวลาที่เหมาะสมก่อนจะโจมตี (เช่น 1 วินาที เพื่อให้ตรงกับแอนิเมชัน)
        yield return new WaitForSeconds(1f);

        // โจมตีผู้เล่น (เช่น การยิงโปรเจกไทล์ หรือสร้างความเสียหาย)
        DealMagicDamage();

        // ปิดการร่ายเวทหลังจากโจมตีเสร็จ
        if (animator != null)
        {
            animator.SetBool("isCasting", false);
        }

        // รอคูลดาวน์ก่อนจะร่ายเวทอีกครั้ง
        yield return new WaitForSeconds(spellCastCooldown);

        isCastingSpell = false;
    }

    // ฟังก์ชันโจมตีผู้เล่นด้วยเวทมนตร์ (อาจเป็นการสร้างโปรเจกไทล์)
    void DealMagicDamage()
    {
        // ใส่โค้ดสำหรับการสร้างความเสียหายหรือยิงโปรเจกไทล์
        Debug.Log("Casting magic spell to attack the player!");
    }

    // ฟังก์ชันสำหรับแสดงระยะตรวจจับใน editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // แสดงระยะที่ศัตรูจะหยุด
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }
}
