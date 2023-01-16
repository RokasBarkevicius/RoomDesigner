using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    Button loadButton;
    public GameObject MainCanvas;
    public GameObject LoadCanvas;
    string DirPath;
    public GameObject dropdown;
    GameObject saveManager;
    private void Start()
    {
        saveManager = GameObject.Find("SaveManager");
        DirPath = Application.dataPath + Path.AltDirectorySeparatorChar + "/Saves";
        HideLoadCanvas();
        loadButton = GameObject.Find("LoadRoomButton").GetComponent<Button>();
        CheckData();
    }
    public void NewScene()
    {
        saveManager.GetComponent<SaveManager>().SetNewRoom(true);
        SceneManager.LoadScene("GameScene");
    }
    public void LoadScene()
    {
        LoadData();
        saveManager.GetComponent<SaveManager>().SetNewRoom(false);
        SceneManager.LoadScene("GameScene");

    }

    public void LoadData()
    {
        string file = dropdown.GetComponent<TMP_Dropdown>().options[dropdown.GetComponent<TMP_Dropdown>().value].text;
        string path = DirPath +"/"+ file + ".sav";
        using (Stream stream = File.Open(path, FileMode.Open))
        {
            var bformatter = new BinaryFormatter();

            List<ItemData> items = (List<ItemData>)bformatter.Deserialize(stream);
            saveManager.GetComponent<SaveManager>().SetItems(items);

        }

    }

    public void CheckData()
    {
       
        int fileCount = Directory.GetFiles(DirPath, "*.sav", SearchOption.TopDirectoryOnly).Length;
        if (fileCount==0)
        {
            loadButton.interactable = false;
        }

    }

    public void QuitGame()
    {
        //for built game
        Application.Quit();
        //for unity editor
        UnityEditor.EditorApplication.isPlaying = false;
    }
    public void DisplayDropdown()
    {
        
        string[] saveFiles = Directory.GetFiles(DirPath, "*.sav", SearchOption.TopDirectoryOnly);

        List<string> fileList = new List<string>();
        foreach (var file in saveFiles)
        {
            fileList.Add(Path.GetFileNameWithoutExtension(file));
        }


        dropdown.GetComponent<TMP_Dropdown>().ClearOptions();

        dropdown.GetComponent<TMP_Dropdown>().AddOptions(fileList);
    }

    public void HideMainCanvas()
    {
        MainCanvas.SetActive(false);
    }
    public void ShowMainCanvas()
    {
        MainCanvas.SetActive(true);
    }

    public void HideLoadCanvas()
    {
        LoadCanvas.SetActive(false);
    }
    public void ShowLoadCanvas()
    {
        LoadCanvas.SetActive(true);
    }
}
