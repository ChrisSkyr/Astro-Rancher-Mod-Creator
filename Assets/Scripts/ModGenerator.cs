using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class ModGenerator : MonoBehaviour
{
    [SerializeField] private string ModName;
    [SerializeField] private string dir;
    [SerializeField] private TMP_InputField Name_Input;
    [SerializeField] private TMP_InputField Directory_Input;
    public const string ModSaveDirectory = "/Mods/";
    private int charLimit = 16;

    private void Awake()
    {
        Name_Input.characterLimit = charLimit;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateFiles()
    {
        ModName = Name_Input.text;
        dir = Directory_Input.text;

        if (dir == null)
        {
            dir = Application.persistentDataPath + ModSaveDirectory;
            Debug.Log("The Mod will be saved on " + dir);
        }

        
        Debug.Log(ModName + "The mod " + ModName + " is starting the creation procedure.");
        Process createSLN = new Process();
        createSLN.StartInfo.WorkingDirectory = dir;
        createSLN.StartInfo.FileName = "dotnet";
        createSLN.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        createSLN.StartInfo.Arguments = "new sln --output " + ModName;
        createSLN.Start();
        Debug.Log("The " + ModName + " created.");
        createSLN.WaitForExit();
        Process createCLASSLIB = new Process();
        createCLASSLIB.StartInfo.WorkingDirectory = dir+@"\"+ModName;
        createCLASSLIB.StartInfo.FileName = "dotnet";
        createCLASSLIB.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        createCLASSLIB.StartInfo.Arguments = "new classlib -o " + ModName;
        createCLASSLIB.Start();
        createCLASSLIB.WaitForExit();
        Process AddCLASSLIB = new Process();
        AddCLASSLIB.StartInfo.WorkingDirectory = dir + "/" + ModName;
        AddCLASSLIB.StartInfo.FileName = "dotnet";
        AddCLASSLIB.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        AddCLASSLIB.StartInfo.Arguments = "sln " + ModName+".sln add "+ModName+@"\"+ModName+".csproj";
        AddCLASSLIB.Start();

    }

    public void CreateMod()
    {
        if(Name_Input == null)
        {
            Debug.LogWarning("Name is required!");
            return;
        }
        else
        {
            GenerateFiles();
        }
            
        
        
        
        
    }
}
