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
        


        }

        public void CreatePrefabFromPrototype() {
            common.CreatePrefabFromPrototype();
        }

    }

}

