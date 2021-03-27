using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathPitScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().DeathSequence();
        }
        else { return; }

    }
}
