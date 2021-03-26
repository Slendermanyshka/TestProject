using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerGeneralController : MonoBehaviour
{




    //PlayerVariables
    public float health = 100f;
    public float speed = 50f;

    public float minimumSize;
    public float maxSize = 100f;
    public float maxTime = 100f;
    public float bulletSpeed;
    public Vector3 scale;

    private bool isShootingStage=true;


    private Vector3 spawnPos;

    //object refenrences
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject roadObject;
    //[SerializeField] CinemachineCameraOffset

    //UI references
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject joystickGameObject;

    //counting time in game
    private float pressButtonStartTime = 0f;

    GameObject instantiatedBall;
    Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
joystick = joystickGameObject.GetComponent<Joystick>();
                    scale = new Vector3(health, health, health);
                    spawnPos = new Vector3(playerObject.transform.position.x - 100f, playerObject.transform.position.y - 10f, playerObject.transform.position.z);

    }


    // Update is called once per frame
    private void Update()
    {

            switch (isShootingStage)
            {

                case true:
                    {

                        if (Input.GetMouseButtonDown(0))
                        {
                            //start the calculation of tine
                            pressButtonStartTime = Time.time;
                            //spawn the bullet
                            instantiatedBall = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
                        }

                        if (Input.GetMouseButton(0))
                        {
                            //calculate the time passsed
                            float holdDownTime = Time.time - pressButtonStartTime;

                            //Player Sizing
                            health = health - CalculateBallSize(holdDownTime);
                            scale = new Vector3(health, health, health);
                            playerObject.transform.localScale = scale;
                            SizingMethod(health);
                            healthChecker();

                            //Bullet Sizing
                            instantiatedBall.GetComponent<BulletScript>().explosionRadius = CalculateBulletSize(holdDownTime) * 1000;



                        }

                        if (Input.GetMouseButtonUp(0))
                        {
                            //calculating the time for button press
                            float holdDownTime = Time.time - pressButtonStartTime;

                            //adding force for bullet
                            instantiatedBall.GetComponent<Rigidbody>().velocity = new Vector3(-bulletSpeed, 0, 0);

                        }

                        //checking health
                        healthChecker();
                        break;

                    }
                case false:
                    {
                    transform.Translate(new Vector3(-speed*Time.deltaTime,0,joystick.Horizontal/3));
                    break;
                    }
            }

    }




    private void healthChecker()
    {
        if (health <= 20f)
        {
            SceneManager.LoadScene(0);
        }
    }

    private float CalculateBallSize(float holdtime)
    {
        
        float holdTimeNormalized = Mathf.Clamp01(holdtime / maxTime);
        float calculatedBallSize = holdTimeNormalized / maxTime;
        return calculatedBallSize;
    }

    private float CalculateBulletSize(float holdtime)
    {

        float holdTimeNormalized = Mathf.Clamp01(holdtime / maxTime);
        float calculateBulletSize = holdTimeNormalized / maxTime;
        return calculateBulletSize;
    }


    private void SizingMethod(float healthVariable)
    {
        playerObject.transform.localScale = new Vector3(health, health, health);
        roadObject.GetComponent<RoadScript>().ChangeSizeOfRoad(healthVariable/10);
    }
    public void startRunning()
    {
        isShootingStage = false;
        joystickGameObject.SetActive(true);
        startButton.SetActive(false);
        playerObject.GetComponent<Rigidbody>().velocity = new Vector3(speed,0,0);
    }

}
