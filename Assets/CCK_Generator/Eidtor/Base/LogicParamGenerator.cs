#if UNITY_EDITOR
using ClusterVR.CreatorKit;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using ClusterVR.CreatorKit.Operation;
using System.Collections.Generic;


namespace Ponta.CCK_Generator.Base
{

    public static class LogicParamGenerator
    {
        public static GimmickKey CreateOnReceiveKey(GimmickTarget gimmickTarget, string key) {
            return new GimmickKey(gimmickTarget, key);
        }

        /* ---------------------------------------------------------------- */
        // ExpressionValue
        /* ---------------------------------------------------------------- */
        public static ExpressionValue CreateExpressionValue_EMPTY() {
            return new ExpressionValue(ValueType.Constant, new ConstantValue(false), new SourceState(GimmickTarget.Item, null));
        }

        public static ExpressionValue CreateExpressionValue_COMPARE() {
            return new ExpressionValue(ValueType.RoomState, new ConstantValue(false), new SourceState(GimmickTarget.Item, null));
        }

        public static ExpressionValue CreateExpressionValue_CALCULATE() {
            return new ExpressionValue(ValueType.Constant, new ConstantValue(false), new SourceState(GimmickTarget.Item, null));
        }

        public static ExpressionValue CreateExpressionValue_SETVALUE(ConstantValue constantValue) {
            return new ExpressionValue(ValueType.Constant, constantValue, new SourceState(GimmickTarget.Item, null));
        }

        public static ExpressionValue CreateExpressionValue_SETVALUE_FROM_KEY(ParameterType parameterType, string sourceKey) {
            return new ExpressionValue(ValueType.RoomState, ConstantValue.CreateByType(parameterType), new SourceState(GimmickTarget.Item, sourceKey));
        }

        /* ---------------------------------------------------------------- */
        // Expression
        /* ---------------------------------------------------------------- */
        public static Expression CreateExpression_ROOMSTATE(GimmickTarget sourceTarget, string sourcekey) {

            var value = new ExpressionValue(ValueType.RoomState, new ConstantValue(false), new SourceState(sourceTarget, sourcekey));
            var operatorExpression = new OperatorExpression(Operator.Not, null);

            return new Expression(ExpressionType.Value, value, operatorExpression);
        }

        public static Expression CreateExpression_CONSTANT(ConstantValue constantValue) {

            var value = new ExpressionValue(ValueType.Constant, constantValue, new SourceState(GimmickTarget.Item, null));
            var operatorExpression = new OperatorExpression(Operator.Not, null);

            return new Expression(ExpressionType.Value, value, operatorExpression);
        }

        /* ---------------------------------------------------------------- */
        // SingleStatement
        /* ---------------------------------------------------------------- */
        public static SingleStatement CreateSingleStatement(
            TargetState target,
            Operator inOperator,
            Expression inExpression) {

            /* Expression */
            var expression = new Expression(
                ExpressionType.OperatorExpression,
                CreateExpressionValue_EMPTY(),
                CreateOperatorExpression(inOperator, inExpression));

            return new SingleStatement(target, expression);
        }

        public static SingleStatement CreateSingleStatement_COMPARE(
            TargetState target,
            Operator inOperator,
            Expression inExpression_1st,
            Expression inExpression_2nd) {

            /* Expression */
            var expression = new Expression(
                ExpressionType.OperatorExpression,
                CreateExpressionValue_COMPARE(),
                CreateOperatorExpression(inOperator, inExpression_1st, inExpression_2nd));

            return new SingleStatement(target, expression);
        }

        public static SingleStatement CreateSingleStatement_CALCULATE(
            TargetState target,
            Operator inOperator,
            Expression inExpression_1st,
            Expression inExpression_2nd) {

            /* Expression */
            var expression = new Expression(
                ExpressionType.OperatorExpression,
                CreateExpressionValue_CALCULATE(),
                CreateOperatorExpression(inOperator, inExpression_1st, inExpression_2nd));

            return new SingleStatement(target, expression);
        }

