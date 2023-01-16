using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject itemChoiceCanvas;
    public GameObject tooltipsCanvas;
    public GameObject colorChoiceCanvas;
    public GameObject optionsCanvas;
    public GameObject saveCanvas;

    public GameObject cameraText;

    ItemController ItemController;
    void Start()
    {
        ItemController = GameObject.Find("ItemController").GetComponent<ItemController>();
        HideItemChoiceCanvas();
        HideColorChoiceCanvas();
        HideOptionsCanvas();
        HideSaveCanvas();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !colorChoiceCanvas.activeSelf && !optionsCanvas.activeSelf && !saveCanvas.activeSelf)
        {
            if (itemChoiceCanvas.activeSelf)
                HideItemChoiceCanvas();
            else
            {
                ItemController.DestroyHeldItem();
                ShowItemChoiceCanvas();
               
            }
                
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !colorChoiceCanvas.activeSelf && !itemChoiceCanvas.activeSelf && !saveCanvas.activeSelf)
        {
            if (optionsCanvas.activeSelf)
                HideOptionsCanvas();
            else
            {
                ItemController.DestroyHeldItem();
                ShowOptionsCanvas();
            }
        }

    }

    public void HideItemChoiceCanvas()
    {
        itemChoiceCanvas.SetActive(false);
    }
    public void ShowItemChoiceCanvas()
    {
        itemChoiceCanvas.SetActive(true);
    }

    public void HideColorChoiceCanvas()
    {
        colorChoiceCanvas.SetActive(false);
    }
    public void ShowColorChoiceCanvas()
    {
        colorChoiceCanvas.SetActive(true);
    }

    public void HideOptionsCanvas()
    {
        optionsCanvas.SetActive(false);
    }
    public void ShowOptionsCanvas()
    {
        optionsCanvas.SetActive(true);
    }

    public void HideSaveCanvas()
    {
        saveCanvas.SetActive(false);
    }
    public void ShowSaveCanvas()
    {
        saveCanvas.SetActive(true);
    }
}
