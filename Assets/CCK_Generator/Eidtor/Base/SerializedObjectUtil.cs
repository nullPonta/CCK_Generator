using ClusterVR.CreatorKit.Trigger.Implements;
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

        public static void SetValue(Object obj, string propertyName, TriggerParam[] value) {
            SetManagedReferenceValue(obj, propertyName, value);
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
