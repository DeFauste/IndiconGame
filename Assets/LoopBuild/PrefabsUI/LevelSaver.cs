using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public struct DataLvl
{
    public int numericLVl;
    public bool lockLvl;
    public bool doneLvl;
}
public class LevelSaver: MonoBehaviour
{
    public List<DataLvl> dataLvlList = new List<DataLvl>();

    private void Start()
    {
        Save();
    }
    public void Save()
    {
        dataLvlList.Add(new DataLvl { numericLVl = 1, lockLvl = true, doneLvl = false} );
        dataLvlList.Add(new DataLvl { numericLVl = 2, lockLvl = true, doneLvl = false} );
        dataLvlList.Add(new DataLvl { numericLVl = 3, lockLvl = true, doneLvl = false} );
        string json = JsonConvert.SerializeObject( dataLvlList, Formatting.Indented );
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Assets\\Resources", "Levels.json");
        if (File.Exists(filePath))
        {
            File.WriteAllText(filePath, json);
            Debug.Log(filePath);
        }
        else
        {
            Debug.Log(filePath);
        }
    }

    public void Load() 
    {
        
    }
    
}
