using ClusterVR.CreatorKit.Item.Implements;
using UnityEditor;
using UnityEngine;


namespace Ponta.CCK_Generator
{
    public class GameObjectCreator
    {
        GameObject gameObject;
        HandGunDefinition definition;

        public void Init(HandGunDefinition definition) {

            const string name = "target";
            gameObject = EditorUtility.CreateGameObjectWithHideFlags(name, HideFlags.HideInHierarchy);

            this.definition = definition;
        }

        public void SaveAsPrefabAsset() {
            bool success;
            PrefabUtility.SaveAsPrefabAsset(gameObject, definition.outputPath, out success);
            Editor.DestroyImmediate(gameObject);
        }

        public void AddItem(ItemInfo itemInfo) {

            if (itemInfo.isItem) {
                var item = gameObject.AddComponent<Item>();

                SerializedObjectUtil.SetValue(item, "itemName", itemInfo.itemName);
            }

            if (itemInfo.isMovableItem) {
                var grabbableItem = gameObject.AddComponent<GrabbableItem>();
            }

            if (itemInfo.isGrabbableItem) {
                var grabbableItem = gameObject.AddComponent<GrabbableItem>();
            }

        }

    }
}
