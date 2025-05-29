using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

/* This is the class that handles the File saving of the game
 * in binary.
 * 
 * The FileName is the file where the game data is stored.
 * 
 * The Save method handles the saving of all the important data
 * into the FileName.
 * 
 * The Load method handles the loading of the previously mentioned
 * data.
 */
public class Saver
{
    static string FileName = "/data.game";
    public static void Save() {
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
            Debug.LogException(e);
            return null;
        }
    }
}
