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

        public static void SetTriggerValue(Object obj, string propertyName, List<TriggerParam> triggerList) {
            var serializedObject = CreateSerializedObject(obj);
            var prop = serializedObject.FindProperty(propertyName);

            prop.arraySize = triggerList.Count;

            for (int cnt_i = 0; cnt_i < prop.arraySize; cnt_i++) {
                var elementprop = prop.GetArrayElementAtIndex(cnt_i);
                var target = elementprop.FindPropertyRelative("target");
                var specifiedTargetItem = elementprop.FindPropertyRelative("specifiedTargetItem");
                var key = elementprop.FindPropertyRelative("key");
                var type = elementprop.FindPropertyRelative("type");
                var typeValue = elementprop.FindPropertyRelative("value");

                var inputTrigger = triggerList[cnt_i];

                target.enumValueIndex = (int)inputTrigger.Target;

                if (inputTrigger.Target == ClusterVR.CreatorKit.Trigger.TriggerTarget.SpecifiedItem) {
                    specifiedTargetItem.managedReferenceValue = inputTrigger.SpecifiedTargetItem;
                }

                key.stringValue = inputTrigger.Key;
                type.enumValueIndex = (int)inputTrigger.Type;

                /* Set value */
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

        public static void SetValue(Object obj, string propertyName, Transform value) {
            SetObjectReferenceValue(obj, propertyName, value);
        }

        public static void SetManagedReferenceValue(Object obj, string propertyName, System.Object value) {
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

    }

}
