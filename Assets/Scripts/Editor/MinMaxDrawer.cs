﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MinMaxPair))]
[CustomPropertyDrawer(typeof(MinMaxAttribute))]
public class MinMaxDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.serializedObject.isEditingMultipleObjects) return 0f;
        return base.GetPropertyHeight(property, label) + 16f;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.serializedObject.isEditingMultipleObjects) return;

        var minProperty = property.FindPropertyRelative("Min");
        var maxProperty = property.FindPropertyRelative("Max");
        var minmax = attribute as MinMaxAttribute ?? new MinMaxAttribute(0, 1);
        position.height -= 16f;

        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        var min = minProperty.floatValue;
        var max = maxProperty.floatValue;

        var left = new Rect(position.x, position.y, position.width / 2 - 11f, position.height);
        var right = new Rect(position.x + position.width - left.width, position.y, left.width, position.height);
        var mid = new Rect(left.xMax, position.y, 22, position.height);
        min = Mathf.Clamp(EditorGUI.FloatField(left, min), minmax.Min, max);
        EditorGUI.LabelField(mid, " to ");
        max = Mathf.Clamp(EditorGUI.FloatField(right, max), min, minmax.Max);

        position.y += 16f;
        EditorGUI.MinMaxSlider(position, GUIContent.none, ref min, ref max, minmax.Min, minmax.Max);

        minProperty.floatValue = min;
        maxProperty.floatValue = max;
        EditorGUI.EndProperty();
    }
}
