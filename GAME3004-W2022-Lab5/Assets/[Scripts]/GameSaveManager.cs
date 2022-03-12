using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

[System.Serializable]
class SaveData
{
    public float[] playerPosition;
    public float[] playerRotation;

    public SaveData()
    {
        playerPosition = new float[3]; // Create empty container
        playerRotation = new float[3]; // Create empty container
    }
    
}

public class GameSaveManager : MonoBehaviour
{
    public Transform player;
    private void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
        SaveData data = new SaveData();
        data.playerPosition[0] = player.position.x;
        data.playerPosition[1] = player.position.y;
        data.playerPosition[2] = player.position.z; 
        
        data.playerRotation[0] = player.localEulerAngles.x;
        data.playerRotation[1] = player.localEulerAngles.y;
        data.playerRotation[2] = player.localEulerAngles.z;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

    private void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            var x = data.playerPosition[0];
            var y = data.playerPosition[1];
            var z = data.playerPosition[2];

            var Rotx = data.playerRotation[0];
            var Roty = data.playerRotation[1];
            var Rotz = data.playerRotation[2];

            player.gameObject.GetComponent<CharacterController>().enabled = false;
            player.position = new Vector3(x, y, z);
            player.rotation = Quaternion.Euler(Rotx, Roty, Rotz);
            player.gameObject.GetComponent<CharacterController>().enabled = true;

            Debug.Log("Game data loaded");
        }
        else
        {
            Debug.LogError("There is no save data!");
        }
    }

    void ResetData()
    {
        if(File.Exists(Application.persistentDataPath + "/MySaveData.data"))
        {
            File.Delete(Application.persistentDataPath + "/MySaveData.dat");
            Debug.Log("Data reset complete!");
        }
        else
        {
            Debug.LogError("No save data to delete.");
        }
    }

    public void OnSaveButton_Pressed()
    {
        SaveGame();
    }

    public void OnLoadButton_Pressed()
    {
        LoadGame();
    }    
}
