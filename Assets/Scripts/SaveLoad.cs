using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/*

for saving game data, such as scores

 */
public static class SaveLoad
{
    
    public static int highScore = 0; //scores for completed games

    

    #region high score
    public static void SaveHighScore(int score)
    {
        highScore = score;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/highscore.gd");
        bf.Serialize(file, SaveLoad.highScore);
        file.Close();

    }
    //loading data from file. rn only loads results from solo games
    public static int LoadHighScore()
    {
        if (File.Exists(Application.persistentDataPath + "/highscore.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/highscore.gd", FileMode.Open);
            SaveLoad.highScore = (int)bf.Deserialize(file);
            file.Close();

            return highScore;
        }
        return 0;
    }
    #endregion
    // public static void DeleteFile(string toDelete)
    // {
    //     if(toDelete=="gameState"){
    //         File.Delete(Application.persistentDataPath + "/gameStateSheet.gd");
    //         File.Delete(Application.persistentDataPath + "/gameStateOther.gd");
    //     }
    // }
}
