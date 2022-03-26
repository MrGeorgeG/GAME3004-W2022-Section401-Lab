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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            SaveGame();
        }
        
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
    }

    private void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
        SaveData data = new SaveData();
        data.playerPosition = new[] { player.position.x, player.position.y, player.position.z };
        data.playerRotation = new[] { player.rotation.eulerAngles.x, player.rotation.eulerAngles.y, player.rotation.eulerAngles.z };
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
            player.gameObject.GetComponent<CharacterController>().enabled = false;
            player.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
            player.rotation = Quaternion.Euler(data.playerRotation[0], data.playerRotation[1], data.playerRotation[2]);
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
