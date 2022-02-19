using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;
public class ShakeScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public Shaker shaker;
    public ShakePreset preset;
    void Start()
    {
        shaker = Camera.main.GetComponent<Shaker>();
    }


    public void ShakeScreens()
    {
        shaker.Shake(preset);
    }
}
