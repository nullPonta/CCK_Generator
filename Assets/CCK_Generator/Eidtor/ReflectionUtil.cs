﻿using System;
using System.Reflection;
using UnityEngine;


namespace Ponta.CCK_Generator
{


    public static class ReflectionUtil
    {

        public static void SetValue(System.Object obj, string propertyName, System.Object value) {

            var type = obj.GetType();
            var flags = BindingFlags.Instance | BindingFlags.Public;
            var property = type.GetProperty(propertyName, flags);

            property.SetValue(obj, value);
        }

        public static void SetValueByField(System.Object obj, string propertyName, string value) {

            var type = obj.GetType();
            var flags = BindingFlags.NonPublic | BindingFlags.Instance;
            var field = type.GetField(propertyName, flags);

            field.SetValue(type, value);
        }

        public static void PrintFields(System.Object obj) {
            Type type = obj.GetType();

            var flags = BindingFlags.Instance | BindingFlags.NonPublic;
            var fields = type.GetFields(flags);

            Debug.Log("--- Fields : NonPublic ---");
            foreach (var m in fields) {
                Debug.Log(m.Name);
            }

            flags = BindingFlags.Instance | BindingFlags.Public;
            fields = type.GetFields(flags);

            Debug.Log("--- Fields : Public ---");
            foreach (var m in fields) {
                Debug.Log(m.Name);
            }

        }

        public static void PrintProperty(System.Object obj) {
            Type type = obj.GetType();

            var flags = BindingFlags.Instance | BindingFlags.NonPublic;
            var properties = type.GetProperties(flags);

            Debug.Log("--- Propertys : NonPublic ---");
            foreach (var m in properties) {
                Debug.Log(m.Name);
            }

            flags = BindingFlags.Instance | BindingFlags.Public;
            properties = type.GetProperties(flags);

            Debug.Log("--- Propertys : Public ---");
            foreach (var m in properties) {
                Debug.Log(m.Name);
            }

        }


    }

}