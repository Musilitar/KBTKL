using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class Persistence
{
    public static List<Game> saves = new List<Game>();

    public static void Save()
    {
        GatherInformation();
        saves.Add(Game.current);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/saves.mus");
        bf.Serialize(fs, Persistence.saves);
        fs.Close();
    }

    public static void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/saves.mus"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/saves.mus", FileMode.Open);
            Persistence.saves = (List<Game>) bf.Deserialize(fs);
            fs.Close();
            SetInformation(0);
        }
    }

    public static void GatherInformation()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Game.current.x = player.transform.position.x;
        Game.current.y = player.transform.position.y;
        Game.current.items = player.inventory.Items;
    }

    public static void SetInformation(int saveIndex)
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Game save = Persistence.saves[saveIndex];
        Game.current = save;
        player.transform.position = new Vector3(save.x, save.y);
        player.inventory.Items = save.items;
        player.inventory.LoadInventory();
    }
}
