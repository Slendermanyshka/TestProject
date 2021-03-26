using UnityEngine;

public class CinemachineSwitcher : MonoBehaviour
{

    private Animator animator;
    private bool shootingPhaseCam = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SwitchState()
    {
        if (shootingPhaseCam)
        {
            animator.Play("RunningCam");
        }
        else
        {
            animator.Play("ShootingStageCamera");
        }
        shootingPhaseCam = !shootingPhaseCam;
    }

}
