using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerGeneralController : MonoBehaviour
{
    //PlayerVariables
    [Header("PlayerVariables")]
    public float health = 100f;
    public float speed = 50f;
    public float maxTime = 100f;
    public float bulletSpeed;
    private Vector3 scale;

    //gameControllingVariables
    private bool isShootingStage=true;
    private Vector3 bulletSpawnPos;
    private bool isScreenUnlocked=false;

    [Header("ObjectReferences")]
    //object refenrences
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject roadObject;

    //UI references
    [Header("UI References")]
    [SerializeField] GameObject startButton;
    public GameObject joystickGameObject;

    //counting time in game
    private float pressButtonStartTime = 0f;

    //instances of objects
    GameObject instantiatedBall;
    Joystick joystick;

    private void Awake()
    {
        //initialization and health-scale setting
        joystick = joystickGameObject.GetComponent<Joystick>();
        scale = new Vector3(health, health, health);
        bulletSpawnPos = new Vector3(playerObject.transform.position.x - 100f, playerObject.transform.position.y - 10f, playerObject.transform.position.z);
    }
    private void Start()
    {
        SizingMethod(health);
    }

    //function to check pointing over game object 
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


    // Update is called once per frame
    private void Update()
    {

        //switch to check current game state
        switch (isShootingStage)
        {
            case true:
                {
                    //preventing interaction with UI
                    if (!IsPointerOverUIObject())
                    {
                        if (Input.touchCount > 0)
                        {
                            Touch touch = Input.GetTouch(0);
                            if (touch.phase == TouchPhase.Began)
                            {
                                //start the calculation of tine
                                pressButtonStartTime = Time.time;
                                //spawn the bullet
                                instantiatedBall = Instantiate(bulletPrefab, bulletSpawnPos, Quaternion.identity);
                                isScreenUnlocked = true; 
                            }

                            if (isScreenUnlocked)
                            {

                                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
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

                                if (touch.phase == TouchPhase.Ended)
                                {
                                    //calculating the time for button press
                                    float holdDownTime = Time.time - pressButtonStartTime;

                                    //adding force for bullet
                                    instantiatedBall.GetComponent<Rigidbody>().velocity = new Vector3(-bulletSpeed, 0, 0);
                                }
                            }
                            else { return; }
                        }

                    }
                    //checking health
                    healthChecker();
                    break;
                }

            case false:
                {
                    MovingCharacterForward();
                    break;
                }
        }
    }

    //moves Character fourwards
    private void MovingCharacterForward()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0, joystick.Horizontal*speed/ 1.5f * Time.deltaTime));
    }

    public void StopMoving()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0, -joystick.Horizontal * speed / 1.5f * Time.deltaTime));
    }

    //method to switch game state
    public void startRunning()
    {
        isShootingStage = !isShootingStage;
        joystickGameObject.SetActive(true);
        startButton.SetActive(false);
        playerObject.GetComponent<Rigidbody>().velocity = new Vector3(speed,0,0);
    }


    //private calculations
    private void healthChecker()
    {
        if (health <= 20f)
        {
            GetComponentInChildren<PlayerMovement>().DeathSequence();
        }
    }


    //calculate player size
    private float CalculateBallSize(float holdtime)
    {

        float holdTimeNormalized = Mathf.Clamp01(holdtime / maxTime);
        float calculatedBallSize = holdTimeNormalized / maxTime;
        return calculatedBallSize;
    }

    //calculate Bullet size
    private float CalculateBulletSize(float holdtime)
    {

        float holdTimeNormalized = Mathf.Clamp01(holdtime / maxTime);
        float calculateBulletSize = holdTimeNormalized / maxTime;
        return calculateBulletSize;
    }

    //size the player
    private void SizingMethod(float healthVariable)
    {
        playerObject.transform.localScale = new Vector3(health, health, health);
        roadObject.GetComponent<RoadScript>().ChangeSizeOfRoad(healthVariable / 10);
    }

}
