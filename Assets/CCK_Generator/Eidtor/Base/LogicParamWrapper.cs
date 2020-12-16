﻿using ClusterVR.CreatorKit;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Operation;

using LPG = Ponta.CCK_Generator.Base.LogicParamGenerator;


namespace Ponta.CCK_Generator.Base {

    public class LogicParamWrapper
    {

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
            Operator ope,
            string compareKey,
            ConstantValue constantValue,
            string sendKey) {

            var sendSignal = LPG.CreateSingleStatement_COMPARE(
                        new TargetState(TargetStateTarget.Item, sendKey, ParameterType.Signal),
                        ope,
                        LPG.CreateExpression_ROOMSTATE(GimmickTarget.Item, compareKey),
                        LPG.CreateExpression_CONSTANT(constantValue));

            return sendSignal;
        }

        public static SingleStatement SendSignalToSelfByCompare(
            Operator ope,
            string compareKeyleft,
            string compareKeyRight,
            string sendKey) {

            var sendSignal = LPG.CreateSingleStatement_COMPARE(
                        new TargetState(TargetStateTarget.Item, sendKey, ParameterType.Signal),
                        ope,
                        LPG.CreateExpression_ROOMSTATE(GimmickTarget.Item, compareKeyleft),
                        LPG.CreateExpression_ROOMSTATE(GimmickTarget.Item, compareKeyRight));

            return sendSignal;
        }

        public static SingleStatement Calculate(
            Operator ope,
            string targetKey,
            ConstantValue constantValue) {

            var calculate = LPG.CreateSingleStatement_CALCULATE(
                        CreateTargetState(constantValue.Type, targetKey),
                        ope,
                        LPG.CreateExpression_ROOMSTATE(GimmickTarget.Item, targetKey),
                        LPG.CreateExpression_CONSTANT(constantValue));

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

            var statement = LPG.CreateSingleStatement_SETVALUE_BY_COMPARE(
                        CreateTargetState(targetType, targetKey),
                        LPG.CreateExpression_ROOMSTATE(GimmickTarget.Item, key_1st),
                        ope,
                        LPG.CreateExpression_CONSTANT(constantValue_2nd));

            return statement;
        }

        static TargetState CreateTargetState(ParameterType parameterType, string key) {
            return new TargetState(TargetStateTarget.Item, key, parameterType);
        }

    }

}

