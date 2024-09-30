using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;           // ตัวแปรเก็บตำแหน่งของ player
    public float detectionRange = 10f; // ระยะตรวจจับผู้เล่น
    public float attackRange = 1.5f;   // ระยะโจมตี
    public float speed = 5f;            // ความเร็วของศัตรู
    public float rotationSpeed = 5f;    // ความเร็วในการหมุนของศัตรู

    private Animator animator;           // อ้างอิงไปยัง Animator
    private bool isAttacking = false;    // ตรวจสอบสถานะการโจมตี
    public WeaponEnemy equippedWeapon;   // เพิ่มอ้างอิงไปยังอาวุธของศัตรู

    void Start()
    {
        animator = GetComponent<Animator>(); // ดึง Animator จาก GameObject
    }

    void Update()
    {
        // คำนวณระยะห่างระหว่างศัตรูกับผู้เล่น
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ถ้าผู้เล่นอยู่ในระยะตรวจจับ
        if (distanceToPlayer <= detectionRange)
        {
            // คำนวณทิศทางที่ต้องการให้ศัตรูหมุนไปทางผู้เล่น
            Vector3 direction = (player.position - transform.position).normalized;

            // หมุนศัตรูให้หันหน้าหาผู้เล่น
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            // หากอยู่ในระยะโจมตี
            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                StartCoroutine(AttackRoutine()); // เริ่มการโจมตี
            }
            else if (distanceToPlayer > attackRange)
            {
                // เคลื่อนที่เข้าหาผู้เล่น
                transform.position += direction * speed * Time.deltaTime;
                animator.SetBool("isMoving", true); // เริ่มการเคลื่อนไหว
            }
        }
        else
        {
            // หากอยู่นอกระยะตรวจจับให้หยุดการเคลื่อนไหว
            animator.SetBool("isMoving", false); // หยุดการเคลื่อนไหว
        }
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true; // ตั้งค่าสถานะการโจมตี
        animator.SetTrigger("isAttacking"); // เรียกใช้อนิเมชันการโจมตี

        // ทำการโจมตีที่นี่ เช่น เรียกใช้ฟังก์ชันการโจมตีของอาวุธ
        if (equippedWeapon != null)
        {
            equippedWeapon.Attack(); // เรียกใช้ฟังก์ชันการโจมตีจาก WeaponEnemy
        }

        yield return new WaitForSeconds(1f); // รอระยะเวลาในการโจมตี (ปรับได้ตามต้องการ)
        isAttacking = false; // เปลี่ยนสถานะกลับไปที่ไม่โจมตี
    }

    // ฟังก์ชันสำหรับแสดงระยะตรวจจับใน editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange); // แสดงระยะโจมตี
    }
}
