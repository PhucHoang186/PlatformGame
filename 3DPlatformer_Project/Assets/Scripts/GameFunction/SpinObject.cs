using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public Vector3 rotation;
    public float rotateSpeed = 40f;
    void Update()
    {
        transform.Rotate(rotation * rotateSpeed * Time.deltaTime);
    }
}
