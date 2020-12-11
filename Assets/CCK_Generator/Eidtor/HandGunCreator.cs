using ClusterVR.CreatorKit;
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

            gameObjectCreator.AddItem(handGunDefinition.itemInfo);
            gameObjectCreator.AddTrigger(handGunDefinition.triggerInfo);
            gameObjectCreator.SaveAsPrefabAsset();
        }

    }

    public class HandGunDefinition : BaseDefinition
    {
        public ItemInfo itemInfo = new ItemInfo();

        public TriggerInfo triggerInfo = new TriggerInfo();


        public HandGunDefinition() {

            OutputPath = "HandGun.prefab";
            PrototypePath = "Prototype_HandGun.prefab";

            /* Define Item  */
            itemInfo.isItem = true;
            itemInfo.itemName = "ハンドガン";

            itemInfo.isMovableItem = true;

            itemInfo.isGrabbableItem = true;

            /* Define */
            var bullets = triggerInfo.CreateTriggerParamInteger(TriggerTarget.Item, null, "bullets", 6);
            triggerInfo.AddOnCreateItemTrigger(bullets);

            var maxBullets = triggerInfo.CreateTriggerParamInteger(TriggerTarget.Item, null, "maxBullets", 6);
            triggerInfo.AddOnCreateItemTrigger(maxBullets);




        }

    }
       


}

