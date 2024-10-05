using UnityEngine;
using System.Collections;
using UnityEngine.AI; // นำเข้า NavMeshAgent

public class EnemyChase : MonoBehaviour
{
    public Transform player;          // ตัวแปรเก็บตำแหน่งของ player
    public float detectionRange = 100f; // ระยะตรวจจับผู้เล่น
    public float chaseDelay = 5f;     // ระยะเวลาหน่วงก่อนจะไล่ผู้เล่น

    private Animator animator;
    private bool isMoving = false;
    private bool isChasing = false;   // ตัวแปรตรวจสอบว่าเริ่มไล่ตามหรือยัง
    private bool playerDetected = false; // ตรวจจับผู้เล่น
    private NavMeshAgent agent;        // ตัวแปร NavMeshAgent ของศัตรู

    void Start()
    {
        // ดึง Component Animator และ NavMeshAgent มาเก็บไว้ในตัวแปร
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
{
    float distanceToPlayer = Vector3.Distance(transform.position, player.position);
    
    if (distanceToPlayer <= detectionRange && !playerDetected)
    {
        playerDetected = true;
        StartCoroutine(ChasePlayerAfterDelay());
    }

    if (isChasing)
    {
        isMoving = true;
        
        // Set destination for the NavMesh Agent
        agent.SetDestination(player.position);
    }
    else
    {
        isMoving = false;
    }

    animator.SetBool("isMoving", isMoving); 
}


    // Coroutine สำหรับหน่วงเวลาหลังจากตรวจจับผู้เล่น
    IEnumerator ChasePlayerAfterDelay()
    {
        // รอเป็นเวลา chaseDelay วินาทีก่อนจะเริ่มไล่ตามผู้เล่น
        yield return new WaitForSeconds(chaseDelay);
        isChasing = true;
    }

    // ฟังก์ชันสำหรับแสดงระยะตรวจจับใน editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
