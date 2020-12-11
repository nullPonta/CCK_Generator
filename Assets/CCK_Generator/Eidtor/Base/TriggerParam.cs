﻿using ClusterVR.CreatorKit;
using ClusterVR.CreatorKit.Item.Implements;
using ClusterVR.CreatorKit.Trigger;


namespace Ponta.CCK_Generator.Base
{

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
}
