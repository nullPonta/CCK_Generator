using ClusterVR.CreatorKit;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Operation;
using ClusterVR.CreatorKit.Trigger;
using Ponta.CCK_Generator.Base;
using System.Collections.Generic;
using UnityEditor;

using LPW = Ponta.CCK_Generator.Base.LogicParamWrapper;


namespace Ponta.CCK_Generator
{

    public static class HandGunCreator
    {
        [MenuItem("CCK_Generator/Create/HandGun")]
        public static void CreateHandGunPrefab() {

            IDefinition definition = new HandGunDefinition();
            definition.CreatePrefabFromPrototype();
        }
    }

    public class HandGunDefinition : IDefinition
    {
        CommonInfo common = new CommonInfo();


        public HandGunDefinition() {

            /* Set path */
            common.prefabsPathController.SetPath("Prototype_HandGun.prefab", "HandGun.prefab");

            /* ---------------------------------------------------------------- */
            // Define : Item
            /* ---------------------------------------------------------------- */
            {
                var itemInfo = common.itemInfo;

                itemInfo.isItem = true;
                itemInfo.itemName = "ハンドガン";

                itemInfo.isMovableItem = true;

                itemInfo.isGrabbableItem = true;
            }

            /* ---------------------------------------------------------------- */
            // Define : Trigger
            /* ---------------------------------------------------------------- */
            {
                var triggerInfo = common.triggerInfo;

                /* OnCreateItemTrigger */
                var bullets = triggerInfo.CreateTriggerParamInteger(TriggerTarget.Item, null, "bullets", 6);
                var maxBullets = triggerInfo.CreateTriggerParamInteger(TriggerTarget.Item, null, "maxBullets", 6);

                triggerInfo.AddOnCreateItemTrigger(bullets);
                triggerInfo.AddOnCreateItemTrigger(maxBullets);

                /* OnGrabItemTrigger */
                var enableUI_On = triggerInfo.CreateTriggerParamBool(TriggerTarget.Item, null, "enableUI", true);
                var reloaded = triggerInfo.CreateTriggerParamSignal(TriggerTarget.Item, null, "Reloaded");

                triggerInfo.AddOnGrabItemTrigger(enableUI_On);
                triggerInfo.AddOnGrabItemTrigger(reloaded);

                /* OnReleaseItemTrigger */
                var enableUI_Off = triggerInfo.CreateTriggerParamBool(TriggerTarget.Item, null, "enableUI", false);

                triggerInfo.AddOnReleaseItemTrigger(enableUI_Off);

                /* UseItemTrigger */
                var shootUnlessReloading = triggerInfo.CreateTriggerParamSignal(TriggerTarget.Item, null, "ShootUnlessReloading");
                triggerInfo.AddUseItemTrigger_Down(shootUnlessReloading);
            }

            /* ---------------------------------------------------------------- */
            // Define : Logic
            /* ---------------------------------------------------------------- */
            var logicInfo = common.logicInfo;

            /* ItemLogic */
            {
                /* On receive */
                var onReceive = LogicParamGenerator.CreateOnReceiveKey(GimmickTarget.Item, "ShootUnlessReloading");

                /* Logic */
                // if (!reloading) { SendSignal(Item, "ShootOrReload") }
                var logic = LogicParamGenerator.CreateLogic(
                    LPW.SendSignalToSelf(Operator.Not, "reloading", "ShootOrReload"));

                /* LogicParam */
                logicInfo.AddItemLogicParam(new LogicParam(onReceive, logic));
            }

            {
                /* On receive */
                var onReceive = LogicParamGenerator.CreateOnReceiveKey(GimmickTarget.Item, "ShootOrReload");

                /* Logic */
                // if (bullets > 0) { SendSignal(Item, "Shoot") }
                // if (bullets <= 0) { SendSignal(Item, "Reload") }
                var logic = LogicParamGenerator.CreateLogic(
                    LPW.SendSignalToSelfByCompare(Operator.GreaterThan, "bullets", new Base.ConstantValue(0), "Shoot"),
                    LPW.SendSignalToSelfByCompare(Operator.LessThanOrEqual, "bullets", new Base.ConstantValue(0), "Reload"));

                /* LogicParam */
                logicInfo.AddItemLogicParam(new LogicParam(onReceive, logic));
            }

            {
                /* On receive */
                var onReceive = LogicParamGenerator.CreateOnReceiveKey(GimmickTarget.Item, "Shoot");

                /* Logic */
                // bullets = bullets - 1
                var logic = LogicParamGenerator.CreateLogic(
                    LPW.Calculate(Operator.Subtract, "bullets", new Base.ConstantValue(1)));

                /* LogicParam */
                logicInfo.AddItemLogicParam(new LogicParam(onReceive, logic));
            }

            {
                /* On receive */
                var onReceive = LogicParamGenerator.CreateOnReceiveKey(GimmickTarget.Item, "Reload");

                /* Logic */
                // reloading = true
                // enableUI = false
                var logic = LogicParamGenerator.CreateLogic(
                    LPW.SetValue("reloading", new Base.ConstantValue(true)),
                    LPW.SetValue("enableUI", new Base.ConstantValue(false)));

                /* LogicParam */
                logicInfo.AddItemLogicParam(new LogicParam(onReceive, logic));
            }

            {
                // Reload -> Item Timer -> Reloaded
                /* On receive */
                var onReceive = LogicParamGenerator.CreateOnReceiveKey(GimmickTarget.Item, "Reloaded");

                /* Logic */
                // bullets = maxBullets
                // reloading = false
                // enableUI = true
                var logic = LogicParamGenerator.CreateLogic(
                    LPW.SetValueFromKey("bullets", ParameterType.Integer, "maxBullets"),
                    LPW.SetValue("reloading", new Base.ConstantValue(false)),
                    LPW.SetValue("enableUI", new Base.ConstantValue(true)));

                /* LogicParam */
                logicInfo.AddItemLogicParam(new LogicParam(onReceive, logic));

            }

        }

        public void CreatePrefabFromPrototype() {
            common.CreatePrefabFromPrototype();
        }

    }

}

