using ClusterVR.CreatorKit.Item.Implements;
using UnityEngine;


namespace Ponta.CCK_Generator.Base
{

    public class ItemInfo
    {
        public bool isItem;
        public string itemName;

        public bool isMovableItem;

        public bool isGrabbableItem;


        public void AddItem(GameObject gameObject) {

            /* Item */
            if (isItem) {
                var item = gameObject.AddComponent<Item>();

                SerializedObjectUtil.SetStringValue(item, "itemName", itemName);
            }

            /* MovableItem */
            if (isMovableItem) {
                var movableItem = gameObject.AddComponent<MovableItem>();
            }

            /* GrabbableItem */
            if (isGrabbableItem) {
                var grabbableItem = gameObject.AddComponent<GrabbableItem>();

                var grip = gameObject.transform.Find("Grip");
                if (grip != null) {
                    SerializedObjectUtil.SetTransformValue(grabbableItem, "grip", grip);
                }

            }

        }
    }

}
