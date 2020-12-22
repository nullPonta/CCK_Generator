#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


namespace Ponta.CCK_Generator.Base
{
    public class GameObjectCreator
    {
        PrefabsPathController prefabsPathController;
        GameObject gameObject;

        public UnityAction<GameObject> OnCreate;


        public bool Init(PrefabsPathController definition) {

            this.prefabsPathController = definition;

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

            if (OnCreate != null) {
                OnCreate(gameObject);
            }

            /* Save prefab */
            bool success;
            PrefabUtility.SaveAsPrefabAsset(gameObject, prefabsPathController.GetOutputPath(), out success);

            if (!success) {
                Debug.LogError("SaveAsPrefabAsset failed ! : " + prefabsPathController.GetOutputPath());
            }

            /* Clean up gameObject */
            if (prefabsPathController.GetPrototypePath() != null) {
                PrefabUtility.UnloadPrefabContents(gameObject);
            }
            else {
                Editor.DestroyImmediate(gameObject);
            }
        }

        public void AddItem(ItemInfo itemInfo) {
            itemInfo.AddItemComponent(gameObject);
        }

        public void AddTrigger(TriggerInfo triggerInfo) {
            triggerInfo.AddTriggerComponent(gameObject);
        }

        public void AddLogic(LogicInfo logicInfo) {
            logicInfo.AddLogicComponent(gameObject);
        }

        GameObject LoadPrototypePrefab() {

            /* Check file name */
            string fileName = System.IO.Path.GetFileName(prefabsPathController.GetPrototypePath());

            if (!fileName.StartsWith("Prototype_")) {
                Debug.LogError("Prototype fileName error ! Required [Prototype_XXX] : " + fileName);
                return null;
            }

            /* Load prefab */
            try {
                gameObject = PrefabUtility.LoadPrefabContents(prefabsPathController.GetPrototypePath());
                return gameObject;
            }
            catch (Exception e) {
                Debug.LogError("LoadPrefabContents failed ! : " + prefabsPathController.GetPrototypePath() + " : " + e.Message);
                return null;
            }
        }

    }
}
#endif
