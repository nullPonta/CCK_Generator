﻿using ClusterVR.CreatorKit;
using ClusterVR.CreatorKit.Trigger.Implements;
using Ponta.CCK_Generator.Base;
using UnityEditor;

using TriggerTarget = ClusterVR.CreatorKit.Trigger.TriggerTarget;


namespace Ponta.CCK_Generator
{

    public static class HandGunCreator
    {
        [MenuItem("CCK_Generator/Create/HandGun")]
        public static void CreateHandGunPrefab() {

            GameObjectCreator gameObjectCreator = new GameObjectCreator();
            HandGunDefinition handGunDefinition = new HandGunDefinition();

            var result = gameObjectCreator.Init(handGunDefinition);
            if (!result) { return; }

            handGunDefinition.AddComponent(gameObjectCreator);
            gameObjectCreator.SaveAsPrefabAsset();
        }

    }

    public class HandGunDefinition : BaseDefinition
    {
        ItemInfo itemInfo = new ItemInfo();

        TriggerInfo triggerInfo = new TriggerInfo();

        LogicInfo logicInfo = new LogicInfo();


        public HandGunDefinition() {

            OutputPath = "HandGun.prefab";
            PrototypePath = "Prototype_HandGun.prefab";

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

        }

        public void AddComponent(GameObjectCreator gameObjectCreator) {

            gameObjectCreator.AddItem(itemInfo);
            gameObjectCreator.AddTrigger(triggerInfo);
            gameObjectCreator.AddLogic(logicInfo);
        }

    }
    
}

