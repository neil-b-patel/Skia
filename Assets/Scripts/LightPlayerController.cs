using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumables>().item;

            if (hitObject != null)
            {
                switch (hitObject.itemType)
                {
                    case Item.ItemType.LFoot:
                        this.LFoot = true;
                        break;
                    case Item.ItemType.RFoot:
                        this.RFoot = true;
                        break;
                    default:
                        break;
                }

                    collision.gameObject.SetActive(false);
            }

        }
    }

}
