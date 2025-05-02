using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
public class Saver
{
    static string FileName = "/data.game";
    public static void Save() {
        GameHandler.DefaultScene = "LevelScene";
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + FileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData();

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData Load() {
        string path = Application.persistentDataPath + FileName;

        try {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = bf.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        catch (FileNotFoundException e) {
            return null;
        }
    }
}
