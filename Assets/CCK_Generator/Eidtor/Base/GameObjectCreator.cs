using System;
using UnityEditor;
using UnityEngine;


namespace Ponta.CCK_Generator.Base
{
    public class GameObjectCreator
    {
        HandGunDefinition definition;
        GameObject gameObject;


        public bool Init(HandGunDefinition definition) {

            this.definition = definition;

            /* GameObject */
            if (definition.GetPrototypePath() != null) {

                /* Load prototype prefab */
                gameObject = LoadPrototypePrefab();
                if (gameObject == null) { return false; }
            }
            else {
                /* Create new gameObject */
                const string name = "target";
                gameObject = EditorUtility.CreateGameObjectWithHideFlags(name, HideFlags.HideInHierarchy);
            }

            return true;
        }

        public void SaveAsPrefabAsset() {

            /* Save prefab */
            bool success;
            PrefabUtility.SaveAsPrefabAsset(gameObject, definition.GetOutputPath(), out success);

            if (!success) {
                Debug.LogError("SaveAsPrefabAsset failed ! : " + definition.GetOutputPath());
            }

            /* Clean up gameObject */
            if (definition.GetPrototypePath() != null) {
                PrefabUtility.UnloadPrefabContents(gameObject);
            }
            else {
                Editor.DestroyImmediate(gameObject);
            }
        }

        public void AddItem(ItemInfo itemInfo) {
            itemInfo.AddItem(gameObject);
        }

        public void AddTrigger(TriggerInfo triggerInfo) {
            triggerInfo.AddTrigger(gameObject);
        }

        public void AddLogic(LogicInfo logicInfo) {
            logicInfo.AddLogic(gameObject);
        }

        GameObject LoadPrototypePrefab() {

            /* Check file name */
            string fileName = System.IO.Path.GetFileName(definition.GetPrototypePath());

            if (!fileName.StartsWith("Prototype_")) {
                Debug.LogError("Prototype fileName error ! Required [Prototype_XXX] : " + fileName);
                return null;
            }

            /* Load */
            try {
                gameObject = PrefabUtility.LoadPrefabContents(definition.GetPrototypePath());
                return gameObject;
            }
            catch (Exception e) {
                Debug.LogError("LoadPrefabContents failed ! : " + definition.GetPrototypePath() + " : " + e.Message);
                return null;
            }
        }

    }
}
