using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public int numberOfObstacles = 0;
    public Vector3 size;
    public Vector3 center;
    public Color gizmoColor;
    public GameObject obstaclePrefab;


    private void Start()
    {
        for(int i = 0; i <= numberOfObstacles; i++)
        {
            spawnProcedure();

        }

    }

    private void FixedUpdate()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(center,size);
    }

    private void spawnProcedure()
    {
        Vector3 pos = center + new Vector3(Random.RandomRange(-size.x / 2, size.x / 2), Random.RandomRange(-size.y / 2, size.y / 2), Random.RandomRange(-size.z / 2, size.z / 2));
        Instantiate(obstaclePrefab , pos , Quaternion.identity);
    }

}
