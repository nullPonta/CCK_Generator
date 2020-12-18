#if UNITY_EDITOR
using ClusterVR.CreatorKit;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Operation;
using ClusterVR.CreatorKit.Trigger;
using Ponta.CCK_Generator.Base;
using UnityEditor;

using LPG = Ponta.CCK_Generator.Base.TriggerParamGenerator;
using LPW = Ponta.CCK_Generator.Base.LogicParamWrapper;


namespace Ponta.CCK_Generator
{

    public class ChargeGunCreator
    {

        [MenuItem("CCK_Generator/Create/ChargeGun")]
        public static void CreatePrefab() {

            IDefinition definition = new ChargeGunDefinition();
            definition.CreatePrefabFromPrototype();
        }

    }

    public class ChargeGunDefinition : IDefinition
    {
        CommonInfo common = new CommonInfo();


        public ChargeGunDefinition() {

            /* Set path */
            common.prefabsPathController.SetPath("Prototype_ChargeGun.prefab", "ChargeGun.prefab");

            /* ---------------------------------------------------------------- */
            // Define : Item
            /* ---------------------------------------------------------------- */
            {
                var itemInfo = common.itemInfo;

                itemInfo.isItem = true;
                itemInfo.itemName = "チャージガン";

                itemInfo.isMovableItem = true;

                itemInfo.isGrabbableItem = true;
            }

            /* ---------------------------------------------------------------- */
            // Define : Trigger
            /* ---------------------------------------------------------------- */
            {
                var triggerInfo = common.triggerInfo;

                /* OnCreateItemTrigger */

                /* OnGrabItemTrigger */
                triggerInfo.AddOnGrabItemTrigger(LPG.CreateSignal(TriggerTarget.Item, null, "OnGrab"));

                /* OnReleaseItemTrigger */
                triggerInfo.AddOnReleaseItemTrigger(LPG.CreateBool(TriggerTarget.Item, null, "using", false));

                /* UseItemTrigger */
                triggerInfo.AddUseItemTrigger_Down(LPG.CreateSignal(TriggerTarget.Item, null, "StartCharging"));
                triggerInfo.AddUseItemTrigger_Down(LPG.CreateBool(TriggerTarget.Item, null, "using", true));
                triggerInfo.AddUseItemTrigger_Down(LPG.CreateBool(TriggerTarget.Item, null, "fullyCharged", false));
                triggerInfo.AddUseItemTrigger_Down(LPG.CreateBool(TriggerTarget.Item, null, "charging", true));

                triggerInfo.AddUseItemTrigger_Up(LPG.CreateSignal(TriggerTarget.Item, null, "Shoot"));
                triggerInfo.AddUseItemTrigger_Up(LPG.CreateBool(TriggerTarget.Item, null, "using", false));

            }

            /* ---------------------------------------------------------------- */
            // Define : Logic
            /* ---------------------------------------------------------------- */
            var logicInfo = common.logicInfo;

            /* ItemLogic */
            {
                // StartCharging -> Item Timer -> CompleteChargingIfUsing
                /* On receive */
                var onReceive = LogicParamGenerator.CreateOnReceiveKey(GimmickTarget.Item, "CompleteChargingIfUsing");

                /* Logic */
                // if (using) { SendSignal(Item, "CompleteCharging") }
                // fullyCharged = using
                // charging = false
                var logic = LogicParamGenerator.CreateLogic(
                    LPW.SendSignalToSelf("using", "CompleteCharging"),
                    LPW.SetValueFromKey("fullyCharged", ParameterType.Bool, "using"),
                    LPW.SetValue("charging", new Base.ConstantValue(false)));

                /* LogicParam */
                logicInfo.AddItemLogicParam(new LogicParam(onReceive, logic));
            }

            /* ItemLogic */
            {
                /* On receive */
                var onReceive = LogicParamGenerator.CreateOnReceiveKey(GimmickTarget.Item, "Shoot");

                /* Logic */
                // if (fullyCharged) { SendSignal(Item, "ShootStrongBullet") }
                // if (!fullyCharged) { SendSignal(Item, "ShootWeakBullet") }
                var logic = LogicParamGenerator.CreateLogic(
                    LPW.SendSignalToSelf("fullyCharged", "ShootStrongBullet"),
                    LPW.SendSignalToSelf(Operator.Not, "fullyCharged", "ShootWeakBullet"));

                /* LogicParam */
                logicInfo.AddItemLogicParam(new LogicParam(onReceive, logic));
            }

        }

        public void CreatePrefabFromPrototype() {
            common.CreatePrefabFromPrototype();
        }

    }

}
#endif
