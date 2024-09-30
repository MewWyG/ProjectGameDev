using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool isMove = false;
    private bool isAttack = false; // เก็บสถานะการโจมตี
    public Rigidbody rb;
    public Camera cam;
    private Vector3 targetPosition;
    public static PlayerController instance;

    // Reference to the Animator component
    private Animator animator;

    // อ้างอิงไปยังอาวุธ
    public Weapon equippedWeapon;

    // ระยะเวลาระหว่างการโจมตี
    public float attackInterval = 1f; // 1 วินาที

    void Awake()
    {
        // กำหนดให้ instance เป็นตัวนี้
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>(); 
        StartCoroutine(AttackRoutine()); // เริ่ม Coroutine สำหรับการโจมตีอัตโนมัติ
    }

    void Update()
    {
        // ตรวจสอบการคลิกขวา
        if (Input.GetMouseButtonDown(1)) // 1 คือ คลิกขวา
        {
            // หาตำแหน่งที่เมาส์คลิกในโลก 3D
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // สร้าง Plane สำหรับพื้น
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                targetPosition = ray.GetPoint(rayDistance); // กำหนดตำแหน่งเป้าหมาย
                isMove = true; // เปลี่ยนสถานะเป็นกำลังเคลื่อนที่
                animator.SetBool("isMoving", true); // ตั้งค่า isMoving เป็น true เพื่อเริ่มอนิเมชันการวิ่ง
            }
        }

        // หาตำแหน่งเมาส์ในโลก 3D เพื่อใช้ในการหมุนตัวละคร
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane mousePlane = new Plane(Vector3.up, Vector3.zero); // สร้าง Plane สำหรับพื้น
        float mouseRayDistance;

        if (mousePlane.Raycast(mouseRay, out mouseRayDistance))
        {
            Vector3 mousePos = mouseRay.GetPoint(mouseRayDistance);
            // หมุนตัวละครไปยังตำแหน่งเมาส์
            Vector3 lookDir = mousePos - rb.position;
            lookDir.y = 0; // แก้ไขค่า y เป็น 0 เพื่อไม่ให้ตัวละครหมุนในแนวตั้ง

            if (lookDir != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(lookDir);
                rb.MoveRotation(rotation);
            }
        }
    }

    void FixedUpdate()
    {
        // หากกำลังเคลื่อนที่ ให้คำนวณการเคลื่อนที่
        if (isMove)
        {
            Vector3 direction = (targetPosition - rb.position).normalized; // หาทิศทางไปยังตำแหน่งเป้าหมาย

            // หากใกล้ถึงตำแหน่งเป้าหมาย ให้หยุดการเคลื่อนที่
            if (Vector3.Distance(rb.position, targetPosition) < 0.1f)
            {
                isMove = false; // เปลี่ยนสถานะเป็นไม่เคลื่อนที่
                animator.SetBool("isMoving", false); // ตั้งค่า isMoving เป็น false เพื่อเปลี่ยนกลับไปที่ท่า Idle
            }
            else
            {
                // เคลื่อนที่ตามทิศทางไปยังตำแหน่งเป้าหมาย
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (equippedWeapon != null)
            {
                equippedWeapon.Attack(); // เรียกใช้ฟังก์ชันการโจมตี
                animator.SetTrigger("isAttacking"); // เรียกใช้การโจมตีในอนิเมชัน
            }

            yield return new WaitForSeconds(attackInterval); // รอระยะเวลาที่กำหนด
        }
    }

    // ฟังก์ชันสำหรับการใช้สกิล (สามารถแยกต่างหากได้เหมือนเดิม)
    private void UseSkill()
    {
        StartCoroutine(PlaySkillAnimation());
    }

    private IEnumerator PlaySkillAnimation()
    {
        animator.SetTrigger("isUseSkill");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
