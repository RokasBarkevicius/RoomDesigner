using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public List<ItemData> items;
    public bool newRoom;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        newRoom = false;
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        
    }
    public void SetItems(List<ItemData> itemList)
    {
        items = itemList;
    }

    public void SetNewRoom(bool value)
    {
        newRoom = value;
    }

}
