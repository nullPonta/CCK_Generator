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

        public static ExpressionValue CreateEmptyExpressionValue() {
            return new ExpressionValue(ValueType.Constant, new ConstantValue(false), new SourceState(GimmickTarget.Item, null));
        }

        public static Expression CreateExpression_IF(Operator inOperator, GimmickTarget target, string key) {

            var valueB = new ExpressionValue(ValueType.RoomState, new ConstantValue(false), new SourceState(target, key));
            var operatorExpressionB = new OperatorExpression(inOperator, null);

            return new Expression(ExpressionType.Value, valueB, operatorExpressionB);
        }

        public static SingleStatement CreateSingleStatement(Expression inExpression, TargetState target) {

            /* OperatorExpression */
            var operands = new List<Expression>();
            operands.Add(inExpression);

            OperatorExpression operatorExpression = new OperatorExpression(Operator.Not, operands);

            /* Expression */
            var emptyValue = LogicParamGenerator.CreateEmptyExpressionValue();
            var expression = new Expression(ExpressionType.OperatorExpression, emptyValue, operatorExpression);

            return new SingleStatement(target, expression);
        }

        public static Logic CreateLogic_AtSingleStatement(SingleStatement singleStatement) {

            var statement = new Statement(singleStatement);
            var logic = new Logic();

            logic.Statements.Add(statement);

            return logic;
        }

    }

}
