using Ponta.CCK_Generator.Base;
using UnityEditor;


namespace Ponta.CCK_Generator
{

    public static class HandGunCreator
    {
        [MenuItem("CCK_Generator/Create/New HandGun")]
        public static void CreateHandGunPrefab() {

            GameObjectCreator gameObjectCreator = new GameObjectCreator();
            HandGunDefinition handGunDefinition = new HandGunDefinition();

            var result = gameObjectCreator.Init(handGunDefinition);
            if (!result) { return; }

            gameObjectCreator.AddItem(handGunDefinition.itemInfo);
            gameObjectCreator.AddTrigger();
            gameObjectCreator.SaveAsPrefabAsset();
        }

    }

    public class HandGunDefinition : BaseDefinition
    {
        public ItemInfo itemInfo = new ItemInfo();


        public HandGunDefinition() {

            OutputPath = "HandGun.prefab";
            PrototypePath = "Prototype_HandGun.prefab";

            /* Define Item  */
            itemInfo.isItem = true;
            itemInfo.itemName = "ハンドガン";

            itemInfo.isMovableItem = true;

            itemInfo.isGrabbableItem = true;

            /* Define */



        }

    }
       


}

