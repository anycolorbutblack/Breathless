using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.UI;

public class ReadFromFile : MonoBehaviour {
    
    public string Location; //Variable for the location for the text file.
   
    public Text t;
    private string textString;


    void Start()
    {
        textString = ReadFile(Location);
    }
    void Update()
    {
        t.text = textString;
    }
    public string ReadFile(string Location) //read file function
    {
        TextAsset textr = Resources.Load(Location) as TextAsset; //Reads the text file from a certain location that is specified in Unity. Made for reuse.
        return textr.text;
        

    }
  
}

