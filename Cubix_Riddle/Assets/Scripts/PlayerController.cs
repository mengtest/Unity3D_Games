using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float jump;                            // Height at which player will jump.
    public float thrust;                           // Amount of thrust in direction of force.
    public float thres;                           // Bottom threshold after which player will be destroyed.
    public GameObject redCubeParticle;            // Reference to the exploding particle of the red cube.
    public GameObject playerExplode;              // Reference to the exploding particle of the player.
    public AudioClip collectCube;                  // Reference to the audio clip after collecting the cube.
    public AudioClip cubeExplode;                  // Reference to the audio clip when player get destroyed.
    public int highScore = 0;                     // To store current high score.
    public Text scoreText;                         // Reference to the text displayed after scoring points.
    public Text winText;                           // Reference to the text displayed after winning.
    public Text gameOverText;                     // Reference to the text displayed after game over.
    public Text restartText;                      // Reference to the text displayed for restarting.
    public Text portalText;
    public Button portalButton;
    public GameObject MP1, MP2, MP3, MP4, SP1, SP2, SP3, SP4;

    private float moveHorizontal;                // The name of the input axis for moving left and right.
    private float moveVertical;                  // The name of the input axis for moving forward and back.
    private string highScoreKey = "HighScore";    // To store previous high score.
    private Rigidbody rb;                          // Refrence to the player's rigidbody component.
    private Vector3 prePos;
    private ColorBlock actiColor;
    private int score;                            // Current score of the player in the game.
    private bool isGrounded;                    // Whether the player is on ground or in air.
    private bool canMove;                      // Whether the player can move or not.
    private AudioSource audioSource;               // Reference to the audio source in the game.

    void Start()
    {
        // Setting up the refernces.
        rb = GetComponent<Rigidbody>();
        audioSource = GameObject.Find("Audio").GetComponent<AudioSource> ();
        actiColor = portalButton.colors;
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        score = 0;
        SetScoreText();
        
        canMove = true;
        winText.text = "";
        gameOverText.text = "";
        restartText.text = "";
    }

    void FixedUpdate()
    {
        MainPortalActivate();
        // If player can move...
        if (canMove)
        {
            // ... and device is PC...
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                // ... take the input.
                moveHorizontal = Input.GetAxis("Horizontal");
                moveVertical = Input.GetAxis("Vertical");

                // ... force the player to move in the defined vector.
                rb.AddForce(new Vector3(moveHorizontal, 0, moveVertical) * thrust);

                // ... if player is on ground and space key is pressed...
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                {
                    // ... jump.
                    rb.AddForce(new Vector3(0, jump, 0));
                }

                // ... if player is below the ground...
                else if (transform.position.y < thres)
                {
                    // ... destroy the player.
                    Destroy(gameObject);

                    // ... game over.
                    GameOver();
                }

            }

            // ...devices with accelerometer...
            else
            {
                // ... move the player in the tilt direction of the device.
                rb.AddForce(new Vector3(Input.acceleration.x, 0.0f, Input.acceleration.y) * thrust);

                // ... if player is below the ground...
                if (transform.position.y < thres)
                {
                    // ... destroy the player.
                    Destroy(gameObject);

                    // ... game over.
                    GameOver();
                }
            }
        }
    }

    void OnCollisionStay(Collision col)
    {
        // If player is on ground ...
        if (col.gameObject.tag == ("Ground"))
        {
            // ... set the boolen value to true.
            isGrounded = true;
        }

    }

   void OnCollisionExit(Collision col)
    {
        // If player was on ground...
        if (isGrounded)
        {
            // ... currently it's in air.
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        // If the player is on the winning platform...
        if (col.gameObject.name == "WinArea")
        {
            // ... win.
            Win();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If the player is collided with green cube...
        if (other.gameObject.tag == "GreenCube")
        {
            // ... update the score.
            score += 1;
            SetScoreText();

            // ... play the clip for collecting the cube.
            audioSource.clip = collectCube;
            audioSource.Play();

            // ... destroy the cube.
            Destroy(other.gameObject);
        }

        // If the player is collided with yellow cube...
        else if (other.gameObject.tag == "YellowCube")
        {
            // ... update the score.
            score += 2;
            SetScoreText();

            // ... play the clip for collecting the cube.
            audioSource.clip = collectCube;
            audioSource.Play();

            // ... destroy the cube.
            Destroy(other.gameObject);
        }

        else if (other.gameObject.tag == "Negative1")
        {
            // ... update the score.
            score -= 1;
            SetScoreText();

            // ... play the clip for collecting the cube.
            audioSource.clip = collectCube;
            audioSource.Play();

            // ... destroy the cube.
            Destroy(other.gameObject);
        }

        else if (other.gameObject.tag == "Negative2")
        {
            // ... update the score.
            score -= 2;
            SetScoreText();

            // ... play the clip for collecting the cube.
            audioSource.clip = collectCube;
            audioSource.Play();

            // ... destroy the cube.
            Destroy(other.gameObject);
        }

        // If the player is collided with red cube...
        else if (other.gameObject.tag == "RedCube")
        {
            // ... instantiate the particle at the place of collision between red cube and player. 
            Instantiate(redCubeParticle, transform.position, Quaternion.identity);
            Instantiate(playerExplode, transform.position, Quaternion.identity);

            // ... play the clip for destorying the player.
            audioSource.clip = cubeExplode;
            audioSource.Play();

            // ... destroy the player and the collided red cube.
            Destroy(gameObject);
            Destroy(other.gameObject);

            // ... game over.
            GameOver();
        }

        else if (other.gameObject.tag == "MP1")
        {
            other.gameObject.SetActive(false);
            prePos = transform.position;
            transform.position = new Vector3(60, 0.6f, 15);
            SP1.gameObject.SetActive(true);
        }

        else if (other.gameObject.tag == "MP2")
        {
            other.gameObject.SetActive(false);
            prePos = transform.position;
            transform.position = new Vector3(-41, 0.6f, 58);
            SP2.gameObject.SetActive(true);
        }

        else if (other.gameObject.tag == "MP3")
        {
            other.gameObject.SetActive(false);
            prePos = new Vector3(4,0.6f,-3);
            transform.position = new Vector3(-50, 0.6f, 8);
            SP3.gameObject.SetActive(true);
        }

        else if (other.gameObject.tag == "MP4")
        {
            other.gameObject.SetActive(false);
            prePos = transform.position;
            transform.position = new Vector3(58, 0.6f, -15);
            SP4.gameObject.SetActive(true);
        }

        else if (other.gameObject.tag == "SP")
        {
            other.gameObject.SetActive(false);
            transform.position = new Vector3(prePos.x + 2,  prePos.y, prePos.z - 1);
        }
    }

    void SetScoreText()
    {
        // Show the updated score.
        scoreText.text = "Score : " + score.ToString();
    }

    void MainPortalActivate()
    {
        MP1.gameObject.SetActive(false);
        MP2.gameObject.SetActive(false);
        MP3.gameObject.SetActive(false);
        MP4.gameObject.SetActive(false);

        if (score > 7 && score < 10)
        {
            MP2.gameObject.SetActive(true);
            actiColor.disabledColor = Color.green;
            portalButton.colors = actiColor;
            portalText.text = "MAIN PORTAL\n ACTIVATED";

        }
        else if (score > 13 && score < 17)
        {
            MP3.gameObject.SetActive(true);
            actiColor.disabledColor = Color.green;
            portalButton.colors = actiColor;
            portalText.text = "MAIN PORTAL\n ACTIVATED";
        }
        else if (score > 27 && score < 34)
        {
            MP4.gameObject.SetActive(true);
            actiColor.disabledColor = Color.green;
            portalButton.colors = actiColor;
            portalText.text = "MAIN PORTAL\n ACTIVATED";
        }
        else if (score > 36  && score < 41)
        {
            MP1.gameObject.SetActive(true);
            actiColor.disabledColor = Color.green;
            portalButton.colors = actiColor;
            portalText.text = "MAIN PORTAL\n ACTIVATED";
        }
        else
        {
            actiColor.disabledColor = Color.red;
            portalButton.colors = actiColor;
            portalText.text = "MAIN PORTAL\n DEACTIVATED";
        }
    }

    void Win()
    {
        // Update the score.
        score += 10;
        SetScoreText();

        // Display the win text.
        winText.text = "You Win!";

        // Stop the player movement completely.
        canMove = false;
        rb.Sleep();
    }

    void GameOver()
    {
        // Stop accessing the camera.
        GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = false;
        canMove = false;

        // Display the game over text.
        gameOverText.text = "Game Over!";

        // If the device is PC...
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            // ... display the restart text.
            restartText.text = " Press 'R' for Restart";
        }

        // Otherwise
        else
        {
            // ... display the restart text.
            restartText.text = " Tap to Retry";
        }
    }

    void OnDisable()
    {
        //If our scoree is greter than highscore, set new higscore and save.
        if (score > highScore)
        {
            // ... Save the highscore.
            PlayerPrefs.SetInt(highScoreKey, score);
            PlayerPrefs.Save();
        }
    }
}
