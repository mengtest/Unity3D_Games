#pragma strict
import UnityEngine.UI;

private var secretKey="12345678"; // Edit this value and make sure it's the same as the one stored on the server
var addScoreUrl="http://localhost/Score/AddScore3.php?"; //be sure to add a ? to your url
var highscoreUrl="http://localhost/Score/display3.php";    

private var highScoreKey : String = "HighScore";    // To store previous high score.
private var highScore: int; 

function Start() {
   // getScores();
    highScore = PlayerPrefs.GetInt(highScoreKey,0);
}

function Update(){
    if (Input.GetKeyDown(KeyCode.Escape)){
        postScore(highScore);
    }
}
 
function postScore(highscore) {
    //This connects to a server side php script that will add the name and score to a MySQL DB.
    // Supply it with a string representing the players name and the players score.

    var hash=Md5.Md5Sum(highscore + secretKey); 
 
    var highscore_url = addScoreUrl + "&highscore=" + highscore + "&hash=" + hash;
 
    // Post the URL to the site and create a download object to get the result.
   var hs_post = WWW(highscore_url);
    yield hs_post; // Wait until the download is done
    if(hs_post.error) {
        print("There was an error posting the high score: " + hs_post.error);
    }
}
 
// Get the scores from the MySQL DB to display in a GUIText.
/*function getScores() {
    gameObject.guiText.text = "Loading Scores";
   var hs_get = WWW(highscoreUrl);
    yield hs_get;
 
    if(hs_get.error) {
        print("There was an error getting the high score: " + hs_get.error);
    } else {
        gameObject.guiText.text = hs_get.text; // this is a GUIText that will display the scores in game.
    }
}*/