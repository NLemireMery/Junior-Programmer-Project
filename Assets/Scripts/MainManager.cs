using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    /* this code creates a static member to the MainManager class */
    /* Static class member declaration: values stored in this class member will be shared by all the instances of that class.
    For example, if there were ten instances of MainManager in your scene, they would all share the same value stored in Instance. */
    public static MainManager Instance;

    public Color TeamColor;

    /* This first line of code stores “this” in the class member Instance — the current instance of MainManager.
    You can now call MainManager.Instance from any other script without needing to have a reference to it.
    The second line of code marks the MainManager GameObject attached to this script not to be destroyed when the scene changes. */
    private void Awake()
    {
        /* New code to avoid multiple instances of MainManager:
        This pattern is called a singleton. You use it to ensure that only a single instance of the MainManager can ever exist,
        so it acts as a central point of access. */
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadColor();
    }

    /* Serialization is the action of converting complex data into a format in which it can be stored */
    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        /* Create a new instance of the SaveData and filled its TeamColor member with the one saved in the MainManager */
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;

        /* Transform the SaveData instance to JSON */
        string json = JsonUtility.ToJson(data);

        /* Write a string to the file: first parameter is the path to the file. The Unity method will give you a folder where you can save data
        that will survive between application reinstall or update and append to it the filename savefile.json
        Second parameter is the text you want to write in the file, here your JSON */
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor = data.TeamColor;
        }
    }
}
