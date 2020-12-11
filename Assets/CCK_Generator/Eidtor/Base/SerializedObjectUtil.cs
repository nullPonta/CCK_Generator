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
                else if (inputTrigger.Type == ParameterType.Integer) {
                    var intValue = typeValue.FindPropertyRelative("integerValue");
                    intValue.intValue = inputTrigger.RawValue.IntegerValue;
                }

            }

            serializedObject.ApplyModifiedProperties();
        }

        public static void SetLogicValue(Object obj, string propertyName) {
            var serializedObject = CreateSerializedObject(obj);
            var prop = serializedObject.FindProperty(propertyName);

            for (int cnt_i = 0; cnt_i < prop.arraySize; cnt_i++) {
                

            }

            PrintAll(obj);

            serializedObject.ApplyModifiedProperties();
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

        static void PrintAll(Object obj) {

            var so = new SerializedObject(obj);
            var sp = so.GetIterator();

            Debug.Log("--- SerializedProperty ---");

            while (sp.Next(true)) {
                Debug.Log(sp.propertyType + " : " + sp.propertyPath + " : " + sp.displayName);
            }

        }

    }

}
