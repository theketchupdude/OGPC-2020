using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace OGPC
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Destructible Tile", menuName = "OGPC/Tile/Destructible Tile")]
    public class DestructibleTile : RuleTile
    {
        public Item dropItem;

        public Item getDestroyItem()
        {
            return dropItem;
        }
    }
}
