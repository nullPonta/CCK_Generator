using ClusterVR.CreatorKit;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Operation;
using UnityEngine;
using LPG = Ponta.CCK_Generator.Base.LogicParamGenerator;


namespace Ponta.CCK_Generator.Base {

    public class LogicParamWrapper
    {

        /* ---------------------------------------------------------------- */
        // Send Signal
        /* ---------------------------------------------------------------- */
        public static SingleStatement SendSignalToSelf(
            string sendKey) {

            var sendSignal = LPG.CreateSingleStatement_SETVALUE(
                        new TargetState(TargetStateTarget.Item, sendKey, ParameterType.Signal),
                        LPG.CreateExpressionValue_SETVALUE(new Base.ConstantValue(true)));

            return sendSignal;
        }

        public static SingleStatement SendSignalToSelf(
            string key,
            string sendKey) {

            var statement = LPG.CreateSingleStatement_SETVALUE_FROM_KEY(
                        CreateTargetState(ParameterType.Signal, sendKey),
                        key);

            return statement;
        }

        public static SingleStatement SendSignalToSelf(
            Operator ope,
            string key,
            string sendKey) {

            var sendSignal = LPG.CreateSingleStatement(
                        new TargetState(TargetStateTarget.Item, sendKey, ParameterType.Signal),
                        ope,
                        LPG.CreateExpression_ROOMSTATE(GimmickTarget.Item, key));

            return sendSignal;
        }

        public static SingleStatement SendSignalToSelfByCompare(
            string compareKey_1st,
            Operator ope,
            ConstantValue constantValue_2nd,
            string sendKey) {

            OperatorValidator.Validate_IsCompareOperator(ope);

            var sendSignal = LPG.CreateSingleStatement_COMPARE(
                        new TargetState(TargetStateTarget.Item, sendKey, ParameterType.Signal),
                        ope,
                        LPG.CreateExpression_ROOMSTATE(GimmickTarget.Item, compareKey_1st),
                        LPG.CreateExpression_CONSTANT(constantValue_2nd));

            return sendSignal;
        }

        public static SingleStatement SendSignalToSelfByCompare(
            string compareKey_1st,
            Operator ope,
            string compareKey_2nd,
            string sendKey) {

            OperatorValidator.Validate_IsCompareOperator(ope);

            var sendSignal = LPG.CreateSingleStatement_COMPARE(
                        new TargetState(TargetStateTarget.Item, sendKey, ParameterType.Signal),
                        ope,
                        LPG.CreateExpression_ROOMSTATE(GimmickTarget.Item, compareKey_1st),
                        LPG.CreateExpression_ROOMSTATE(GimmickTarget.Item, compareKey_2nd));

            return sendSignal;
        }

        /* ---------------------------------------------------------------- */
        // SetValue
        /* ---------------------------------------------------------------- */
        public static SingleStatement SetValueByCalculate(
            string targetKey,
            Operator ope,
            ConstantValue constantValue) {

            OperatorValidator.Validate_IsCalculateOperator(ope);

            var calculate = LPG.CreateSingleStatement_CALCULATE(
                        CreateTargetState(constantValue.Type, targetKey),
                        ope,
                        LPG.CreateExpression_ROOMSTATE(GimmickTarget.Item, targetKey),
                        LPG.CreateExpression_CONSTANT(constantValue));

            return calculate;
        }

        public static SingleStatement SetValueByCalculate(
            string targetKey,
            ParameterType targetType,
            Operator ope,
            string key_1st,
            ConstantValue constantValue_2nd) {

            OperatorValidator.Validate_IsCalculateOperator(ope);

            var calculate = LPG.CreateSingleStatement_CALCULATE(
                        CreateTargetState(targetType, targetKey),
                        ope,
                        LPG.CreateExpression_ROOMSTATE(GimmickTarget.Item, key_1st),
                        LPG.CreateExpression_CONSTANT(constantValue_2nd));

            return calculate;
        }

        public static SingleStatement SetValue(
            string targetKey,
            ConstantValue constantValue) {

            var statement = LPG.CreateSingleStatement_SETVALUE(
                        CreateTargetState(constantValue.Type, targetKey),
                        LPG.CreateExpressionValue_SETVALUE(constantValue));

            return statement;
        }

        public static SingleStatement SetValueFromKey(
            string targetKey,
            ParameterType targetType,
            string sourceKey) {

            var statement = LPG.CreateSingleStatement_SETVALUE_FROM_KEY(
                        CreateTargetState(targetType, targetKey),
                        sourceKey);

            return statement;
        }

        public static SingleStatement SetValueByCompare(
            string targetKey,
            ParameterType targetType,
            string key_1st,
            Operator ope,
            ConstantValue constantValue_2nd) {

            OperatorValidator.Validate_IsCompareOperator(ope);

            var statement = LPG.CreateSingleStatement_SETVALUE_BY_COMPARE(
                        CreateTargetState(targetType, targetKey),
                        LPG.CreateExpression_ROOMSTATE(GimmickTarget.Item, key_1st),
                        ope,
                        LPG.CreateExpression_CONSTANT(constantValue_2nd));

            return statement;
        }

        public static SingleStatement SetValueByCondition(
            string targetKey,
            ParameterType targetType,
            string key_1st,
            ConstantValue constantValue_2nd,
            string key_3rd) {

            var statement = LPG.CreateSingleStatement_SETVALUE_BY_CONDITION(
                        CreateTargetState(targetType, targetKey),
                        LPG.CreateExpression_ROOMSTATE(GimmickTarget.Item, key_1st),
                        LPG.CreateExpression_CONSTANT(constantValue_2nd),
                        LPG.CreateExpression_ROOMSTATE(GimmickTarget.Item, key_3rd));

            return statement;
        }

        static TargetState CreateTargetState(ParameterType parameterType, string key) {
            return new TargetState(TargetStateTarget.Item, key, parameterType);
        }

    }

}

