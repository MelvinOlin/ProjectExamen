using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour
{

    public static GameController gameController;

    public bool[] level_Unlocked;

    public float[] level_HighScore_Time;

    public bool character_Two_Unlocked = false;

    // Use this for initialization
    void Awake()
    {
        Debug.Log(Application.persistentDataPath);
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
        level_Unlocked = new bool[5];
        Load();
        Debug.Log(Application.persistentDataPath);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Delete();
        }
    }

    public void Save()
    {
        var binaryFormatter = new BinaryFormatter();
        var file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        var data = new PlayerData();
        #region VAR SAVE

        for (int i = 0; i < level_Unlocked.Length; i++)
        {
            data.level_Unlocked[i] = level_Unlocked[i];
        }
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
            for (int i = 0; i < level_Unlocked.Length; i++)
            {
                level_Unlocked[i] = data.level_Unlocked[i];
            }

            for (int i = 0; i < level_HighScore_Time.Length; i++)
            {
                level_HighScore_Time[i] = data.level_HighScore_Time[i];
            }

            character_Two_Unlocked = data.character_Two_Unlocked;
            #endregion
        }
    }

    public void Delete()
    {
        try
        {
            File.Delete(Application.persistentDataPath + "/playerInfo.dat");
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}

[Serializable]
class PlayerData
{

    public bool[] level_Unlocked = new bool[5];

    public float[] level_HighScore_Time = new float[4];

    public bool character_Two_Unlocked;

}