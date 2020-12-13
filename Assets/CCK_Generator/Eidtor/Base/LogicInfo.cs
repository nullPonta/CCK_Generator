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
        List<LogicParam> itemLogicParamList;


        public void AddItemLogicParam(LogicParam logicParam) {
            AddLogicParamToList(logicParam, ref itemLogicParamList);
        }

        public void AddLogicComponent(GameObject gameObject) {

            /* ItemLogic */
            if (itemLogicParamList != null) {

                foreach(var logicParam in itemLogicParamList) {
                    var itemLogic = gameObject.AddComponent<ItemLogic>();
                    SerializedObjectUtil.SetLogicValue(itemLogic, "logic", logicParam);
                }
            }
            
        }

        void AddLogicParamToList(LogicParam logicParam, ref List<LogicParam> list) {

            if (list == null) {
                list = new List<LogicParam>();
            }

            list.Add(logicParam);
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
            Type = ParameterType.Integer;
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
