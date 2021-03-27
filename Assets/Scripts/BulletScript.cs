using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public GameObject explosionEffect;
    public float explosionRadius;
    public float explosionMultiplier;
    public Color changeColor;


    private void Update()
    {
        transform.localScale = new Vector3(explosionRadius,explosionRadius,explosionRadius);
    }


    //Collision detection and layer filtering
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.layer)
        {
            case 6:
                {
                    //do an explosion vfx
                    GameObject exp = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                    Destroy(exp, 2f);
                    Destroy(this.gameObject);
                    //do stuff to change color and tag
                    radiusColor();
                    break;
                }
            case 7:
                {

                    Physics.IgnoreCollision(this.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
                    break;
                }
            case 9:
                {
                    GameObject exp = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                    Destroy(exp, 2f);
                    Destroy(this.gameObject);
                    //do stuff to change color and tag
                    radiusColor();
                    break;
                }
            default:
                {
                    Destroy(this.gameObject);
                    break;
                }
        }
    }

    //calculations for the objects in radius
    private void radiusColor()
    {
        //storing colliders in radius in an array
      Collider[] other = Physics.OverlapSphere(transform.position, explosionRadius*explosionMultiplier);

        //doing stuff to each collider in array
      foreach (Collider nearby in other)
        {
            GameObject gOther = nearby.gameObject;

            //filtering tags for coloring
            if (gOther != null && gOther.tag == "Obstacle" && gOther.tag!="Bullet" && gOther.tag != "Player")
             {
                 coloringProcess(gOther);
             }
            else if (gOther!=null && gOther.tag == "Finish")
            {

            }

        }
    }

    //method for coloring and chacnhing tag and layer
    private void coloringProcess(GameObject other)
    {
            other.GetComponent<MeshRenderer>().material.color = changeColor;
            other.gameObject.tag = "ObstacleHit";
            other.gameObject.layer = 7;
    }


}
