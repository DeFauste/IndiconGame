using System.Collections.Generic;
using UnityEngine;


public struct DataLvl
{
    public int numericLVl;
    public bool lockLvl;
    public bool doneLvl;
}
public class LevelSaver
{
    public List<DataLvl> dataLvlList;

    private void Start()
    {
        Save();
    }
    public void Save()
    {
        dataLvlList.Add(new DataLvl { numericLVl = 1, lockLvl = true, doneLvl = false} );
        dataLvlList.Add(new DataLvl { numericLVl = 2, lockLvl = true, doneLvl = false} );
        dataLvlList.Add(new DataLvl { numericLVl = 3, lockLvl = true, doneLvl = false} );
        //string json = JsonConvert.SerializeObject(dataLvlList, Formatting.Indented);
        //Debug.Log( json );
    }

    public void Load() 
    {
        
    }
    
}
