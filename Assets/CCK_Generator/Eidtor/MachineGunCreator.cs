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
                triggerInfo.AddOnCreateItemTrigger(LPG.CreateFloat(TriggerTarget.Item, null, "heat", 1.0f));
                triggerInfo.AddOnCreateItemTrigger(LPG.CreateFloat(TriggerTarget.Item, null, "overheatThreshold", 10));

                /* OnGrabItemTrigger */
                triggerInfo.AddOnGrabItemTrigger(LPG.CreateBool(TriggerTarget.Item, null, "enableUI", true));
                triggerInfo.AddOnGrabItemTrigger(LPG.CreateSignal(TriggerTarget.Item, null, "OnGrab"));

                /* OnReleaseItemTrigger */
                triggerInfo.AddOnReleaseItemTrigger(LPG.CreateBool(TriggerTarget.Item, null, "enableUI", false));
                triggerInfo.AddOnReleaseItemTrigger(LPG.CreateBool(TriggerTarget.Item, null, "using", false));

                /* UseItemTrigger Down */
                triggerInfo.AddUseItemTrigger_Down(LPG.CreateSignal(TriggerTarget.Item, null, "ShootUnlessOverheating"));
                triggerInfo.AddUseItemTrigger_Down(LPG.CreateBool(TriggerTarget.Item, null, "using", true));

                /* UseItemTrigger Up */
                triggerInfo.AddUseItemTrigger_Up(LPG.CreateBool(TriggerTarget.Item, null, "using", false));
                triggerInfo.AddUseItemTrigger_Up(LPG.CreateBool(TriggerTarget.Item, null, "shooting", false));
            }

            /* ---------------------------------------------------------------- */
            // Define : Logic
            /* ---------------------------------------------------------------- */
            var logicInfo = common.logicInfo;

            /* ItemLogic */
            {
                /* On receive */
                var onReceive = LogicParamGenerator.CreateOnReceiveKey(GimmickTarget.Item, "ShootUnlessOverheating");

                /* Logic */
                // if (!overheating) { SendSignal(Item, "Shoot") }
                // if (!overheating) { SendSignal(Item, "shooting") }
                // if (overheating) { SendSignal(Item, "Overheat") }
                // if (true) { SendSignal(Item, "DelayCoolDown") }
                var logic = LogicParamGenerator.CreateLogic(
                    LPW.SendSignalToSelf(Operator.Not, "overheating", "Shoot"),
                    LPW.SendSignalToSelf(Operator.Not, "overheating", "shooting"),
                    LPW.SendSignalToSelf("overheating", "Overheat"),
                    LPW.SendSignalToSelf("DelayCoolDown"));

                /* LogicParam */
                logicInfo.AddItemLogicParam(new LogicParam(onReceive, logic));
            }

            {
                /* On receive */
                var onReceive = LogicParamGenerator.CreateOnReceiveKey(GimmickTarget.Item, "Shoot");

                /* Logic */
                // if (true) { SendSignal(Item, "CreateBullet") }
                // heat = heat * 1.1
                // if (heat >= overheatThreshold) { SendSignal(Item, "Overheat") }
                var logic = LogicParamGenerator.CreateLogic(
                    LPW.SendSignalToSelf("CreateBullet"),
                    LPW.SetValueByCalculate("heat", Operator.Multiply, new Base.ConstantValue(1.1f)),
                    LPW.SendSignalToSelfByCompare("heat", Operator.GreaterThanOrEqual, "overheatThreshold", "Overheat"));

                /* LogicParam */
                logicInfo.AddItemLogicParam(new LogicParam(onReceive, logic));
            }

            {
                // ShootUnlessOverheating -> Item Timer -> ShootIfUsing
                /* On receive */
                var onReceive = LogicParamGenerator.CreateOnReceiveKey(GimmickTarget.Item, "ShootIfUsing");

                /* Logic */
                // if (using) { SendSignal(Item, "Overheat") }
                var logic = LogicParamGenerator.CreateLogic(
                    LPW.SendSignalToSelf("using", "ShootUnlessOverheating"));

                /* LogicParam */
                logicInfo.AddItemLogicParam(new LogicParam(onReceive, logic));
            }

            {
                // DelayCoolDown -> Item Timer -> StartCoolDown, PlayCoolDownSound
                // StartCoolDown -> Item Timer -> CoolDownUnlessShooting
                /* On receive */
                var onReceive = LogicParamGenerator.CreateOnReceiveKey(GimmickTarget.Item, "CoolDownUnlessShooting");

                /* Logic */
                // if (!shooting) { SendSignal(Item, "CoolDown") }
                var logic = LogicParamGenerator.CreateLogic(
                    LPW.SendSignalToSelf(Operator.Not, "shooting", "CoolDown"));

                /* LogicParam */
                logicInfo.AddItemLogicParam(new LogicParam(onReceive, logic));
            }

            {
                /* On receive */
                var onReceive = LogicParamGenerator.CreateOnReceiveKey(GimmickTarget.Item, "CoolDown");

                /* Logic */
                // heat = heat - 1.0
                // cooled = (heat <= 1)
                // heat = Max(heat, 1.0f)
                // if (!cooled) { SendSignal(Item, "StartCoolDown") }
                // overheating = cooled ? false : overheating
                var logic = LogicParamGenerator.CreateLogic(
                    LPW.SetValueByCalculate("heat", Operator.Subtract, new Base.ConstantValue(1.0f)),
                    LPW.SetValueByCompare("cooled", ParameterType.Bool, "heat", Operator.LessThanOrEqual, new Base.ConstantValue(1.0f)),
                    LPW.SetValueByCalculate("heat", ParameterType.Float, Operator.Max, "heat", new Base.ConstantValue(1.0f)),
                    LPW.SendSignalToSelf(Operator.Not, "cooled", "StartCoolDown"),
                    LPW.SetValueByCondition("overheating", ParameterType.Bool, "cooled", new Base.ConstantValue(false), "overheating"));

                /* LogicParam */
                logicInfo.AddItemLogicParam(new LogicParam(onReceive, logic));
            }

            {
                /* On receive */
                var onReceive = LogicParamGenerator.CreateOnReceiveKey(GimmickTarget.Item, "Overheat");

                /* Logic */
                // overheating = true
                // shooting = false
                // cooled = false
                var logic = LogicParamGenerator.CreateLogic(
                    LPW.SetValue("overheating", new Base.ConstantValue(true)),
                    LPW.SetValue("shooting", new Base.ConstantValue(false)),
                    LPW.SetValue("cooled", new Base.ConstantValue(false)));

                /* LogicParam */
                logicInfo.AddItemLogicParam(new LogicParam(onReceive, logic));
            }

        }

        public void CreatePrefabFromPrototype() {
            common.CreatePrefabFromPrototype();
        }

    }

}

