using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStuff : MonoBehaviour
{
    public float health = 1000f;
    public float speed = 100f;
    public float pressTime = 0f;
    public float maxTime = 10f;
    public GameObject bulletPrefab;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Touch touch = Input.GetTouch(0);
       /* switch (touch.phase)
        {
            case (Input.GetTouch(0)):
            {
                    break;
            }
        }*/



        if (Input.GetMouseButtonDown(0))
        {

            print("Started touch");
            //Touch Begin - True when the finger touches the screen
            //Start inflating the bullet
        }
        else if (Input.GetMouseButton(0))
        {
            print("Touching");
            
            pressTime = pressTime + 1 * Time.deltaTime;
            health = health - 12;//*Time.deltaTime;
            Mathf.Clamp(health, 0f, 1000f);
            //Touch Continued - True when the finger is still touching the screen
            //Inflating bullet
        }
        else if (Input.GetMouseButtonUp(0))
        {
            print("touch ended");
            print(pressTime);
            //Touch End - True when the finger is lifted from the screen
            //Release the bullet
        }


        

    }




}
