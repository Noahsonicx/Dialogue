using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plr;

namespace Inventorye
{
    public class DropPickupItem : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private Transform dropPoint;
        [SerializeField] private Camera cam;
    

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hitInfo;
                if(Physics.Raycast(ray, out hitInfo, 50f))
                {
                    DropItem droppedItem = hitInfo.collider.gameObject.GetComponent<DropItem>();
                    if(droppedItem != null)
                    {
                        inventory.AddItem(droppedItem.item);
                        Destroy(hitInfo.collider.gameObject);
                    }
                }
            }
        }
        public void DropItem()
        {
            if(inventory.selectedItem == null)
            {
                return;
            }
            GameObject mesh = inventory.selectedItem.Mesh;
            if(mesh != null)
            {
                GameObject spawningMesh = Instantiate(mesh, null);
                spawningMesh.transform.position = dropPoint.position;

                DropItem droppedItem = mesh.GetComponent<DropItem>();
                if (droppedItem != null)
                {
                    droppedItem.item = new Items(inventory.selectedItem, 1);
                }
            }

            inventory.selectedItem.Amount--;
            if(inventory.selectedItem.Amount <= 0)
            {
                inventory.RemoveItem(inventory.selectedItem);
                inventory.selectedItem = null;
            }

        }
    }
}