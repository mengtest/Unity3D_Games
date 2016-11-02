using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour {

    public float timeDelay = 0.3f;        // Duration before loading next scene.

    private float timer;                  // Timer for counting up load the next scene.


    void Update()
    {
        // Updating the timer value.
        timer += Time.deltaTime;

        // If timee value is greater than the duration value...
        if (timer > timeDelay)
        {
            // ... load next scene.
            SceneManager.LoadScene("Menu");
        }
    }
}
