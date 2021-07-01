using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventorye;

public class EquipItem : MonoBehaviour
{
    [SerializeField]
    public Items items;

    public void EquipIt()
    {
        Equipment equipment = FindObjectOfType<Equipment>();

        equipment.primary.EquipedItem = items;
    }
}
