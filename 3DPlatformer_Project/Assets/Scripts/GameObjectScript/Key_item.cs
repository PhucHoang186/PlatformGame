using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_item : Item
{
    
    public override void OnTriggerEnter(Collider other)
    {
        other.GetComponentInParent<PlayerController>().haveKey = true;
        base.OnTriggerEnter(other);
        
    }
}
