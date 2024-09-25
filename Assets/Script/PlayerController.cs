using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody rb;
    public Camera cam;
    private Vector3 targetPosition;
    private bool isMoving = false;

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
                isMoving = true; // เปลี่ยนสถานะเป็นกำลังเคลื่อนที่
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
        if (isMoving)
        {
            Vector3 direction = (targetPosition - rb.position).normalized; // หาทิศทางไปยังตำแหน่งเป้าหมาย

            // หากใกล้ถึงตำแหน่งเป้าหมาย ให้หยุดการเคลื่อนที่
            if (Vector3.Distance(rb.position, targetPosition) < 0.1f)
            {
                isMoving = false; // เปลี่ยนสถานะเป็นไม่เคลื่อนที่
            }
            else
            {
                // เคลื่อนที่ตามทิศทางไปยังตำแหน่งเป้าหมาย
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }
}
