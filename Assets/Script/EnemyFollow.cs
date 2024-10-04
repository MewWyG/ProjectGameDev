using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Rigidbody rb;
    public Transform Player;
    public float moveSpeed = 3f;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if (Player != null)
        {
            // หา Vector ที่ชี้จาก Enemy ไปยัง Player
            Vector3 direction = (Player.position - transform.position).normalized;

            // เคลื่อนที่ Enemy ไปในทิศทางนั้น
            rb.velocity = direction * moveSpeed;

            // หมุน Enemy ให้หันหน้าไปทาง Player
            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 5f * Time.deltaTime);
            }
        }
    }
}