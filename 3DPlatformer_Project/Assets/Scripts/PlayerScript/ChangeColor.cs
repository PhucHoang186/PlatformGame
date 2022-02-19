using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{    // Obstacle Renderer Material color;
    private Color obstacleColor;
    private Color obstacleEmissionColor;
    public PlayerController controller;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            controller.isGround = true;
            ChangeMaterialColor(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        controller.isGround = false;
        if (other.gameObject.CompareTag("Ground"))
        {
            ReverseMaterialColor(other);
        }
    }

    private void ChangeMaterialColor(Collider other)
    {
        //Get the default color of obstacle
        obstacleColor = other.gameObject.GetComponent<Renderer>().material.color;
        obstacleEmissionColor = other.gameObject.GetComponent<Renderer>().material.GetColor("_EmissionColor");
        //apply color to obstacle
        other.gameObject.GetComponent<Renderer>().material.color = Color.white;
        other.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f) * 4f);
        other.gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }
    private void ReverseMaterialColor(Collider other)
    {
        //Return back the origin color
        other.gameObject.GetComponent<Renderer>().material.color = obstacleColor;
        other.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", obstacleEmissionColor);
    }
}
