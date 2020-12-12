using ClusterVR.CreatorKit;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Operation;
using ClusterVR.CreatorKit.Trigger;
using Ponta.CCK_Generator.Base;
using UnityEditor;


namespace Ponta.CCK_Generator
{

    public static class HandGunCreator
    {
        [MenuItem("CCK_Generator/Create/HandGun")]
        public static void CreateHandGunPrefab() {

            GameObjectCreator gameObjectCreator = new GameObjectCreator();
            HandGunDefinition handGunDefinition = new HandGunDefinition();

            var result = gameObjectCreator.Init(handGunDefinition.prefabsPathController);
            if (!result) { return; }

            handGunDefinition.AddComponent(gameObjectCreator);
            gameObjectCreator.SaveAsPrefabAsset();
        }

    }

    public class HandGunDefinition
    {
        public PrefabsPathController prefabsPathController = new PrefabsPathController();

        ItemInfo itemInfo = new ItemInfo();

        TriggerInfo triggerInfo = new TriggerInfo();

        LogicInfo logicInfo = new LogicInfo();


        public HandGunDefinition() {

            /* Set path */
            prefabsPathController.OutputPath = "HandGun.prefab";
            prefabsPathController.PrototypePath = "Prototype_HandGun.prefab";

            /* Define : Item  */
            itemInfo.isItem = true;
            itemInfo.itemName = "ハンドガン";

            itemInfo.isMovableItem = true;

            itemInfo.isGrabbableItem = true;

            /* Define : OnCreateItemTrigger */
            var bullets = triggerInfo.CreateTriggerParamInteger(TriggerTarget.Item, null, "bullets", 6);
            var maxBullets = triggerInfo.CreateTriggerParamInteger(TriggerTarget.Item, null, "maxBullets", 6);

            triggerInfo.AddOnCreateItemTrigger(bullets);
            triggerInfo.AddOnCreateItemTrigger(maxBullets);

            /* Define : OnGrabItemTrigger */
            var enableUI_On = triggerInfo.CreateTriggerParamBool(TriggerTarget.Item, null, "enableUI", true);
            var reloaded = triggerInfo.CreateTriggerParamSignal(TriggerTarget.Item, null, "Reloaded");

            triggerInfo.AddOnGrabItemTrigger(enableUI_On);
            triggerInfo.AddOnGrabItemTrigger(reloaded);

            /* Define : OnReleaseItemTrigger */
            var enableUI_Off = triggerInfo.CreateTriggerParamBool(TriggerTarget.Item, null, "enableUI", false);

            triggerInfo.AddOnReleaseItemTrigger(enableUI_Off);

            /* Define : UseItemTrigger */
            var shootUnlessReloading = triggerInfo.CreateTriggerParamSignal(TriggerTarget.Item, null, "ShootUnlessReloading");
            triggerInfo.AddUseItemTrigger_Down(shootUnlessReloading);

            /* Define : ItemLogic */
            {
                /* On receive */
                var onReceive = LogicParamGenerator.CreateOnReceiveKey(GimmickTarget.Item, "ShootUnlessReloading");

                /* Logic */
                // if (!reloading) { SendSignal(Item, "ShootOrReload") }
                var sendSignal = LogicParamGenerator.CreateSingleStatement(
                        LogicParamGenerator.CreateExpression_IF(Operator.Not, GimmickTarget.Item, "reloading"),
                        new Base.TargetState(TargetStateTarget.Item, "ShootOrReload", ParameterType.Signal));

                var logic = LogicParamGenerator.CreateLogic_AtSingleStatement(sendSignal);

                /* LogicParam */
                logicInfo.AddItemLogicParam(new LogicParam(onReceive, logic));
            }

        }

        public void AddComponent(GameObjectCreator gameObjectCreator) {

            gameObjectCreator.AddItem(itemInfo);
            gameObjectCreator.AddTrigger(triggerInfo);

            gameObjectCreator.AddLogic(logicInfo);
        }

    }
    
}