        public static SingleStatement CreateSingleStatement_SETVALUE(
            TargetState target,
            ExpressionValue value) {

            /* Expression */
            var expression = new Expression(
                ExpressionType.Value,
                value,
                CreateOperatorExpression(
                    Operator.Not,
                    CreateExpression_CONSTANT(new ConstantValue(false))));

            return new SingleStatement(target, expression);
        }

        public static SingleStatement CreateSingleStatement_SETVALUE_FROM_KEY(
            TargetState target,
            string sourceKey) {

            /* Expression */
            var expression = new Expression(
                ExpressionType.Value,
                CreateExpressionValue_SETVALUE_FROM_KEY(target.ParameterType, sourceKey),
                CreateOperatorExpression(
                    Operator.Not,
                    CreateExpression_CONSTANT(new ConstantValue(false))));

            return new SingleStatement(target, expression);
        }

        public static SingleStatement CreateSingleStatement_SETVALUE_BY_COMPARE(
            TargetState target,
            Expression inExpression_1st,
            Operator ope,
            Expression inExpression_2nd) {

            /* Expression */
            var expression = new Expression(
                ExpressionType.OperatorExpression,
                CreateExpressionValue_COMPARE(),
                CreateOperatorExpression(
                    ope,
                    inExpression_1st,
                    inExpression_2nd));

            return new SingleStatement(target, expression);
        }

        public static SingleStatement CreateSingleStatement_SETVALUE_BY_CONDITION(
            TargetState target,
            Expression inExpression_1st,
            Expression inExpression_2nd,
            Expression inExpression_3rd) {

            /* Expression */
            var expression = new Expression(
                ExpressionType.OperatorExpression,
                CreateExpressionValue_COMPARE(),
                CreateOperatorExpression(
                    Operator.Condition,
                    inExpression_1st,
                    inExpression_2nd,
                    inExpression_3rd));

            return new SingleStatement(target, expression);
        }

        public static List<SingleStatement>  CreateSingleStatementList(params SingleStatement[] args) {
            return new List<SingleStatement>(args);
        }

        /* ---------------------------------------------------------------- */
        // Logic
        /* ---------------------------------------------------------------- */
        public static Logic CreateLogic(params SingleStatement[] args) {
            var list = CreateSingleStatementList(args);
            return CreateLogic(list);
        }

        public static Logic CreateLogic(List<SingleStatement> singleStatementList) {

            var logic = new Logic();

            foreach(var singleStatement in singleStatementList) {
                var statement = new Statement(singleStatement);
                logic.Statements.Add(statement);
            }

            return logic;
        }

        /* ---------------------------------------------------------------- */
        // OperatorExpression
        /* ---------------------------------------------------------------- */
        static OperatorExpression CreateOperatorExpression(
            Operator inOperator,
            Expression inExpression_1st) {

            /* OperatorExpression */
            var operands = new List<Expression>();
            operands.Add(inExpression_1st);

            OperatorExpression operatorExpression = new OperatorExpression(inOperator, operands);

            return operatorExpression;
        }

        static OperatorExpression CreateOperatorExpression(
            Operator inOperator,
            Expression inExpression_1st,
            Expression inExpression_2nd) {

            var operands = new List<Expression>();
            operands.Add(inExpression_1st);
            operands.Add(inExpression_2nd);

            OperatorExpression operatorExpression = new OperatorExpression(inOperator, operands);

            return operatorExpression;
        }

        static OperatorExpression CreateOperatorExpression(
            Operator inOperator,
            Expression inExpression_1st,
            Expression inExpression_2nd,
            Expression inExpression_3rd) {

            var operands = new List<Expression>();
            operands.Add(inExpression_1st);
            operands.Add(inExpression_2nd);
            operands.Add(inExpression_3rd);

            OperatorExpression operatorExpression = new OperatorExpression(inOperator, operands);

            return operatorExpression;
        }

    }

}
#endif
