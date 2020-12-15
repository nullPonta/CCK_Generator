using ClusterVR.CreatorKit;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Operation;


namespace Ponta.CCK_Generator.Base {

    public class LogicParamWrapper
    {

        public static SingleStatement SendSignalToSelf(
            string key,
            string sendKey) {

            var statement = LogicParamGenerator.CreateSingleStatement_SETVALUE_FROM_KEY(
                        CreateTargetState(ParameterType.Signal, sendKey),
                        key);

            return statement;
        }

        public static SingleStatement SendSignalToSelf(
            Operator ope,
            string key,
            string sendKey) {

            var sendSignal = LogicParamGenerator.CreateSingleStatement(
                        new TargetState(TargetStateTarget.Item, sendKey, ParameterType.Signal),
                        ope,
                        LogicParamGenerator.CreateExpression_ROOMSTATE(GimmickTarget.Item, key));

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
                        LogicParamGenerator.CreateExpression_ROOMSTATE(GimmickTarget.Item, compareKey),
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
                        LogicParamGenerator.CreateExpression_ROOMSTATE(GimmickTarget.Item, calculateKey),
                        LogicParamGenerator.CreateExpression_CONSTANT(constantValue));

            return calculate;
        }

        public static SingleStatement SetValue(
            string targetKey,
            ConstantValue constantValue) {

            var statement = LogicParamGenerator.CreateSingleStatement_SETVALUE(
                        CreateTargetState(constantValue.Type, targetKey),
                        constantValue);

            return statement;
        }

        public static SingleStatement SetValueFromKey(
            string targetKey,
            ParameterType parameterType,
            string sourceKey) {

            var statement = LogicParamGenerator.CreateSingleStatement_SETVALUE_FROM_KEY(
                        CreateTargetState(parameterType, targetKey),
                        sourceKey);

            return statement;
        }

        static TargetState CreateTargetState(ParameterType parameterType, string key) {
            return new TargetState(TargetStateTarget.Item, key, parameterType);
        }

    }

}

