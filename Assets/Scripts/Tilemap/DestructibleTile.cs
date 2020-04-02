using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

namespace OGPC
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Destructible Tile", menuName = "OGPC/Tile/Destructible Tile")]
    public class DestructibleTile : RuleTile
    {
        public Item dropItem;

        public double hardness;

        public void SpawnDrop(Vector3 pos)
        {
            GameObject drop = new GameObject("Dropped Item", typeof(ItemEntity), typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(BoxCollider2D));
            drop.transform.position = pos;

            drop.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);

            drop.GetComponent<ItemEntity>().item = dropItem;

            BoxCollider2D trigger = drop.AddComponent<BoxCollider2D>() as BoxCollider2D;
            trigger.size = new Vector2(1.1f, 1.1f);
            trigger.isTrigger = true;
        }

        public override bool RuleMatch(int neighbor, TileBase tile)
        {
            switch (neighbor)
            {
                case TilingRule.Neighbor.This: return tile != null;
                case TilingRule.Neighbor.NotThis: return tile == null;
            }
            return true;
        }
    }
}
