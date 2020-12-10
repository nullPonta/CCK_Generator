using UnityEditor;
using UnityEngine;


namespace Ponta.CCK_Generator
{

    public static class SerializedObjectUtil
    {

        public static void SetValue(Object obj, string propertyName, string value) {

            var serializedObject = CreateSerializedObject(obj);
            var prop = serializedObject.FindProperty(propertyName);
            prop.stringValue = value;

            serializedObject.ApplyModifiedProperties();
        }

        public static void SetValue(Object obj, string propertyName, Transform value) {

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
