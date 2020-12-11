using ClusterVR.CreatorKit;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using ClusterVR.CreatorKit.Operation;
using ClusterVR.CreatorKit.Operation.Implements;
using System.Collections.Generic;
using UnityEngine;


namespace Ponta.CCK_Generator.Base
{

    public class LogicInfo
    {

        public void AddLogic(GameObject gameObject) {

            var itemLogic = gameObject.AddComponent<ItemLogic>();

            /* On receive key : GimmickKey */
            var gimmickKey = new GimmickKey(GimmickTarget.Item, "ShootUnlessReloading");

            /* TargetState */
            var targetState = new TargetState(TargetStateTarget.Item, "ShootOrReload", ParameterType.Signal);

            /* Expression */
            var value = new ExpressionValue(ValueType.Constant, new ConstantValue(false), new SourceState(GimmickTarget.Item, null));

            OperatorExpression operatorExpression;

            {
                var operands = new List<Expression>();

                var valueB = new ExpressionValue(ValueType.RoomState, new ConstantValue(false), new SourceState(GimmickTarget.Item, "reloading"));
                var operatorExpressionB = new OperatorExpression(Operator.Not, null);

                var operand = new Expression(ExpressionType.Value, valueB, operatorExpressionB);

                operands.Add(operand);

                operatorExpression = new OperatorExpression(Operator.Not, operands);
            }

            var expression = new Expression(ExpressionType.OperatorExpression, value, operatorExpression);

            /* Logic */
            var singleStatement = new SingleStatement(targetState, expression);
            var statement = new Statement(singleStatement);
            var logic = new Logic();

            logic.Statements.Add(statement);

            LogicParam logicParam = new LogicParam(gimmickKey, logic);

            SerializedObjectUtil.SetLogicValue(itemLogic, "logic", logicParam);
        }

    }

    public class Logic
    {
        public List<Statement> Statements;

        public Logic() {
            Statements = new List<Statement>();
        }
    }

    public class Statement
    {
        public SingleStatement SingleStatement;

        public Statement(SingleStatement singleStatement) {
            SingleStatement = singleStatement;
        }
    }

    public class SingleStatement
    {
        public TargetState TargetState;
        public Expression Expression;

        public SingleStatement(TargetState targetState, Expression expression) {
            TargetState = targetState;
            Expression = expression;
        }
    }

    public class TargetState
    {
        public TargetStateTarget Target;
        public string Key;
        public ParameterType ParameterType;

        public TargetState(TargetStateTarget target, string key, ParameterType parameterType) {
            Target = target;
            Key = key;
            ParameterType = parameterType;
        }
    }

    public class Expression
    {
        public ExpressionType Type;
        public ExpressionValue Value;
        public OperatorExpression OperatorExpression;

        public Expression(ExpressionType type, ExpressionValue value, OperatorExpression operatorExpression) {
            Type = type;
            Value = value;
            OperatorExpression = operatorExpression;
        }
    }

    public class ExpressionValue
    {
        public ValueType Type;
        public ConstantValue Constant;
        public SourceState SourceState;

        public ExpressionValue(ValueType type, ConstantValue constant, SourceState sourceState) {
            Type = type;
            Constant = constant;
            SourceState = sourceState;
        }
    }

    public class OperatorExpression
    {
        public OperatorExpression(Operator inOperator, List<Expression> inOperands) {
            Operator = inOperator;
            Operands = inOperands;
        }

        public Operator Operator;
        public List<Expression> Operands;
    }

    public class ConstantValue
    {
        public ParameterType Type = ParameterType.Bool;
        public bool BoolValue;
        public float FloatValue;
        public int IntegerValue;

        public ConstantValue() {
            Type = ParameterType.Signal;
        }

        public ConstantValue(bool boolValue) {
            Type = ParameterType.Bool;
            BoolValue = boolValue;
        }

        public ConstantValue(float floatValue) {
            Type = ParameterType.Float;
            FloatValue = floatValue;
        }

        public ConstantValue(int integerValue) {
            Type = ParameterType.Bool;
            IntegerValue = integerValue;
        }

    }

    public class SourceState
    {
        public GimmickTarget Target;
        public string Key;

        public SourceState(GimmickTarget target, string key) {
            Target = target;
            Key = key;
        }
    }

}
