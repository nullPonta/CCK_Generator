﻿#if UNITY_EDITOR
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


        public void AddItemComponent(GameObject gameObject) {

            /* Item */
            if (isItem) {
                var item = gameObject.GetComponent<Item>();

                if(item == null) {
                    item = gameObject.AddComponent<Item>();
                }

                SerializedObjectUtil.SetStringValue(item, "itemName", itemName);
            }

            /* MovableItem */
            if (isMovableItem) {
                var movableItem = gameObject.GetComponent<MovableItem>();

                if (movableItem == null) {
                    movableItem = gameObject.AddComponent<MovableItem>();
                }
            }

            /* GrabbableItem */
            if (isGrabbableItem) {
                var grabbableItem = gameObject.GetComponent<GrabbableItem>();

                if (grabbableItem == null) {
                    grabbableItem = gameObject.AddComponent<GrabbableItem>();
                }

                var grip = gameObject.transform.Find("Grip");
                if (grip != null) {
                    SerializedObjectUtil.SetTransformValue(grabbableItem, "grip", grip);
                }

            }

        }
    }

}
#endif
