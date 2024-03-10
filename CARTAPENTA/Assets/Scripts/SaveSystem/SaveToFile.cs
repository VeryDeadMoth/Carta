using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveToFile 
{
    string directory = "CollectedData";
    string file = "MyData.txt"; //make sure to write file type here (.txt or .csv)

    public SaveToFile()
    {
        Directory.CreateDirectory(Application.streamingAssetsPath + "/" + directory); // attempts to create the directory when savetofile object is created
    }

    public void SaveData(string myData)
    {
        File.AppendAllText(Application.streamingAssetsPath+"/"+directory+"/"+file,myData + ";" + DateTime.Now.ToString("yyyy-MM-dd\\THH:mm:ss\\Z") +"\n");
    }

}
