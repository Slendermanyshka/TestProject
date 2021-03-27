using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    //detection stuff
    [Header("Detection Controls")]
    public LayerMask[] layerMasksForCollisions;
    public Transform groundChecker;
    public float checkGroundRadius = 0.4f;

    //flags of current object Colliding
    bool isGrounded = false;
    bool isTouchingObstacle = false;
    bool isTouchingHitObstacle = false;
    bool isTouchingFinish = false;



    //gravity stuff
    [Header("Gravity Controls")]
    Vector3 fallingVelocity;
    public float bounceForce = 100f;
    public float fallingSpeed=-5f;



    //UIStuff
    [SerializeField] GameObject deathText;

    //references
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        CollisionDetectionMethod();


        //custom bit of gravity
        fallingVelocity.y += fallingSpeed * Time.deltaTime;
        rb.velocity = fallingVelocity;


        //checking for type of ground
        switch (isGrounded)
        {
            case true:
                {
                    fallingVelocity.y = -1f;
                    break;
                }
            case false: { break; }
        }
        switch (isTouchingObstacle)
        {
            case true:
                {
                    DeathSequence();
                    fallingVelocity.y = -1f;
                    break;
                }
            case false: { break; }
        }
        switch (isTouchingHitObstacle)
        {
            case true:
                {
                    fallingVelocity.y = Mathf.Sqrt(bounceForce * -2f * fallingSpeed);
                    break;
                }
            case false: { break; }
        }
        switch (isTouchingFinish)
        {
            case true:
                {
                    fallingVelocity.y = -1f;
                    break; 
                }
            case false: { break; }
        }

    }

    //method for checking type of ground
    private void CollisionDetectionMethod()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, checkGroundRadius, layerMasksForCollisions[0]);
        isTouchingObstacle = Physics.CheckSphere(groundChecker.position, checkGroundRadius, layerMasksForCollisions[1]);
        isTouchingHitObstacle = Physics.CheckSphere(groundChecker.position, checkGroundRadius, layerMasksForCollisions[2]);
        isTouchingFinish = Physics.CheckSphere(groundChecker.position, checkGroundRadius, layerMasksForCollisions[3]);
    }

    public void DeathSequence()
    {
        deathText.SetActive(true);
        GetComponentInParent<PlayerGeneralController>().joystickGameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
        Invoke("DeathSceneReload", 3f);
    }

    private void DeathSceneReload()
    {
        SceneManager.LoadScene(0);

    }

}
