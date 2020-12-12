using ClusterVR.CreatorKit;
using ClusterVR.CreatorKit.Item.Implements;
using ClusterVR.CreatorKit.Trigger.Implements;
using System;
using System.Collections.Generic;
using UnityEngine;
using TriggerTarget = ClusterVR.CreatorKit.Trigger.TriggerTarget;


namespace Ponta.CCK_Generator.Base
{

    public class TriggerInfo
    {
        List<TriggerParam> OnCreateItemTriggerParamList;

        List<TriggerParam> OnGrabItemTriggerParamList;
        List<TriggerParam> OnReleaseItemTriggerParamList;

        List<TriggerParam> UseItemTriggerParamList_Down;
        List<TriggerParam> UseItemTriggerParamList_Up;


        public void AddOnCreateItemTrigger(TriggerParam triggerParam) {
            AddTriggerParamToList(triggerParam, ref OnCreateItemTriggerParamList);
        }

        public void AddOnGrabItemTrigger(TriggerParam triggerParam) {
            AddTriggerParamToList(triggerParam, ref OnGrabItemTriggerParamList);
        }

        public void AddOnReleaseItemTrigger(TriggerParam triggerParam) {
            AddTriggerParamToList(triggerParam, ref OnReleaseItemTriggerParamList);
        }

        public void AddUseItemTrigger_Down(TriggerParam triggerParam) {
            AddTriggerParamToList(triggerParam, ref UseItemTriggerParamList_Down);
        }

        public void AddUseItemTrigger_Up(TriggerParam triggerParam) {
            AddTriggerParamToList(triggerParam, ref UseItemTriggerParamList_Up);
        }

        public void AddTriggerComponent(GameObject gameObject) {

            /* OnCreateItemTrigger */
            if (OnCreateItemTriggerParamList != null) {
                var onCreateItemTrigger = gameObject.AddComponent<OnCreateItemTrigger>();
                SerializedObjectUtil.SetTriggerValue(onCreateItemTrigger, "triggers", OnCreateItemTriggerParamList);
            }

            /* OnGrabItemTrigger */
            if (OnGrabItemTriggerParamList != null) {
                var onGrabItemTrigger = gameObject.AddComponent<OnGrabItemTrigger>();
                SerializedObjectUtil.SetTriggerValue(onGrabItemTrigger, "triggers", OnGrabItemTriggerParamList);
            }

            /* OnReleaseItemTrigger */
            if (OnReleaseItemTriggerParamList != null) {
                var onReleaseItemTrigger = gameObject.AddComponent<OnReleaseItemTrigger>();
                SerializedObjectUtil.SetTriggerValue(onReleaseItemTrigger, "triggers", OnReleaseItemTriggerParamList);
            }

            /* UseItemTrigger */
            if ((UseItemTriggerParamList_Down != null) ||
                (UseItemTriggerParamList_Up != null) ){

                var useItemTrigger = gameObject.AddComponent<UseItemTrigger>();

                if (UseItemTriggerParamList_Down != null) {
                    SerializedObjectUtil.SetTriggerValue(useItemTrigger, "downTriggers", UseItemTriggerParamList_Down);
                }

                if (UseItemTriggerParamList_Up != null) {
                    SerializedObjectUtil.SetTriggerValue(useItemTrigger, "upTriggers", UseItemTriggerParamList_Up);
                }
            }


        }

        public TriggerParam CreateTriggerParamSignal(TriggerTarget target, Item specifiedTargetItem, string key) {
            var value = new TriggerValue();
            var param = new TriggerParam(TriggerTarget.Item, null, key, ParameterType.Signal, value);

            return param;
        }

        public TriggerParam CreateTriggerParamBool(TriggerTarget target, Item specifiedTargetItem, string key, bool boolValue) {
            var value = new TriggerValue();
            value.BoolValue = boolValue;

            var param = new TriggerParam(TriggerTarget.Item, null, key, ParameterType.Bool, value);

            return param;
        }

        public TriggerParam CreateTriggerParamInteger(TriggerTarget target, Item specifiedTargetItem, string key, int intValue) {
            var value = new TriggerValue();
            value.IntegerValue = intValue;

            var param = new TriggerParam(TriggerTarget.Item, null, key, ParameterType.Integer, value);

            return param;
        }

        void AddTriggerParamToList(TriggerParam triggerParam, ref List<TriggerParam> list) {

            if (list == null) {
                list = new List<TriggerParam>();
            }

            list.Add(triggerParam);
        }

    }

    public class TriggerValue
    {
        public bool BoolValue;
        public float FloatValue;
        public int IntegerValue;
    }

}

