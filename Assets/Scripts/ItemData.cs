using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string tag;
    public float[] position = new float[3];
    public float[] rotation = new float[3];
    public float[] color = new float[3];

    public ItemData(GameObject obj)
    {
        this.tag = obj.tag; ;
        this.position[0] = obj.transform.position.x;
        this.position[1] = obj.transform.position.y; 
        this.position[2] = obj.transform.position.z; 
        this.rotation[0] = Mathf.Round(obj.transform.eulerAngles.x);
        this.rotation[1] = Mathf.Round(obj.transform.eulerAngles.y); 
        this.rotation[2] = Mathf.Round(obj.transform.eulerAngles.z);
        this.color[0] = obj.GetComponent<ItemCollider>().GetOriginalColor().r;
        this.color[1] = obj.GetComponent<ItemCollider>().GetOriginalColor().g;
        this.color[2] = obj.GetComponent<ItemCollider>().GetOriginalColor().b;
    }

}
