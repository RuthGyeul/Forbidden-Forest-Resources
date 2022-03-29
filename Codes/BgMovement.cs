using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMovement : MonoBehaviour
{
    public string Direction = "H"; //background move direction
    public float Speed = 0.1f; //background move speed
    new Renderer renderer; //set renderer
    float move; //set move
    
    void Update()
    {
        renderer = GetComponent<Renderer>(); //get Renderer
        move += Time.deltaTime * Speed; //set speed with time
        if (Direction == "H")
        {
            renderer.material.mainTextureOffset = new Vector2(move, 0); //move background by x-axis
        }
        if (Direction == "V")
        {
            renderer.material.mainTextureOffset = new Vector2(0, move); //move background by y-axis
        }
    }
}
