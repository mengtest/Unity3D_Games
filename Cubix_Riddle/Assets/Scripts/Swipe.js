#pragma strict

public var jump : float;                // Height at which palyer will jump. 

private var rb : Rigidbody;             // Reference to the player's rigidbody component .
private var fp : Vector2;               // Refrence to the first finger position.
private var lp : Vector2;               // Reference to the last finger position.
private var isGrounded : boolean;       // Whether player is on the ground or in the air.

function Start()
{
    // Setting up the references.
    rb = GetComponent.<Rigidbody>();
}

function Update()
{
    // If player is on ground ...
    if(isGrounded)
    {
        // ... for a touch input...
        for (var touch : Touch in Input.touches)
        {
            // ... start of the touch input...
            if (touch.phase == TouchPhase.Began)
            {
                // ... starting touching postion.
                fp = touch.position;
            }

             // ... end of the touch input...
            if (touch.phase == TouchPhase.Moved)
            {
                // ... final touching postion.
                lp = touch.position;
      
                // ... if up swipe..
                if((fp.y - lp.y) < -80)
                {
                    {
                        // ... add a force in y direction to jump.
                        rb.AddForce(new Vector3(0,jump,0));
                    }
               
                }
            }
        }
    }
    
}

function OnCollisionStay(col : Collision)
{
    // If player is on ground...
    if(col.gameObject.tag==("Ground"))
    {
        // ... set the boolen value to true.
        isGrounded = true;
    }
}

function OnCollisionExit(col : Collision)
{ 
    // If player was on ground...
    if(isGrounded)
    {
        // ... it's in air now.
        isGrounded = false;   
    }
}