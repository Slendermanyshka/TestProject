using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenTriggerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponentInParent<Animation>().Play();
        }
    }
}
