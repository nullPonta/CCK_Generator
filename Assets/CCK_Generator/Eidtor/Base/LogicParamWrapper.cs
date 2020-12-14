using ClusterVR.CreatorKit;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Operation;


namespace Ponta.CCK_Generator.Base {

    public class LogicParamWrapper
    {

        public static SingleStatement SendSignalToSelf(
            Operator ope,
            string key,
            string sendKey) {

            var sendSignal = LogicParamGenerator.CreateSingleStatement(
                        new TargetState(TargetStateTarget.Item, sendKey, ParameterType.Signal),
                        ope,
                        LogicParamGenerator.CreateExpression_TARGET_OPERAND(GimmickTarget.Item, key));

            return sendSignal;
        }

        public static SingleStatement SendSignalToSelfByCompare(
            Operator ope,
            string compareKey,
            ConstantValue constantValue,
            string sendKey) {

            var sendSignal = LogicParamGenerator.CreateSingleStatement_COMPARE(
                        new TargetState(TargetStateTarget.Item, sendKey, ParameterType.Signal),
                        ope,
                        LogicParamGenerator.CreateExpression_TARGET_OPERAND(GimmickTarget.Item, compareKey),
                        LogicParamGenerator.CreateExpression_CONSTANT(constantValue));

            return sendSignal;
        }

        public static SingleStatement Calculate(
            Operator ope,
            string calculateKey,
            ConstantValue constantValue) {

            var calculate = LogicParamGenerator.CreateSingleStatement_CALCULATE(
                        CreateTargetState(constantValue.Type, calculateKey),
                        ope,
                        LogicParamGenerator.CreateExpression_TARGET_OPERAND(GimmickTarget.Item, calculateKey),
                        LogicParamGenerator.CreateExpression_CONSTANT(constantValue));

            return calculate;
        }

        public static SingleStatement SetValue(
            string targetKey,
            ConstantValue constantValue) {

            var sendSignal = LogicParamGenerator.CreateSingleStatement_SETVALUE(
                        CreateTargetState(constantValue.Type, targetKey),
                        constantValue);

            return sendSignal;
        }

        public static SingleStatement SetValueFromKey(
            string targetKey,
            ParameterType parameterType,
            string sourceKey) {

            var sendSignal = LogicParamGenerator.CreateSingleStatement_SETVALUE_FROM_KEY(
                        CreateTargetState(parameterType, targetKey),
                        sourceKey);

            return sendSignal;
        }

        static TargetState CreateTargetState(ParameterType parameterType, string key) {
            return new TargetState(TargetStateTarget.Item, key, parameterType);
        }

    }

}

