using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
   public List<string> inventory;

   void Update()
   {
    if(Input.GetMouseButtonDown(0))
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray,out hitInfo))
        {
            if(hitInfo.collider.gameObject.tag == "Pickable")
            {
                inventory.Add(hitInfo.collider.gameObject.name);
                Destroy(hitInfo.collider.gameObject);
            }
        }
    }
   }
}
