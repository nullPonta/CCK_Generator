using ClusterVR.CreatorKit;
using ClusterVR.CreatorKit.Item.Implements;
using ClusterVR.CreatorKit.Trigger.Implements;
using System.Collections.Generic;
using UnityEngine;
using TriggerTarget = ClusterVR.CreatorKit.Trigger.TriggerTarget;


namespace Ponta.CCK_Generator.Base
{

    public class TriggerInfo
    {
        List<TriggerParam> OnCreateItemTriggerParamList;

        List<TriggerParam> UseItemTriggerParamList_Down;
        List<TriggerParam> UseItemTriggerParamList_Up;


        public void AddOnCreateItemTrigger(TriggerParam triggerParam) {
            AddTriggerParamToList(triggerParam, ref OnCreateItemTriggerParamList);
        }

        public void AddUseItemTrigger_Down(TriggerParam triggerParam) {
            AddTriggerParamToList(triggerParam, ref UseItemTriggerParamList_Down);
        }

        public void AddUseItemTrigger_Up(TriggerParam triggerParam) {
            AddTriggerParamToList(triggerParam, ref UseItemTriggerParamList_Up);
        }

        public void AddTrigger(GameObject gameObject) {

            /* OnCreateItemTrigger */
            if (OnCreateItemTriggerParamList != null) {
                var onCreateItemTrigger = gameObject.AddComponent<OnCreateItemTrigger>();
                SerializedObjectUtil.SetTriggerValue(onCreateItemTrigger, "triggers", OnCreateItemTriggerParamList);
            }

            /* UseItemTrigger */
            if ((UseItemTriggerParamList_Down != null) ||
                (UseItemTriggerParamList_Up != null) ){

                var onCreateItemTrigger = gameObject.AddComponent<UseItemTrigger>();

                if (UseItemTriggerParamList_Down != null) {
                    SerializedObjectUtil.SetTriggerValue(onCreateItemTrigger, "downTriggers", UseItemTriggerParamList_Down);
                }

                if (UseItemTriggerParamList_Up != null) {
                    SerializedObjectUtil.SetTriggerValue(onCreateItemTrigger, "upTriggers", UseItemTriggerParamList_Up);
                }
            }


        }

        public TriggerParam CreateTriggerParamInteger(TriggerTarget target, Item specifiedTargetItem, string key, int intValue) {
            var value = new Value();
            value.IntegerValue = intValue;

            var param = new TriggerParam(TriggerTarget.Item, null, key, ParameterType.Integer, value);

            return param;
        }

        public TriggerParam CreateTriggerParamSignal(TriggerTarget target, Item specifiedTargetItem, string key) {
            var value = new Value();
            var param = new TriggerParam(TriggerTarget.Item, null, key, ParameterType.Signal, value);

            return param;
        }

        void AddTriggerParamToList(TriggerParam triggerParam, ref List<TriggerParam> list) {

            if (list == null) {
                list = new List<TriggerParam>();
            }

            list.Add(triggerParam);
        }

    }


    public class Value
    {
        public bool BoolValue;
        public float FloatValue;
        public int IntegerValue;
    }

}

