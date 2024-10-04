using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
=======

>>>>>>> Map
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody rb;
<<<<<<< HEAD
    private bool isAttack = false;
    public Camera cam;
    private Vector3 targetPosition;
    private bool isMove = false;
    public static PlayerController instance;
=======
    public Camera cam;
    private Vector3 targetPosition;
    private bool isMove = false;
>>>>>>> Map

    // Reference to the Animator component
    private Animator animator;

<<<<<<< HEAD
    // Reference to WeaponPlayer component
    private WaeponPlayer weapon;

=======
>>>>>>> Map
    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();
<<<<<<< HEAD

        // Find the WeaponPlayer component attached to the player's weapon
        weapon = GetComponentInChildren<WaeponPlayer>();
=======
>>>>>>> Map
    }

    void Update()
    {
<<<<<<< HEAD
        // ตรวจสอบการคลิกขวาเพื่อเดิน
        if (Input.GetMouseButtonDown(1)) // 1 คือ คลิกขวาเดิน
=======
        // ตรวจสอบการคลิกขวา
        if (Input.GetMouseButtonDown(1)) // 1 คือ คลิกขวา
>>>>>>> Map
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
<<<<<<< HEAD

        if(Input.GetMouseButtonDown(0)) // 0 คือ คลิกซ้ายโจมตี
        {
            Attack();
        }
=======
>>>>>>> Map
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
<<<<<<< HEAD

    private void Attack()
    {
        if (!isAttack)
        {
            isAttack = true; // เปลี่ยนสถานะการโจมตีเป็น true
            animator.SetBool("isAttack", true); // เรียกใช้อนิเมชันการโจมตี

            // เรียกใช้การโจมตีของอาวุธ
            if (weapon != null)
            {
                weapon.Attack();
            }

            StartCoroutine(ResetAttack());
        }
    }

    private IEnumerator ResetAttack()
    {
        // รอเวลาตามความยาวของอนิเมชันการโจมตี (เช่น 0.5 วินาที)
        yield return new WaitForSeconds(0.5f);

        isAttack = false; // รีเซ็ตสถานะการโจมตีเป็น false
        animator.SetBool("isAttack", false); // ตั้งค่าอนิเมชันการโจมตีเป็น false เพื่อหยุดอนิเมชัน
    }
=======
>>>>>>> Map
}
