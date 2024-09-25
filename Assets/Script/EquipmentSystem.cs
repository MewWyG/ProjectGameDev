using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] GameObject weapon;

    GameObject currentWeaponInHand;

    void Start()
    {
        // เริ่มต้นด้วยการสร้างอาวุธในมือ
        currentWeaponInHand = Instantiate(weapon, transform);
    }

    public void DrawWeapon()
    {
        // ฟังก์ชันนี้อาจไม่จำเป็นแล้ว เนื่องจากไม่มีการใช้งาน weaponHolder
        // แต่อาจต้องการตรึงอาวุธไว้ในตำแหน่งอื่นได้
        if (currentWeaponInHand == null)
        {
            currentWeaponInHand = Instantiate(weapon, transform);
        }
    }

    public void SheathWeapon()
    {
        // ไม่มี weaponSheath แล้ว จึงแค่ทำลายอาวุธในมือ
        Destroy(currentWeaponInHand);
    }

    public void StartDealDamage()
    {
        // เรียกใช้ฟังก์ชัน StartDealDamage บนอาวุธที่อยู่ในมือ
        if (currentWeaponInHand != null)
        {
            currentWeaponInHand.GetComponentInChildren<AttackPoint>().StartDealDamage();
        }
    }

    public void EndDealDamage()
    {
        // เรียกใช้ฟังก์ชัน EndDealDamage บนอาวุธที่อยู่ในมือ
        if (currentWeaponInHand != null)
        {
            currentWeaponInHand.GetComponentInChildren<AttackPoint>().EndDealDamage();
        }
    }
}
