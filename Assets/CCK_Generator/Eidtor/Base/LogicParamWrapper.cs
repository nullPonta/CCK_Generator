using ClusterVR.CreatorKit;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Operation;


namespace Ponta.CCK_Generator.Base {

    public class LogicParamWrapper
    {

        public static SingleStatement SendSignalToSelf(Operator inOperator, string key, string sendKey) {

            var sendSignal = LogicParamGenerator.CreateSingleStatement(
                        inOperator,
                        LogicParamGenerator.CreateExpression_TARGET_OPERAND(GimmickTarget.Item, key),
                        new Base.TargetState(TargetStateTarget.Item, sendKey, ParameterType.Signal));

            return sendSignal;
        }

        public static SingleStatement SendSignalToSelfByCompare(Operator inOperator, string key, string sendKey) {

            var sendSignal = LogicParamGenerator.CreateSingleStatement_COMPARE(
                        inOperator,
                        LogicParamGenerator.CreateExpression_TARGET_OPERAND(GimmickTarget.Item, "bullets"),
                        LogicParamGenerator.CreateExpression_CONSTANT(new Base.ConstantValue(0), "bullets"),
                        new Base.TargetState(TargetStateTarget.Item, "Shoot", ParameterType.Signal));

            return sendSignal;
        }

    }

}

