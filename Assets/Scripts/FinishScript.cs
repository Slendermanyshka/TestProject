using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class FinishScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            StartCoroutine(WinninCoroutine(other));
        }
    }

    public void openDoorAnim()
    {
        GetComponent<Animation>().Play();
    }


    IEnumerator WinninCoroutine(Collider other) 
    {
        openDoorAnim();
        other.GetComponent<Rigidbody>().Sleep();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);

    }



}
