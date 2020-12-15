using ClusterVR.CreatorKit;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Ponta.CCK_Generator.Base
{

    public static class SerializedObjectUtil
    {

        public static void SetStringValue(Object obj, string propertyName, string value) {
            var serializedObject = CreateSerializedObject(obj);
            var prop = serializedObject.FindProperty(propertyName);

            prop.stringValue = value;
            serializedObject.ApplyModifiedProperties();
        }

        public static void SetEnumValue(Object obj, string propertyName, int value) {
            var serializedObject = CreateSerializedObject(obj);
            var prop = serializedObject.FindProperty(propertyName);

            prop.enumValueIndex = value;
            serializedObject.ApplyModifiedProperties();
        }

        public static void SetTriggerValue(Object obj, string propertyName, List<TriggerParam> triggerList) {
            var serializedObject = CreateSerializedObject(obj);
            var prop = serializedObject.FindProperty(propertyName);

            prop.arraySize = triggerList.Count;

            for (int cnt_i = 0; cnt_i < prop.arraySize; cnt_i++) {
                var element = prop.GetArrayElementAtIndex(cnt_i);
                var inputTrigger = triggerList[cnt_i];

                /* target */
                var target = element.FindPropertyRelative("target");
                target.enumValueIndex = (int)inputTrigger.Target;

                /* specifiedTargetItem */
                if (inputTrigger.Target == ClusterVR.CreatorKit.Trigger.TriggerTarget.SpecifiedItem) {
                    var specifiedTargetItem = element.FindPropertyRelative("specifiedTargetItem");
                    specifiedTargetItem.managedReferenceValue = inputTrigger.SpecifiedTargetItem;
                }

                /* key */
                var key = element.FindPropertyRelative("key");
                key.stringValue = inputTrigger.Key;

                /* type */
                var type = element.FindPropertyRelative("type");
                type.enumValueIndex = (int)inputTrigger.Type;

                /* Set value */
                var typeValue = element.FindPropertyRelative("value");

                if (inputTrigger.Type == ParameterType.Bool) {
                    var boolValue = typeValue.FindPropertyRelative("boolValue");
                    boolValue.boolValue = inputTrigger.RawValue.BoolValue;
                }
                else if (inputTrigger.Type == ParameterType.Float) {
                    var floatValue = typeValue.FindPropertyRelative("floatValue");
                    floatValue.floatValue = inputTrigger.RawValue.FloatValue;
                }
                else if (inputTrigger.Type == ParameterType.Integer) {
                    var intValue = typeValue.FindPropertyRelative("integerValue");
                    intValue.intValue = inputTrigger.RawValue.IntegerValue;
                }

            }

            serializedObject.ApplyModifiedProperties();
        }

        public static void SetLogicValue(Object obj, string propertyName, LogicParam logicParam) {
            var serializedObject = CreateSerializedObject(obj);

            /* key */
            var keyType = serializedObject.FindProperty("key");

            var target = keyType.FindPropertyRelative("target");
            target.enumValueIndex = (int)logicParam.GimmickKey.Target;

            var key = keyType.FindPropertyRelative("key");
            key.stringValue = logicParam.GimmickKey.Key;

            /* logc */
            var prop = serializedObject.FindProperty(propertyName);
            var statements = prop.FindPropertyRelative("statements");

            statements.arraySize = logicParam.Logic.Statements.Count;

            for (int cnt_i = 0; cnt_i < statements.arraySize; cnt_i++) {
                var statement = statements.GetArrayElementAtIndex(cnt_i);
                var singleStatement = statement.FindPropertyRelative("singleStatement");

                var inSingleStatement = logicParam.Logic.Statements[cnt_i].SingleStatement;

                /* targetState */
                var targetState = singleStatement.FindPropertyRelative("targetState");
                targetState.FindPropertyRelative("target").enumValueIndex = (int)inSingleStatement.TargetState.Target;
                targetState.FindPropertyRelative("key").stringValue = inSingleStatement.TargetState.Key;
                targetState.FindPropertyRelative("parameterType").enumValueIndex = (int)inSingleStatement.TargetState.ParameterType;

                /* expression */
                var expression = singleStatement.FindPropertyRelative("expression");

                SetExpression(inSingleStatement.Expression, expression);
            }

            serializedObject.ApplyModifiedProperties();
        }

        static void SetExpression(Expression fromExpression, SerializedProperty toExpression) {

            toExpression.FindPropertyRelative("type").enumValueIndex = (int)fromExpression.Type;

            /* value */
            var value = toExpression.FindPropertyRelative("value");
            value.FindPropertyRelative("type").enumValueIndex = (int)fromExpression.Value.Type;

            value.FindPropertyRelative("constant.type").enumValueIndex = (int)fromExpression.Value.Constant.Type;
            value.FindPropertyRelative("constant.boolValue").boolValue = fromExpression.Value.Constant.BoolValue;
            value.FindPropertyRelative("constant.floatValue").floatValue = fromExpression.Value.Constant.FloatValue;
            value.FindPropertyRelative("constant.integerValue").intValue = fromExpression.Value.Constant.IntegerValue;

            value.FindPropertyRelative("sourceState.target").enumValueIndex = (int)fromExpression.Value.SourceState.Target;
            value.FindPropertyRelative("sourceState.key").stringValue = fromExpression.Value.SourceState.Key;

            /* operatorExpression */
            var operatorExpression = toExpression.FindPropertyRelative("operatorExpression");
            operatorExpression.FindPropertyRelative("operator").enumValueIndex = (int)fromExpression.OperatorExpression.Operator;

            if (fromExpression.OperatorExpression.Operands != null) {
                var operands = operatorExpression.FindPropertyRelative("operands");

                foreach(var operand in fromExpression.OperatorExpression.Operands) {
                    operands.arraySize++;
                    var operandSp = operands.GetArrayElementAtIndex(operands.arraySize-1);

                    SetExpressionAtLostType(operand, operandSp);
                }
            }

        }

        static void SetExpressionAtLostType(Expression fromExpression, SerializedProperty toExpression) {

            toExpression.FindPropertyRelative("type").intValue = (int)fromExpression.Type;

            /* value */
            var value = toExpression.FindPropertyRelative("value");
            value.FindPropertyRelative("type").intValue = (int)fromExpression.Value.Type;

            value.FindPropertyRelative("constant.type").intValue = (int)fromExpression.Value.Constant.Type;
            value.FindPropertyRelative("constant.boolValue").boolValue = fromExpression.Value.Constant.BoolValue;
            value.FindPropertyRelative("constant.floatValue").floatValue = fromExpression.Value.Constant.FloatValue;
            value.FindPropertyRelative("constant.integerValue").intValue = fromExpression.Value.Constant.IntegerValue;

            value.FindPropertyRelative("sourceState.target").intValue = (int)fromExpression.Value.SourceState.Target;
            value.FindPropertyRelative("sourceState.key").stringValue = fromExpression.Value.SourceState.Key;

            /* operatorExpression */
            var operatorExpression = toExpression.FindPropertyRelative("operatorExpression");
            operatorExpression.FindPropertyRelative("operator").intValue = (int)fromExpression.OperatorExpression.Operator;

            if (fromExpression.OperatorExpression.Operands != null) {
                var operands = operatorExpression.FindPropertyRelative("operands");

                operands.arraySize++;
                var operand = operands.GetArrayElementAtIndex(0);

                SetExpressionAtLostType(fromExpression.OperatorExpression.Operands[0], operand);
            }

        }

        public static void SetTransformValue(Object obj, string propertyName, Transform value) {
            SetObjectReferenceValue(obj, propertyName, value);
        }

        static void SetManagedReferenceValue(Object obj, string propertyName, System.Object value) {
            var serializedObject = CreateSerializedObject(obj);
            var prop = serializedObject.FindProperty(propertyName);

            prop.managedReferenceValue = value;
            serializedObject.ApplyModifiedProperties();
        }

        static void SetObjectReferenceValue(Object obj, string propertyName, Object value) {
            var serializedObject = CreateSerializedObject(obj);
            var prop = serializedObject.FindProperty(propertyName);

            prop.objectReferenceValue = value;
            serializedObject.ApplyModifiedProperties();
        }

        static SerializedObject CreateSerializedObject(Object obj) {
            var serializedObject = new SerializedObject(obj);
            serializedObject.Update();

            return serializedObject;
        }

        static void PrintAllProperty(Object obj) {

            var so = new SerializedObject(obj);
            var sp = so.GetIterator();

            Debug.Log("--- SerializedProperty ---");

            while (sp.Next(true)) {
                Debug.Log(sp.propertyType + " : " + sp.propertyPath + " : " + sp.displayName);
            }

        }
        static void PrintAllProperty(SerializedProperty inSp) {

            var e = inSp.GetEnumerator();

            while (e.MoveNext()) {
                SerializedProperty sp = e.Current as SerializedProperty;
                Debug.Log(sp.propertyType + " : " + sp.propertyPath + " : " + sp.displayName);
            }

        }

    }

}
