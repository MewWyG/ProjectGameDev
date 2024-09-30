using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;           // ตัวแปรเก็บตำแหน่งของ player
    public float attackRange = 2f;     // ระยะการโจมตีของศัตรู
    public float attackCooldown = 1f;  // เวลาระหว่างการโจมตีแต่ละครั้ง
    public int attackDamage = 10;      // ความเสียหายจากการโจมตี

    private bool canAttack = true;     // ใช้ตรวจสอบว่าศัตรูสามารถโจมตีได้หรือไม่
    private Animator animator;         // อ้างอิงถึง Animator เพื่อใช้แอนิเมชันโจมตี
    private bool isAttacking = false;  // สถานะการโจมตี

    void Start()
    {
        // ดึง Animator มาเก็บในตัวแปร
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // คำนวณระยะห่างระหว่างศัตรูกับผู้เล่น
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ตรวจสอบว่าผู้เล่นอยู่ในระยะการโจมตี
        if (distanceToPlayer <= attackRange && canAttack && !isAttacking)
        {
            AttackPlayer();
        }

        // อัปเดตแอนิเมชัน based on isAttacking state
        animator.SetBool("isAttacking", isAttacking);
    }

    // ฟังก์ชันโจมตีผู้เล่น
    void AttackPlayer()
    {
        // เริ่มโจมตี และปิดการโจมตีชั่วคราวในขณะที่ cooldown กำลังทำงาน
        canAttack = false;
        isAttacking = true;  // เปิดสถานะการโจมตี

        // เรียกใช้การสร้างความเสียหายหลังจากแอนิเมชันโจมตี
        Invoke(nameof(DealDamage), 0.5f);  // Delay damage to match the attack animation timing

        // เริ่ม cooldown หลังจากโจมตี
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    // ฟังก์ชันสร้างความเสียหายให้ผู้เล่น
    void DealDamage()
    {
        // ตรวจสอบระยะอีกครั้ง เพื่อป้องกันการสร้างความเสียหายถ้าผู้เล่นหลุดออกจากระยะการโจมตี
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Damage dealt: " + attackDamage);
            }
            else
            {
                Debug.LogError("PlayerHealth component not found on the player!");
            }
        }
    }

    // ฟังก์ชัน reset การโจมตีหลังจาก cooldown
    void ResetAttack()
    {
        canAttack = true;
        isAttacking = false;  // ปิดสถานะการโจมตีหลังจาก cooldown เสร็จสิ้น
    }

    // ฟังก์ชันแสดงระยะการโจมตีใน editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
