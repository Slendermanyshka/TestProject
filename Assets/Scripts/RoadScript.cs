using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour
{

    public void ChangeSizeOfRoad(float roadSizeFloat)
    {
        transform.localScale = new Vector3(93,1,roadSizeFloat);
    }
}
