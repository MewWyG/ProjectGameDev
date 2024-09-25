using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    bool canDealDamage;
    List<GameObject> hasAttackPoint;

    [SerializeField] float weaponLength;  
    [SerializeField] float weaponDamage;

    void Start()
    {
        canDealDamage = false;
        hasAttackPoint = new List<GameObject>();
    }

    void Update()
    {
        if (canDealDamage)
        {
            RaycastHit hit;

            int layerMask = 1 << 9; 
            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
            {
                if (!hasAttackPoint.Contains(hit.transform.gameObject))
                {
                    print("damage");
                    hasAttackPoint.Add(hit.transform.gameObject); 
                }
            }
        }
    }

    public void StartDealDamage()
    {
        canDealDamage = true;
        hasAttackPoint.Clear(); // เคลียร์ข้อมูลของวัตถุที่โดนโจมตี
    }

    public void EndDealDamage()
    {
        canDealDamage = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; 
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength); 
    }
}
