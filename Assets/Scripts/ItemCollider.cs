using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollider : MonoBehaviour
{
    public bool isCollision = false;
    int collisionCount;
    Color originalColor;
    Material originalMaterial;
    public Material collisionMaterial;
    public bool isActive = false;
    void Start()
    {
        originalMaterial = GetComponentInChildren<MeshRenderer>().material;
        originalColor = originalMaterial.color;
    }

    void Update()
    {
        if (collisionCount > 0)
        {
            isCollision = true;
        }
        else isCollision = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item1" || collision.gameObject.tag == "Item2" || collision.gameObject.tag == "Item3")
        {
            collisionCount++;
            if (isActive && collisionCount>0)
            {
                var renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
                for (var i = 0; i < renderers.Length; i++)
                    renderers[i].material = collisionMaterial;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Item1" || collision.gameObject.tag == "Item2" || collision.gameObject.tag == "Item3")
        {
            collisionCount--;
            if (collisionCount == 0 && isActive)
            {
                var renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
                for (var i = 0; i < renderers.Length; i++)
                    renderers[i].material = originalMaterial;
            }
            
        }
        
    }

    public void ChangeOriginalColor(Color color)
    {
        originalColor = color;
    }

    public Color GetOriginalColor()
    {
        return originalColor;
    }

    public void DestroyHeldItem()
    {
        Destroy(gameObject);
    }

}
