using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OGPC
{
    public class ItemEntity : Entity
    {
        public Item item;

        private bool collected = false;

        void Start()
	    {
            GetComponent<SpriteRenderer>().sprite = item.m_Sprites[0];
	    }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player" && !collected)
            {
                other.gameObject.GetComponent<Inventory>().AddItem(item, 1); // Just accept this line.
                Destroy(gameObject);

                collected = true;
            }
        }
    }
}
