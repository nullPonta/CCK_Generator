﻿using ClusterVR.CreatorKit.Trigger;
using Ponta.CCK_Generator.Base;
using UnityEditor;


namespace Ponta.CCK_Generator
{

    public class MachineGunCreator
    {
        [MenuItem("CCK_Generator/Create/MachineGunCreator")]
        public static void CreateHandGunPrefab() {

            IDefinition definition = new MachineGunDefinition();
            definition.CreatePrefabFromPrototype();
        }
    }

    public class MachineGunDefinition : IDefinition
    {
        CommonInfo common = new CommonInfo();

        public MachineGunDefinition() {

            /* Set path */
            common.prefabsPathController.SetPath("Prototype_MachineGun.prefab", "MachineGun.prefab");

            /* ---------------------------------------------------------------- */
            // Define : Item
            /* ---------------------------------------------------------------- */
            {
                var itemInfo = common.itemInfo;

                itemInfo.isItem = true;
                itemInfo.itemName = "マシンガン";

                itemInfo.isMovableItem = true;

                itemInfo.isGrabbableItem = true;
            }

            /* ---------------------------------------------------------------- */
            // Define : Trigger
            /* ---------------------------------------------------------------- */
            {
                var triggerInfo = common.triggerInfo;

                /* OnCreateItemTrigger */
                var heat = triggerInfo.CreateTriggerParamFloat(TriggerTarget.Item, null, "heat", 1.0f);
                var overheatThreshold = triggerInfo.CreateTriggerParamFloat(TriggerTarget.Item, null, "overheatThreshold", 10);

                triggerInfo.AddOnCreateItemTrigger(heat);
                triggerInfo.AddOnCreateItemTrigger(overheatThreshold);

                /* OnGrabItemTrigger */
                var enableUI_On = triggerInfo.CreateTriggerParamBool(TriggerTarget.Item, null, "enableUI", true);
                var onGrab = triggerInfo.CreateTriggerParamSignal(TriggerTarget.Item, null, "OnGrab");

                triggerInfo.AddOnGrabItemTrigger(enableUI_On);
                triggerInfo.AddOnGrabItemTrigger(onGrab);

                /* OnReleaseItemTrigger */
                var enableUI_Off = triggerInfo.CreateTriggerParamBool(TriggerTarget.Item, null, "enableUI", false);
                var usingfalse = triggerInfo.CreateTriggerParamBool(TriggerTarget.Item, null, "using", false);

                triggerInfo.AddOnReleaseItemTrigger(enableUI_Off);
                triggerInfo.AddOnReleaseItemTrigger(usingfalse);

                /* UseItemTrigger Down */
                var shootUnlessOverheating = triggerInfo.CreateTriggerParamSignal(TriggerTarget.Item, null, "ShootUnlessOverheating");
                var usingTrue = triggerInfo.CreateTriggerParamBool(TriggerTarget.Item, null, "using", true);
                triggerInfo.AddUseItemTrigger_Down(shootUnlessOverheating);
                triggerInfo.AddUseItemTrigger_Down(usingTrue);

                /* UseItemTrigger Up */
                var usingfalseOnUseUp = triggerInfo.CreateTriggerParamBool(TriggerTarget.Item, null, "using", false);
                var shooting = triggerInfo.CreateTriggerParamBool(TriggerTarget.Item, null, "shooting", false);
                triggerInfo.AddUseItemTrigger_Up(usingfalseOnUseUp);
                triggerInfo.AddUseItemTrigger_Up(shooting);
            }

            /* ---------------------------------------------------------------- */
            // Define : Logic
            /* ---------------------------------------------------------------- */
            var logicInfo = common.logicInfo;

            /* ItemLogic */



        }

        public void CreatePrefabFromPrototype() {
            common.CreatePrefabFromPrototype();
        }

    }

}

