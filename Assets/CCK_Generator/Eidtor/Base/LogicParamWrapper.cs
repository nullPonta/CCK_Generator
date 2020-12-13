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
                        CreateTargetState(constantValue, calculateKey),
                        ope,
                        LogicParamGenerator.CreateExpression_TARGET_OPERAND(GimmickTarget.Item, calculateKey),
                        LogicParamGenerator.CreateExpression_CONSTANT(constantValue));

            return calculate;
        }

        public static SingleStatement SetValue(
            string targetKey,
            ConstantValue constantValue) {

            var sendSignal = LogicParamGenerator.CreateSingleStatement_SETVALUE(
                        CreateTargetState(constantValue, targetKey),
                        constantValue);

            return sendSignal;
        }

        static TargetState CreateTargetState(ConstantValue constantValue, string key) {

            TargetState targetState = null;

            if (constantValue.Type == ParameterType.Bool) {
                targetState = new TargetState(TargetStateTarget.Item, key, ParameterType.Bool);
            }
            else if (constantValue.Type == ParameterType.Float) {
                targetState = new TargetState(TargetStateTarget.Item, key, ParameterType.Float);
            }
            else if (constantValue.Type == ParameterType.Integer) {
                targetState = new TargetState(TargetStateTarget.Item, key, ParameterType.Integer);
            }

            return targetState;
        }

    }

}

