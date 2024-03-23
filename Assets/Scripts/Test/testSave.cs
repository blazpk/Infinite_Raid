using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class testSave
{
    public static void SaveData(CardObjects card)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "aaa.card";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, card);
        stream.Close();
    }

    public static CardObjects LoadData(CardObjects card)
    {
        string path = Application.persistentDataPath + "/" + card.cardID + ".card";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            card = (CardObjects)formatter.Deserialize(stream);
            stream.Close();

            return card;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
