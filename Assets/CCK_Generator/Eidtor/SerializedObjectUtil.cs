using UnityEditor;
using UnityEngine;


namespace Ponta.CCK_Generator
{

    public static class SerializedObjectUtil
    {

        public static void SetValue(Object obj, string propertyName, string value) {

            var serializedObject = new SerializedObject(obj);
            serializedObject.Update();

            var prop = serializedObject.FindProperty(propertyName);
            prop.stringValue = value;

            serializedObject.ApplyModifiedProperties();
        }

    }

}
