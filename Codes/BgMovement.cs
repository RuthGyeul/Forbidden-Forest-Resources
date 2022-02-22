using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMovement : MonoBehaviour
{
    public float BgSp = 1f; //Background move speed
    float move; //set move
    new Renderer renderer; //set renderer
    
    void Start()
    {
        renderer = GetComponent<Renderer>(); //get Renderer
    }

    void Update()
    {
        move += Time.deltaTime * BgSp; //set speed with time
        renderer.material.mainTextureOffset = new Vector2(move, 0); //move background by x-axis
    }
}
