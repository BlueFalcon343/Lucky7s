using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemWall : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.PlaceGems();
        }
    }
}
