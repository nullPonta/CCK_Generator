using ClusterVR.CreatorKit;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Operation;
using ClusterVR.CreatorKit.Trigger;
using Ponta.CCK_Generator.Base;
using UnityEditor;

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

