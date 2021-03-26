using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //detection stuff
    public LayerMask[] layerMasksForCollisions;
    public LayerMask finishMask;
    public Transform groundChecker;
    bool isGrounded = false;
    bool isTouchingObstacle = false;
    bool isTouchingHitObstacle = false;
    public float checkGroundRadius = 0.4f;


    //gravity stuff
    Vector3 fallingVelocity;
    public float bounceForce = 100f;
    public float fallingSpeed=-5f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        CollisionDetectionMethod();

        fallingVelocity.y += fallingSpeed * Time.deltaTime;
        rb.velocity = fallingVelocity;

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
                    print("dead");
                    SceneManager.LoadScene(0);
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

    }

    private void CollisionDetectionMethod()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, checkGroundRadius, layerMasksForCollisions[0]);
        isTouchingObstacle = Physics.CheckSphere(groundChecker.position, checkGroundRadius, layerMasksForCollisions[1]);
        isTouchingHitObstacle = Physics.CheckSphere(groundChecker.position, checkGroundRadius, layerMasksForCollisions[2]);
    }
}
