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

        public static SingleStatement SendSignalToSelfByCompare(
            Operator inOperator,
            string compareKey,
            ConstantValue constantValue,
            string sendKey) {

            var sendSignal = LogicParamGenerator.CreateSingleStatement_COMPARE(
                        inOperator,
                        LogicParamGenerator.CreateExpression_TARGET_OPERAND(GimmickTarget.Item, compareKey),
                        LogicParamGenerator.CreateExpression_CONSTANT(constantValue),
                        new Base.TargetState(TargetStateTarget.Item, sendKey, ParameterType.Signal));

            return sendSignal;
        }

    }

}

