using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public GameObject explosionEffect;
    public Vector3 sizeOfBall;
    public float explosionRadius;
    public float speedOfBullet = 10f;
    public Color changeColor;

    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.localScale = sizeOfBall;
        rb.velocity = new Vector3(-speedOfBullet, 0, 0);
    }

   



    //Stuff that's being called on collision
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.layer)
        {
            case 6:
                {
                    //do an explosion vfx
                    GameObject exp = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                    Destroy(exp, 2f);
                    
                    //do stuff in a certain radius
                    radiusColor();
                    Destroy(this.gameObject);

                    break;
                }
            case 7:
                {
                    Physics.IgnoreCollision(this.GetComponent<Collider>(),other.gameObject.GetComponent<Collider>());
                    break;
                }
        }
       
            
        
       


    }



    //calculations for the objects in radius
    private void radiusColor()
    {
        Collider[] other = Physics.OverlapSphere(transform.position,explosionRadius);
        foreach(Collider nearby in other)
        {

            GameObject gOther = nearby.gameObject;
            if (gOther != null && gOther.tag == "Obstacle")
            {
                coloringProcess(gOther);

            }
        }


    }


    private void coloringProcess(GameObject other)
    {
        if (other.tag == "Obstacle")
        {
            other.GetComponent<MeshRenderer>().material.color = changeColor;
            other.gameObject.tag = "ObstacleHit";
            other.gameObject.layer = 7;
        }
    }



}
