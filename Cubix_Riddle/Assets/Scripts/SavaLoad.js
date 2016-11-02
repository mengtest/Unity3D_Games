#pragma strict

/*import System;
import System.IO;
import System.Runtime.Serialization.Formatters.Binary;

public var playerController : PlayerController;

public function SaveData()
{
    var bf : BinaryFormatter = new BinaryFormatter();
    var saveFile :FileStream = File.Create(Application.persistentDataPath+"/playerInfo.dat");

    var data : PlayerData = new PlayerData();
    data.score = score;

    bf.Serialize(saveFile, data);

    saveFile.Close();
}

public function LoadData()
{
    if(File.exists(Application.persistentDataPath+"/playerInfo.dat"))
    {
        var bf : BinaryFormatter = new BinaryFormatter();
        var file :FileStream = File.Open(Application.persistentDataPath+"/playerInfo.dat", FileMode.Open);

        var data : PlayerData = (PlayerData)bf.Deserialize(file);
        file.Close;

        score = data.score;
    }
   
}

@Serializable
public class PlayerData 
{
    public var score : int = playerContoller.score;
    //public var PositionX : float, PositionY, PositionZ;
}

*/