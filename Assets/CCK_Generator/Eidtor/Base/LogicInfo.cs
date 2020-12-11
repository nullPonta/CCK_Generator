using ClusterVR.CreatorKit.Operation.Implements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerTarget = ClusterVR.CreatorKit.Trigger.TriggerTarget;

namespace Ponta.CCK_Generator.Base
{

    public class LogicInfo
    {

        public void AddLogic(GameObject gameObject) {

            var itemLogic = gameObject.AddComponent<ItemLogic>();

            //
            //SerializedObjectUtil.SetEnumValue(itemLogic, "item", (int)TriggerTarget.Item);
            //SerializedObjectUtil.SetStringValue(itemLogic, "key", "TEST");
            SerializedObjectUtil.SetLogicValue(itemLogic, "logic");
        }

    }

}

