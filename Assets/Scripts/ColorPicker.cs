using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public bool on = false;

    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    public GameObject redText;
    public GameObject greenText;
    public GameObject blueText;

    public GameObject itemController;
    public GameObject colorPreviewPanel;

    GameObject item;
    Color itemColor;


    void Update()
    {
        if (on)
        {
            item = itemController.GetComponent<ItemController>().GetCurrentItem();
            itemColor = item.GetComponent<ItemCollider>().GetOriginalColor();
            redText.GetComponent<TMP_Text>().text = "Red: " + itemColor.r;
            redSlider.value = itemColor.r;
            greenText.GetComponent<TMP_Text>().text = "Green: " + itemColor.g;
            greenSlider.value = itemColor.g;
            blueText.GetComponent<TMP_Text>().text = "Blue: " + itemColor.b;
            blueSlider.value = itemColor.b;
            colorPreviewPanel.GetComponent<Image>().color = itemColor;
            on = false;
        }
        ChangedRedValue();
        ChangedGreenvalue();
        ChangedBlueValue();
    }


    public void ChangedRedValue()
    {
        redText.GetComponent<TMP_Text>().text = "Red: " + redSlider.value;
        itemColor.r = redSlider.value;
        ChangePanelColor();
    }
    public void ChangedGreenvalue()
    {
        greenText.GetComponent<TMP_Text>().text = "Green: " + greenSlider.value;
        itemColor.g = greenSlider.value;
        ChangePanelColor();
    }
    public void ChangedBlueValue()
    {
        blueText.GetComponent<TMP_Text>().text = "Blue: " + blueSlider.value;
        itemColor.b = blueSlider.value;
        ChangePanelColor();
    }

    public void ChangePanelColor()
    {
        colorPreviewPanel.GetComponent<Image>().color = itemColor;
    }

    public void applyColor()
    {
        var renderers = item.GetComponentsInChildren<MeshRenderer>();
        for (var i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = itemColor;
        }
        item.GetComponent<ItemCollider>().ChangeOriginalColor(itemColor);
        itemController.GetComponent<ItemController>().colorChosen = true;
        gameObject.SetActive(false);
        on = false;
    }
}
