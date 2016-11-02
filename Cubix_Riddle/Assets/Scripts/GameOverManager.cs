using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameOverManager : MonoBehaviour {

public GameObject pauseMenu ;             // Reference to the pause Menu.
//public int currentScore;
//public PlayerController playerController;

    void Start()
    {
        // Disable the pause menu.
        pauseMenu.SetActive(false);
        //playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        // If the device is PC...
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            // ... if player has been destroyed...
            if (GameObject.Find("Player") == null)
            {
                // ... and r key is pressed...
                if (Input.GetKeyDown(KeyCode.R))
                {
                    // ... reload the current scene.
                    SceneManager.LoadScene("minigame");
                }

                // ... and escape key is pressed...
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    // ... load the menu scene (go to the main menu).
                    SceneManager.LoadScene("Menu");
                }
            }
        }

        // If the palyer is active in the scene and escape key is pressed...
        if (GameObject.Find("Player") != null && Input.GetKeyDown(KeyCode.Escape))
        {
            // ... open the pause menu.
            pauseMenuPanel();
        }

        // If player has been destroyed and to have touch input...
        if (GameObject.Find("Player") == null)
        {
            // ...and touch the screen atleast once...
            if (Input.touchCount >= 1)
            {
                // ... reload the current scene.
                SceneManager.LoadScene("minigame");
            }

            // ... and escape key is pressed...
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // ... load the menu scene (go to the main menu).
                SceneManager.LoadScene("Menu");
            }
        }
    }

    void pauseMenuPanel()
    {
        // Disable the player's controls.
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;

        // Open the pause menu.
        pauseMenu.SetActive(true);

        // Stop the player's motion completely.
        GameObject.Find("Player").GetComponent<Rigidbody>().Sleep();
    }

    public void Resume()
    {
        // Enable the player's controls.
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;

        // Close the pause menu.
        pauseMenu.SetActive(false);
    }

/*    public void SaveData()
    {
       BinaryFormatter bf = new BinaryFormatter();
       FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        data.currentScore = playerController.score;

        bf.Serialize(file, data);

        file.Close();
    }

    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);

            file.Close();

            playerController.score = data.currentScore;
        }

    }*/


    public void QuitMainMenu(String name)
    {
        // Quit to main menu.
        SceneManager.LoadScene(name);
    }

    [Serializable]
    public class PlayerData
    {
        public int currentScore;
        //public var PositionX : float, PositionY, PositionZ;
    }
}
