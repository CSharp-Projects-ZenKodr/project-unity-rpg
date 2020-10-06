﻿using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUpItem : MonoBehaviour
{
    public Item itemData;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (GameManager.instance.items.Count < GameManager.instance.slots.Length)
             {           
                Destroy(gameObject);
                GameManager.instance.AddItem(itemData);
            }
            else
            {
                Debug.Log("Inventory is full!. Can't pick up more items!");
            }
        }
        
    }

}
