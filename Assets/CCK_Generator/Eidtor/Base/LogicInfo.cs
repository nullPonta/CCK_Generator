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

            /* GimmickKey */
            var gimmickKey = new GimmickKey(GimmickTarget.Item, "ShootUnlessReloading");

            /* TargetState */
            var targetState = new TargetState(TargetStateTarget.Item, "ShootOrReload", ParameterType.Signal);

            /* Expression */
            var value = new Value();
            var operatorExpression = new OperatorExpression();
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
        public Value Value;
        public OperatorExpression OperatorExpression;

        public Expression(ExpressionType type, Value value, OperatorExpression operatorExpression) {
            Type = type;
            Value = value;
            OperatorExpression = operatorExpression;
        }
    }

    public class Value
    {
        public bool BoolValue;
        public float FloatValue;
        public int IntegerValue;
    }

    public class OperatorExpression
    {
        public OperatorExpression() {

        }

        public Operator Operator;
        public Expression[] Operands;
    }

}