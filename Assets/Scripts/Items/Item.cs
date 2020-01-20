using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        public double m_Durability;

        public double m_Damage;

        public bool isPlaceable;

        public TileBase m_placedTile;
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
            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(item);

            
        }
    }
}
