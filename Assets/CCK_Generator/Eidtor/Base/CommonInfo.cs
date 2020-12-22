#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.Events;


namespace Ponta.CCK_Generator.Base
{

    public class CommonInfo
    {

        public PrefabsPathController prefabsPathController = new PrefabsPathController();

        public ItemInfo itemInfo = new ItemInfo();

        public TriggerInfo triggerInfo = new TriggerInfo();

        public LogicInfo logicInfo = new LogicInfo();

        public UnityAction<GameObject> OnCreate;


        public void CreatePrefabFromPrototype() {

            var gameObjectCreator = new GameObjectCreator();

            var result = gameObjectCreator.Init(prefabsPathController);
            if (!result) { return; }

            gameObjectCreator.OnCreate = OnCreate;

            AddComponent(gameObjectCreator);
            gameObjectCreator.SaveAsPrefabAsset();
        }

        void AddComponent(GameObjectCreator gameObjectCreator) {

            gameObjectCreator.AddItem(itemInfo);
            gameObjectCreator.AddTrigger(triggerInfo);

            gameObjectCreator.AddLogic(logicInfo);
        }

    }

}
#endif
