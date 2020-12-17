using ClusterVR.CreatorKit;
using ClusterVR.CreatorKit.Item.Implements;
using ClusterVR.CreatorKit.Trigger;


namespace Ponta.CCK_Generator.Base
{

    public static class TriggerParamGenerator
    {

        /* ---------------------------------------------------------------- */
        // TriggerParam
        /* ---------------------------------------------------------------- */
        public static TriggerParam CreateSignal(TriggerTarget target, Item specifiedTargetItem, string key) {
            var value = new TriggerValue();
            var param = new TriggerParam(TriggerTarget.Item, null, key, ParameterType.Signal, value);

            return param;
        }

        public static TriggerParam CreateBool(TriggerTarget target, Item specifiedTargetItem, string key, bool boolValue) {
            var value = new TriggerValue();
            value.BoolValue = boolValue;

            var param = new TriggerParam(TriggerTarget.Item, null, key, ParameterType.Bool, value);

            return param;
        }

        public static TriggerParam CreateFloat(TriggerTarget target, Item specifiedTargetItem, string key, float floatValue) {
            var value = new TriggerValue();
            value.FloatValue = floatValue;

            var param = new TriggerParam(TriggerTarget.Item, null, key, ParameterType.Float, value);

            return param;
        }

        public static TriggerParam CreateInteger(TriggerTarget target, Item specifiedTargetItem, string key, int intValue) {
            var value = new TriggerValue();
            value.IntegerValue = intValue;

            var param = new TriggerParam(TriggerTarget.Item, null, key, ParameterType.Integer, value);

            return param;
        }

    }

}

