using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{


    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            DestroyObject();
        }
    }
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
