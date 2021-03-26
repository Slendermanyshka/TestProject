using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathPitScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Object.Destroy(other);
        SceneManager.LoadScene(0);
    }
}
