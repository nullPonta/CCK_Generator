using ClusterVR.CreatorKit;
using ClusterVR.CreatorKit.Item.Implements;
using System.Collections.Generic;

using TriggerTarget = ClusterVR.CreatorKit.Trigger.TriggerTarget;


namespace Ponta.CCK_Generator.Base
{

    public class TriggerInfo
    {
        public List<TriggerParam> onCreateItemTriggerParamList;


        public void AddOnCreateItemTrigger(TriggerParam triggerParam) {

            if (onCreateItemTriggerParamList == null) {
                onCreateItemTriggerParamList = new List<TriggerParam>();
            }

            onCreateItemTriggerParamList.Add(triggerParam);
        }

        public TriggerParam CreateTriggerParamInteger(TriggerTarget target, Item specifiedTargetItem, string key, int intValue) {
            
            var value = new Value();
            value.IntegerValue = intValue;

            var param = new TriggerParam(TriggerTarget.Item, null, key, ParameterType.Integer, value);

            return param;
        }

    }

    public class TriggerParam
    {
        public TriggerTarget Target;
        public Item SpecifiedTargetItem;
        public string Key;
        public ParameterType Type;
        public Value RawValue;

        public TriggerParam(TriggerTarget target, Item specifiedTargetItem, string key, ParameterType type, Value value) {
            Target = target;
            SpecifiedTargetItem = specifiedTargetItem;
            Key = key;
            Type = type;
            RawValue = value;
        }
    }

    public class Value
    {
        public bool BoolValue;
        public float FloatValue;
        public int IntegerValue;
    }

}

