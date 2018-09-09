using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour
{

    //Veta vilka banor som är upplåsta
    //Veta tiden på alla banor
    //Låst upp sista gubben

    public static GameController gameController;

    public bool level_One_Unlocked = false;
    public bool level_Two_Unlocked = false;
    public bool level_Three_Unlocked = false;
    public bool level_Four_Unlocked = false;

    public float[] level_HighScore_Time;

    //public float level_One_Time;
    //public float level_Two_Time;
    //public float level_Three_Time;
    //public float level_Four_Time;

    public bool character_Two_Unlocked = false;

    // Use this for initialization
    void Awake()
    {
        if (gameController == null)
        {
            DontDestroyOnLoad(gameObject);
            gameController = this;
        }
        else if (gameController != this)
        {
            Destroy(gameObject);
        }
        level_HighScore_Time = new float[4];
        Load();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Save()
    {
        var binaryFormatter = new BinaryFormatter();
        var file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        var data = new PlayerData();
        #region VAR SAVE
        data.level_One_Unlocked = level_One_Unlocked;
        data.level_Two_Unlocked = level_Two_Unlocked;
        data.level_Three_Unlocked = level_Three_Unlocked;
        data.level_Four_Unlocked = level_Four_Unlocked;

        for (int i = 0; i < level_HighScore_Time.Length; i++)
        {
            data.level_HighScore_Time[i] = level_HighScore_Time[i];
        }

        data.character_Two_Unlocked = character_Two_Unlocked;

        #endregion
        binaryFormatter.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            var binaryFormatter = new BinaryFormatter();
            var file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            var data = (PlayerData)binaryFormatter.Deserialize(file);
            #region LOAD VAR
            level_One_Unlocked = data.level_One_Unlocked;
            level_Two_Unlocked = data.level_Two_Unlocked;
            level_Three_Unlocked = data.level_Three_Unlocked;
            level_Four_Unlocked = data.level_Four_Unlocked;

            for (int i = 0; i < level_HighScore_Time.Length; i++)
            {
                level_HighScore_Time[i] = data.level_HighScore_Time[i];
            }

            character_Two_Unlocked = data.character_Two_Unlocked;
            #endregion
        }
    }
}

[Serializable]
class PlayerData
{
    public static GameController gameController;

    public bool level_One_Unlocked;
    public bool level_Two_Unlocked;
    public bool level_Three_Unlocked;
    public bool level_Four_Unlocked;

    public float[] level_HighScore_Time = new float[4];

    public bool character_Two_Unlocked;

}