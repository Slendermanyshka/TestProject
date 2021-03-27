using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour
{
    private void Start()
    {

    }

    public void ChangeSizeOfRoad(float roadSizeFloat)
    {
        transform.localScale = new Vector3(transform.localScale.x, 1, roadSizeFloat);

    }
}
