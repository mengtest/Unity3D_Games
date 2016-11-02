using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

  
    public GameObject player;        // Reference to the player's position.

    private Vector3 offset;            // Reference to the current camera's position.

    void Start()
    {
        // Setting up the references.
        offset = transform.position;
    }

   void Update()
    {
        //Move camera relative to player with an offset.
        transform.position = player.transform.position + offset;
    }
}
