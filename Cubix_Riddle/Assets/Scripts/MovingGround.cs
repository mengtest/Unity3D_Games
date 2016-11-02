using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour {

    public float start;
    public float end;
    public float speed = 0.5f;

    private Vector3 pos1;
    private Vector3 pos2;
	
    void Start()
    {
        pos1 = transform.position;
        pos2 = transform.position;

        pos1.y =  end;
        pos2.y =  start;
    }
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
    }
}
