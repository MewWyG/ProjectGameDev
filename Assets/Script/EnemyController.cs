using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    public int health = 3; // ค่าชีวิตของศัตรู

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // ตรวจสอบว่าชนกับอาวุธและผู้เล่นกำลังโจมตีอยู่
        if (other.CompareTag("Sword") && PlayerController.instance.isAttack)
        {
            animator.SetTrigger("isHit");
            TakeDamage(1); // เรียกใช้ฟังก์ชันสร้างความเสียหาย
        }
    }

    // ฟังก์ชันที่ใช้ในการสร้างความเสียหาย
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die(); // เรียกใช้ฟังก์ชันตายหากค่าชีวิตเหลือ 0
        }
    }

    private void Die()
    {
        animator.SetTrigger("isDead"); // เรียกใช้อนิเมชันตาย
        StartCoroutine(WaitAndDestroy(0.3f)); // เริ่ม Coroutine เพื่อหน่วงเวลา
    }

    private IEnumerator WaitAndDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // หน่วงเวลา
        Destroy(gameObject); // ลบศัตรูออกจากเกม
    }
}
