using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorFunction : MonoBehaviour
{
    public string sceneName;
    private bool haveKey = false;
    public GameObject clueUI;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && haveKey)
        {
            GameManagement.instance.WinRound(SceneManager.GetActiveScene().buildIndex);// build index of scene = levelscene;
            GameManagement.instance.LoadingScreen(sceneName);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool Iskey = other.GetComponentInParent<PlayerController>().haveKey;
            haveKey = Iskey;
            if (haveKey)
            {
                clueUI.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
         
            clueUI.SetActive(false);
        }
    }
}
