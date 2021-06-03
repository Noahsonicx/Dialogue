using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventorye;
using Plr;

[System.Serializable]
public struct EquipSlot
{
    [SerializeField] private Items items;
    public Items EquipedItem
    {
        get
        {
            return items;
        }
        set
        {
            items = value;
            itemEquiped.Invoke(this); // change to this
        }
    }
    public Transform visualLocation;
    public Vector3 offset;

    public delegate void ItemEquiped(EquipSlot item);
    public event ItemEquiped itemEquiped;
}

public class Equipment : MonoBehaviour
{
    public EquipSlot primary;
    public EquipSlot secondary;
    public EquipSlot defensive;

    private void Awake()
    {
        primary.itemEquiped += EquipItem;
        secondary.itemEquiped += EquipItem;
        defensive.itemEquiped += EquipItem;
    }

    // Start is called just before any of the Update methods is called the first time
    private void Start()
    {
        EquipItem(primary);
        EquipItem(secondary);
        EquipItem(defensive);
    }

    public void EquipItem(EquipSlot item) //Item item, Transform visualLocation).
    {
        if (item.visualLocation == null)
        {
            return;
        }

        foreach (Transform child in item.visualLocation)
        {
            GameObject.Destroy(child.gameObject);
        }

        if (item.EquipedItem.Mesh == null)
        {
            return;
        }

        GameObject meshInstance = Instantiate(item.EquipedItem.Mesh, item.visualLocation);
        meshInstance.transform.localPosition = item.offset;
        OffSetLocation offset = meshInstance.GetComponent<OffSetLocation>();

        if (offset != null)
        {
            meshInstance.transform.localPosition += offset.positionOffset;
            meshInstance.transform.localRotation = Quaternion.Euler(offset.rotationOffset);
            meshInstance.transform.localScale = offset.scaleOffset;
        }
    }
}
