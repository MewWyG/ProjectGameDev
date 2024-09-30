using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 10; // ค่า damage ของอาวุธ
    public Collider weaponCollider; // Collider ของอาวุธ
     public Animator animator;

    void Start()
    {
        weaponCollider.enabled = false; // ปิด Collider ของอาวุธไว้ก่อน
    }

    // ฟังก์ชันที่เรียกเมื่อจะเริ่มโจมตี
    public void Attack()
    {
        StartCoroutine(AttackCoroutine()); // เรียกใช้ Coroutine สำหรับการโจมตี
    }

    IEnumerator AttackCoroutine()
    {
        Debug.Log("Attack started"); // เพิ่ม Log
        weaponCollider.enabled = true; // เปิด Collider
        yield return new WaitForSeconds(0.3f); // รอ
        weaponCollider.enabled = false; // ปิด Collider
        Debug.Log("Attack ended"); // เพิ่ม Log
    }


    private void OnTriggerEnter(Collider other)
    {
        // ตรวจสอบว่าชนกับศัตรูที่มี Tag "Enemy"
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // ทำดาเมจให้ศัตรู
            }
        }
    }
}
