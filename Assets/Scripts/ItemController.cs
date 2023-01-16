using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemController : MonoBehaviour
{
    GameObject item;
    bool moveItem = false;
    bool gotSize = false;
    public bool colorChosen = true;
    bool gridOn = false;

    public Vector3 itemSize = new Vector3(1, 1, 1);
    public Vector3 worldPos;
    public LayerMask layersToHit_placement;
    public LayerMask layersToHit_selection;
    public LayerMask layersToHit_other;


    public GameObject itemText;
    public GameObject warningText;

    public GameObject ColorChoiceCanvas;
    public GameObject itemChoiceCanvas;
    public GameObject optionsCanvas;
    public GameObject saveCanvas;

    private Grid grid;

    void Start()
    {
        itemText.GetComponent<TMP_Text>().text = "";
        warningText.GetComponent<TMP_Text>().text = "";
        grid = FindObjectOfType<Grid>();
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(moveItem && colorChosen)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                enableDisableGrid();
            }

            ItemPlacement(ray);
        }
        else
        {
            itemControl(ray);
        }
    }
    public void enableDisableGrid()
    {
        if (gridOn)
        {
            gridOn = false;
        }
        else
        {
            gridOn = true;
        }
    }
    public void itemControl(Ray ray)
    {
        if (IsCanvasActive()) return;
        if (Physics.Raycast(ray, out RaycastHit hitD, 100, layersToHit_other))
        {
            itemText.GetComponent<TMP_Text>().text = "Press Tab to select item";
        }
        if (Physics.Raycast(ray, out RaycastHit hitData, 100, layersToHit_selection))
        {
            var obj = hitData.collider.gameObject;
            
            itemText.GetComponent<TMP_Text>().text = "Press Q to destroy item\nPress M to move item\nPress C to change item color";
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if(obj.transform.parent != null)
                {
                    Destroy(obj.transform.parent.gameObject);
                }
                else Destroy(obj);
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (obj.transform.parent != null)
                {
                    item = obj.transform.parent.gameObject;
                }
                else item = obj; ;
                moveItem = true;
                item.GetComponent<ItemCollider>().isActive = true;
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (obj.transform.parent != null)
                {
                    item = obj.transform.parent.gameObject;
                }
                else item = obj;
                ColorChoiceCanvas.SetActive(true);
                ColorChoiceCanvas.GetComponent<ColorPicker>().on = true;
            }
        }
    }
    public void CreateItem(GameObject itemPrefab)
    {

        item = Instantiate(itemPrefab);
        moveItem = true;
        item.GetComponent<ItemCollider>().isActive = true;
        ColorChoiceCanvas.SetActive(true);
        ColorChoiceCanvas.GetComponent<ColorPicker>().on = true;

        colorChosen = false;
    }


    public void DestroyHeldItem()
    {
        if (item != null && item.GetComponent<ItemCollider>().isActive)
        {
            item.GetComponent<ItemCollider>().DestroyHeldItem();
            moveItem = false;
            colorChosen = true;
            ColorChoiceCanvas.GetComponent<ColorPicker>().on = false;
            gotSize = false;
        }

    }
    void LimitPositionItem(GameObject obj, float x,float y, float z)
    {
        if (obj.transform.position.x > x)
            obj.transform.position = new Vector3(x, obj.transform.position.y, obj.transform.position.z);
        else if (obj.transform.position.x < -x)
            obj.transform.position = new Vector3(-x, obj.transform.position.y, obj.transform.position.z);
        if (obj.transform.position.z > z)
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, z);
        else if (obj.transform.position.z < -z)
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, -z);

        if (obj.transform.position.y > y)
            obj.transform.position = new Vector3(obj.transform.position.x, y, obj.transform.position.z);
        else if (obj.transform.position.y < -y)
            obj.transform.position = new Vector3(obj.transform.position.x, y, obj.transform.position.z);
    }


    void ItemPlacement(Ray ray)
    {
        itemText.GetComponent<TMP_Text>().text = "Press LMB to place item\nPress G to turn grid on/off\nPress R to rotate item";
        if (Physics.Raycast(ray, out RaycastHit hitData, 100, layersToHit_placement))
        {
            worldPos = hitData.point;
        }

        if (!gotSize)
        {
            var collider = item.GetComponent<BoxCollider>();

            itemSize = collider.size;
            gotSize = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            item.transform.Rotate(0, 90, 0);
            itemSize = new Vector3(itemSize.z, itemSize.y, itemSize.x);
        }

        if (!gridOn)
        {
            item.transform.position = new Vector3(worldPos.x, (float)(worldPos.y + itemSize.y / 2), worldPos.z);
        }
        else
        {
            var gridPos = grid.GetNearestPointOnGrid(worldPos);
            item.transform.position = new Vector3(gridPos.x, (float)(gridPos.y + itemSize.y / 2), gridPos.z);
        }



        LimitPositionItem(item, 5 - itemSize.x / 2, itemSize.y / 2, 5 - itemSize.z / 2);

        if (Input.GetMouseButtonDown(0))
        {
            if (item.GetComponent<ItemCollider>().isCollision)
            {
                StartCoroutine(ShowWarningMessage("Can't place: collision with other item", 2));
            }
            else
            {
                warningText.GetComponent<TMP_Text>().text = "";
                itemText.GetComponent<TMP_Text>().text = "";
                moveItem = false;
                gotSize = false;
                gridOn = false;
                item.GetComponent<ItemCollider>().isActive = false;
            }
                
        }
    }

    IEnumerator ShowWarningMessage(string message, float delay)
    {
        warningText.GetComponent<TMP_Text>().text = message;
        yield return new WaitForSeconds(delay);
        warningText.GetComponent<TMP_Text>().text = "";
    }

    public GameObject GetCurrentItem()
    {
        return item;
    }

    public bool IsCanvasActive() 
    {
        return ColorChoiceCanvas.activeSelf || itemChoiceCanvas.activeSelf || optionsCanvas.activeSelf || saveCanvas.activeSelf;
    }
    
}
