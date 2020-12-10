using UnityEditor;
using UnityEngine;


public static class PrefabCreator
{
    [MenuItem("CCK_Generator/Create/New Prefab")]
    public static void CreatePrefab() {
        string name = "target";
        string outputPath = "Assets/Prefab.prefab";

        GameObject gameObject = EditorUtility.CreateGameObjectWithHideFlags(name, HideFlags.HideInHierarchy);

        bool success;
        PrefabUtility.SaveAsPrefabAsset(gameObject, outputPath, out success);

        Editor.DestroyImmediate(gameObject);
    }
}
