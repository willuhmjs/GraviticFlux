using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManagement
{
    public static void SaveSettings(SettingsData data) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SettingsData LoadSettings() {
        string path = Application.persistentDataPath + "/settings.dat";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            // deseralize into new SettingsData object
            SettingsData settingsData = formatter.Deserialize(stream) as SettingsData;
            stream.Close();
            return settingsData;
        } else {
            return new SettingsData();
        }
    }

    // saves the latest level, and returns its value
    public static void SaveLatestLevel() {
        SettingsData data = LoadSettings();
        if (SceneManager.GetActiveScene().buildIndex > data.latestLevel) data.latestLevel = SceneManager.GetActiveScene().buildIndex;
        SaveSettings(data);
    } 
}