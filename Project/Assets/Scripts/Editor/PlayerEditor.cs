using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor {

    Player script;

    public void OnEnable()
    {
        script = target as Player;
    }

    public override void OnInspectorGUI()
    {
        if(Application.isPlaying)
            EditorGUILayout.LabelField(string.Format("Coins in inventory : {0}", script.coinsInventory.Count));        
        DrawDefaultInspector();
    }
}
