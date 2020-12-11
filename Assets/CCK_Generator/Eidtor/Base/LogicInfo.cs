using ClusterVR.CreatorKit.Operation.Implements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ponta.CCK_Generator.Base
{

    public class LogicInfo
    {

        public void AddLogic(GameObject gameObject) {

            var itemLogic = gameObject.AddComponent<ItemLogic>();

            ReflectionUtil.PrintAll(itemLogic);
            SerializedObjectUtil.SetStringValue(itemLogic, "itemName", "");
        }

    }

}

