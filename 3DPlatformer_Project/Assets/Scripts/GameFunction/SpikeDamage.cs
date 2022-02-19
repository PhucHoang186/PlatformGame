using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
  
    private bool hadTouch = false;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(!hadTouch)
            {
                GameManagement.instance.isLoss = true;
                GameManagement.instance.PauseGame();
                hadTouch = true;
                
            }
        }
    }
}