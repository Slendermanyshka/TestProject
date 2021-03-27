using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class FinishScript : MonoBehaviour
{

    [SerializeField] GameObject winText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            other.GetComponentInParent<PlayerGeneralController>().joystickGameObject.SetActive(false);
            other.GetComponentInParent<PlayerGeneralController>().StopMoving();
            StartCoroutine(WinninCoroutine(other));

        }
    }

    public void openDoorAnim()
    {
        GetComponent<Animation>().Play();
    }

    IEnumerator WinninCoroutine(Collider other) 
    {
        winText.SetActive(true);
        other.GetComponent<Rigidbody>().Sleep();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);

    }
}
