using ClusterVR.CreatorKit;
using ClusterVR.CreatorKit.Item.Implements;
using ClusterVR.CreatorKit.Trigger.Implements;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using TriggerTarget = ClusterVR.CreatorKit.Trigger.TriggerTarget;


namespace Ponta.CCK_Generator.Base
{
    public class GameObjectCreator
    {
        HandGunDefinition definition;
        GameObject gameObject;


        public bool Init(HandGunDefinition definition) {

            this.definition = definition;

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
            bool success;
            PrefabUtility.SaveAsPrefabAsset(gameObject, definition.GetOutputPath(), out success);

            if (!success) {
                Debug.LogError("SaveAsPrefabAsset failed ! : " + definition.GetOutputPath());
            }

            if (definition.GetPrototypePath() != null) {
                PrefabUtility.UnloadPrefabContents(gameObject);
            }
            else {
                Editor.DestroyImmediate(gameObject);
            }
        }

        public void AddItem(ItemInfo itemInfo) {

            if (itemInfo.isItem) {
                var item = gameObject.AddComponent<Item>();

                SerializedObjectUtil.SetStringValue(item, "itemName", itemInfo.itemName);
            }

            if (itemInfo.isMovableItem) {
                var movableItem = gameObject.AddComponent<MovableItem>();
            }

            if (itemInfo.isGrabbableItem) {
                var grabbableItem = gameObject.AddComponent<GrabbableItem>();

                var grip = gameObject.transform.Find("Grip");
                if (grip != null) {
                    SerializedObjectUtil.SetValue(grabbableItem, "grip", grip);
                }

            }

        }

        public void AddTrigger() {

            var onCreateItemTrigger = gameObject.AddComponent<OnCreateItemTrigger>();

            var triggers = new List<TriggerParam>();

            //var bulletCount = new Value();
            //ReflectionUtil.PrintAll(bulletCount);
            //SerializedObjectUtil.SetValue(bulletCount, "integerValue", 6);

            var bullets = new TriggerParam(TriggerTarget.Item, null, "bullets", ParameterType.Integer, new Value());
            triggers.Add(bullets);

            ReflectionUtil.PrintAll(onCreateItemTrigger);
            SerializedObjectUtil.SetValue(onCreateItemTrigger, "triggers", triggers.ToArray());
        }

        GameObject LoadPrototypePrefab() {

            string fileName = System.IO.Path.GetFileName(definition.GetPrototypePath());

            if (!fileName.StartsWith("Prototype_")) {
                Debug.LogError("Prototype fileName error ! Required [Prototype_XXX] : " + fileName);
                return null;
            }

            try {
                gameObject = PrefabUtility.LoadPrefabContents(definition.GetPrototypePath());
                return gameObject;
            }
            catch (Exception e) {
                Debug.LogError("LoadPrefabContents failed ! : " + definition.GetPrototypePath());
                return null;
            }
        }

    }
}
