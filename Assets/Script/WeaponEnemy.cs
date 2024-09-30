using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEnemy : MonoBehaviour
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
        // ตรวจสอบว่าชนกับผู้เล่นที่มี Tag "Player"
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // ทำดาเมจให้ผู้เล่น
            }
        }
    }
}
