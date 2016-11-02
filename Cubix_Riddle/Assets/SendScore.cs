using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendScore : MonoBehaviour {

       private string secretKey = "12345678"; // Edit this value and make sure it's the same as the one stored on the server
       public string addScoreUrl = "http://localhost/Score/AddScore3.php?"; //be sure to add a ? to your url
       public string highscoreUrl = "http://localhost/Score/display3.php";

       private string highScoreKey= "HighScore";    // To store previous high score.
       private int highScore; 

       void Start()
       {
           //StartCoroutine(GetScores());
           highScore = PlayerPrefs.GetInt(highScoreKey, 0);
       }

       void Update()
       {
           if (Input.GetKeyDown(KeyCode.Escape))
           {
               PostScores(highScore);
           }
       }

       public string Md5Sum(string strToEncrypt)
       {
           System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
           byte[] bytes = ue.GetBytes(strToEncrypt);

           // encrypt bytes
           System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
           byte[] hashBytes = md5.ComputeHash(bytes);

           // Convert the encrypted bytes back to a string (base 16)
           string hashString = "";

           for (int i = 0; i < hashBytes.Length; i++)
           {
               hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
           }

           return hashString.PadLeft(32, '0');
       }

       // remember to use StartCoroutine when calling this function!
       IEnumerator PostScores(int highscore)
       {
           //This connects to a server side php script that will add the name and score to a MySQL DB.
           // Supply it with a string representing the players name and the players score.
           string hash = Md5Sum(highscore + secretKey);

           string post_url = addScoreUrl + "&highscore=" + highscore + "&hash=" + hash;

           // Post the URL to the site and create a download object to get the result.
           WWW hs_post = new WWW(post_url);
           yield return hs_post; // Wait until the download is done

           if (hs_post.error != null)
           {
               print("There was an error posting the high score: " + hs_post.error);
           }
       }

       // Get the scores from the MySQL DB to display in a GUIText.
       // remember to use StartCoroutine when calling this function!
    /*   IEnumerator GetScores()
       {
           gameObject.guiText.text = "Loading Scores";
           WWW hs_get = new WWW(highscoreURL);
           yield return hs_get;

           if (hs_get.error != null)
           {
               print("There was an error getting the high score: " + hs_get.error);
           }
           else
           {
               gameObject.guiText.text = hs_get.text; // this is a GUIText that will display the scores in game.
           }*/
}
