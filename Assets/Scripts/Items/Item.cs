using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace OGPC
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Item", menuName = "OGPC/Item")]
    public class Item : ScriptableObject
    {
        public Sprite[] m_Sprites;

        public Sprite dropSprite;

        public double m_Durability;

        public double m_Damage;

        public bool isPlaceable;

        public TileBase m_placedTile;

        public int maxStackSize = 64;

        public bool matches(string itemName) // This code defines whether an item matches another, can be expanded to a more complete test if needed
        {
            return ToString() == itemName;
        }

        public bool matches(Item testItem) // This code defines whether an item matches another, can be expanded to a more complete test if needed
        {
            return ToString() == testItem.ToString();
        }
    }

    [CustomEditor(typeof(Item))]
    public class ItemEditor : Editor
    {
        private Item item { get { return (target as Item); } }

        public void OnEnable()
        {
            if (item.m_Sprites == null || item.m_Sprites.Length != 15)
            {
                item.m_Sprites = new Sprite[15];
                EditorUtility.SetDirty(item);
            }
        }

        public override void OnInspectorGUI()
        {
            float oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 210;

            EditorGUI.BeginChangeCheck();
            item.m_Sprites[0] = (Sprite)EditorGUILayout.ObjectField("Default Sprite", item.m_Sprites[0], typeof(Sprite), false, null);

            item.m_Damage = (double)EditorGUILayout.DoubleField("Damage", item.m_Damage, (GUILayoutOption[])null);

            item.isPlaceable = (bool)EditorGUILayout.Toggle("Is Placeable", item.isPlaceable, (GUILayoutOption[])null);

            EditorGUIUtility.labelWidth = oldLabelWidth;
            item.m_placedTile = (TileBase)EditorGUILayout.ObjectField("Placed Tile", item.m_placedTile, typeof(TileBase), false, null);

            item.maxStackSize = (int)EditorGUILayout.IntField("Max Stack Size", item.maxStackSize, (GUILayoutOption[])null);
            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(item);


        }
    }

    [Serializable]
    public class ItemContainer
    {
        public string name = "";
        public Item item;
        public int stackedAmount = 0;

        public ItemContainer(string _name, int _stackedAmount)
        {
            name = _name;
            stackedAmount = _stackedAmount;
        }

        public ItemContainer(Item _item, int _stackedAmount)
        {
            item = _item;
            name = _item.ToString();
            stackedAmount = _stackedAmount;
        }

        public bool matches(string itemName) // This code defines whether an item matches another, can be expanded to a more complete test if needed
        {
            return itemName == name;
        }

        public bool matches(Item testItem) // This code defines whether an item matches another, can be expanded to a more complete test if needed
        {
            return item.matches(testItem);
        }

        public bool isEmpty()
        {
            return name == null || stackedAmount == 0;
        }

        public void clearSlot()
        {
            name = null;
            stackedAmount = 0;
        }
    }
}
