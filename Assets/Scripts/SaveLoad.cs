using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour
{
    public GameObject savedText;
    public GameObject errorText;
    public GameObject savedFilesText;
    public GameObject fileInput;

    List<GameObject> FindGameObjectsWithLayer(int layer)
    {
        var goArray = FindObjectsOfType<GameObject>();
        var goList = new List<GameObject>();
        for (var i = 0; i<goArray.Length; i++) {
            if (goArray[i].layer == layer) {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }
        return goList;
    }

    public void SaveData()
    {
        string DirPath = Application.dataPath + Path.AltDirectorySeparatorChar + "/Saves";
        string newPath = DirPath + "/" + fileInput.GetComponent<TMP_Text>().text + ".sav";
        if (fileInput.GetComponent<TMP_Text>().text.Length > 1)
        {
            if (File.Exists(newPath))
            {
                StartCoroutine(ShowErrorMessage("Such file name alredy exists", 2));
            }
            else
            {
                List<GameObject> objects = FindGameObjectsWithLayer(8);
                List<ItemData> objectRoots = new List<ItemData>();
                if(objects == null)
                {
                    StartCoroutine(ShowErrorMessage("No Items in scene to save", 2));
                }
                else
                {
                    foreach (GameObject obj in objects)
                    {
                        if (obj.transform.parent == null)
                        {
                            objectRoots.Add(new ItemData(obj));
                        }
                    }

                    FileStream fs = new FileStream(newPath, FileMode.Create);
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, objectRoots);
                    fs.Close();

                    StartCoroutine(ShowSavedMessage("Scene was saved as " + fileInput.GetComponent<TMP_Text>().text + ".sav", 2));
                    gameObject.SetActive(false);
                }
               
            }
        }
            
        else StartCoroutine(ShowErrorMessage("Please provide a name", 2));

       


    }
    IEnumerator ShowErrorMessage(string message, float delay)
    {
        errorText.GetComponent<TMP_Text>().text = message;
        yield return new WaitForSeconds(delay);
        errorText.GetComponent<TMP_Text>().text = "";
    }

    IEnumerator ShowSavedMessage(string message, float delay)
    {
        savedText.GetComponent<TMP_Text>().text = message;
        yield return new WaitForSeconds(delay);
        savedText.GetComponent<TMP_Text>().text = "";
    }
    public void ExitRoom()
    {
        GameObject saveManager = GameObject.Find("SaveManager");
        saveManager.GetComponent<SaveManager>().SetItems(null);
        SceneManager.LoadScene("MainMenuManager");
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("SceneLoaded");
        GameObject saveManager = GameObject.Find("SaveManager");
        if (!saveManager.GetComponent<SaveManager>().newRoom)
        {
            CreateItems(saveManager.GetComponent<SaveManager>().items);
        }
        //if (saveManager.GetComponent<SaveManager>().items.Count > 0)
        //{
        //    CreateItems(saveManager.GetComponent<SaveManager>().items);
        //}
        
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void CreateItems(List<ItemData> items)
    {
        foreach (var item in items)
        {
            GameObject obj = null;
            if (item.tag == "Item1")
            {
                obj = Instantiate(Resources.Load("Bed") as GameObject);
            }
            else if (item.tag == "Item2")
            {
                obj = Instantiate(Resources.Load("Desk") as GameObject);
            }
            else if (item.tag == "Item3")
            {
                obj = Instantiate(Resources.Load("Wardrobe") as GameObject);
            }
            if (obj != null)
            {
                obj.transform.position = new Vector3(item.position[0], item.position[1], item.position[2]);
                obj.transform.eulerAngles = new Vector3(item.rotation[0], item.rotation[1], item.rotation[2]);
                Color itemColor = new Color(item.color[0], item.color[1], item.color[2]); ;
                var renderers = obj.GetComponentsInChildren<MeshRenderer>();
                for (var i = 0; i < renderers.Length; i++)
                {
                    renderers[i].material.color = itemColor;
                }
                obj.GetComponent<ItemCollider>().ChangeOriginalColor(itemColor);
            }
        }
    }

    public void DisplaySavedList()
    {
        string DirPath = Application.dataPath + Path.AltDirectorySeparatorChar + "/Saves";
        if (!Directory.Exists(DirPath))
        {
            Directory.CreateDirectory(DirPath);
        }

        string[] saveFiles = Directory.GetFiles(DirPath, "*.sav", SearchOption.TopDirectoryOnly);
        string stringOfItems = "";
        foreach (var file in saveFiles)
        {
            stringOfItems += Path.GetFileNameWithoutExtension(file) + "\n";
        }
        savedFilesText.GetComponent<TMP_Text>().text ="Current saves:\n" + stringOfItems;
    }




}
