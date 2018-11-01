//////////////////////////////////////////////////////
// MK Glow	    			                        //
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de | www.michaelkremmel.store //
// Copyright © 2017 All rights reserved.            //
//////////////////////////////////////////////////////
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

namespace MK.Glow
{
    [CustomEditor(typeof(MKGlow))]
    public class MKGlowEditor : Editor
    {
        public static class GuiStyles
        {
            public static GUIStyle header = new GUIStyle("ShurikenModuleTitle")
            {
                font = (new GUIStyle("Label")).font,
                border = new RectOffset(15, 7, 4, 4),
                fixedHeight = 22,
                contentOffset = new Vector2(20f, -2f),
            };

            public static GUIStyle headerCheckbox = new GUIStyle("ShurikenCheckMark");
            public static GUIStyle headerCheckboxMixed = new GUIStyle("ShurikenCheckMarkMixed");
        }

        private const string m_Style = "box";
        private ColorPickerHDRConfig colorPickerHDRConfig = new ColorPickerHDRConfig(0f, 99f, 1 / 99f, 3f);
        private static GUIContent glowTintLabel = new GUIContent("Glow GlowTint", "The glows coloration in full screen mode(only FullscreenGlowType)");

        private SerializedProperty glowType;
        private SerializedProperty samples;
        private SerializedProperty blurSpreadInner;
        private SerializedProperty blurSpreadOuter;
        private SerializedProperty blurIterations;
        private SerializedProperty glowIntensityInner;
        private SerializedProperty glowIntensityOuter;
        private SerializedProperty glowTint;
        private SerializedProperty glowLayer;
        private SerializedProperty threshold;
        private SerializedProperty lensTex;
        private SerializedProperty lensIntensity;
        private SerializedProperty useLens;

        private SerializedProperty showMainBehavior;
        private SerializedProperty showInnerGlowBehavior;
        private SerializedProperty showOuterGlowBehavior;
        private SerializedProperty showLensBehavior;

        [MenuItem("Window/MK/Glow/Add MKGlow To Selection")]
        private static void AddMKGlowToObject()
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                if (obj.GetComponent<MKGlow>() == null)
                    obj.AddComponent<MKGlow>();
            }
        }

        private void OnEnable()
        {
            samples = serializedObject.FindProperty("samples");
            blurSpreadInner = serializedObject.FindProperty("blurSpreadInner");
            blurSpreadOuter = serializedObject.FindProperty("blurSpreadOuter");
            blurIterations = serializedObject.FindProperty("blurIterations");
            glowIntensityInner = serializedObject.FindProperty("glowIntensityInner");
            glowIntensityOuter = serializedObject.FindProperty("glowIntensityOuter");
            glowTint = serializedObject.FindProperty("glowTint");
            glowType = serializedObject.FindProperty("glowType");
            glowLayer = serializedObject.FindProperty("glowLayer");
            threshold = serializedObject.FindProperty("threshold");
            lensTex = serializedObject.FindProperty("lensTex");
            lensIntensity = serializedObject.FindProperty("lensIntensity");
            useLens = serializedObject.FindProperty("useLens");

            showInnerGlowBehavior = serializedObject.FindProperty("showInnerGlowBehavior");
            showOuterGlowBehavior = serializedObject.FindProperty("showOuterGlowBehavior");
            showMainBehavior = serializedObject.FindProperty("showMainBehavior");
            showLensBehavior = serializedObject.FindProperty("showLensBehavior");
        }

        public override void OnInspectorGUI()
        {
            MKGlow mkGlow = (MKGlow)target;

            EditorGUI.BeginChangeCheck();

            serializedObject.Update();

            if(HandleBehavior("Main", ref showMainBehavior))
            {
                EditorGUILayout.PropertyField(glowType);
                if (glowType.enumValueIndex == 0)
                {
                    EditorGUILayout.PropertyField(glowLayer);
                }
                EditorGUILayout.IntSlider(samples, 2, 4, "Samples");
                EditorGUILayout.IntSlider(blurIterations, 1, 10, "Iterations");
                if (glowType.enumValueIndex == 2)
                {
                    threshold.floatValue = EditorGUILayout.FloatField("Threshold", threshold.floatValue);
                    threshold.floatValue = Mathf.Max(0.0f, threshold.floatValue);
                }
                glowTint.colorValue = EditorGUILayout.ColorField(glowTintLabel, glowTint.colorValue, false, false, false, colorPickerHDRConfig);
            }
            if(HandleBehavior("Glow Inner", ref showInnerGlowBehavior))
            {
                EditorGUILayout.Slider(blurSpreadInner, 0.0f, 2.0f, "Spread Inner");
                EditorGUILayout.Slider(glowIntensityInner, 0.0f, 1.0f, "Intensity");
            }
            if (glowType.enumValueIndex != 1)
            {
                if (HandleBehavior("Glow Outer", ref showOuterGlowBehavior))
                {
                    EditorGUILayout.Slider(blurSpreadOuter, 0.0f, 2.0f, "Spread Outer");
                    EditorGUILayout.Slider(glowIntensityOuter, 0.0f, 1.0f, "Intensity");
                }
                if (HandleBehavior("Lens", ref showLensBehavior, ref useLens))
                {
                    if (glowType.enumValueIndex != 1)
                    {
                        EditorGUILayout.ObjectField(lensTex);
                        EditorGUILayout.Slider(lensIntensity, 0.0f, 1.0f, "Lens Intensity");
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();

            EditorGUI.EndChangeCheck();
        }

        private bool HandleBehavior(string title, ref SerializedProperty behavior)
        {
            EditorGUI.showMixedValue = behavior.hasMultipleDifferentValues;
            var rect = GUILayoutUtility.GetRect(16f, 22f, GuiStyles.header);
            rect.x -= 10;
            rect.width += 10;
            var e = Event.current;

            GUI.Box(rect, title, GuiStyles.header);

            var foldoutRect = new Rect(EditorGUIUtility.currentViewWidth * 0.5f, rect.y + 2, 13f, 13f);
            if (behavior.hasMultipleDifferentValues)
            {
                foldoutRect.x -= 13;
                foldoutRect.y -= 2;
            }

            EditorGUI.BeginChangeCheck();
            if (e.type == EventType.MouseDown)
            {
                if (rect.Contains(e.mousePosition))
                {
                    if (behavior.hasMultipleDifferentValues)
                        behavior.boolValue = false;
                    else
                        behavior.boolValue = !behavior.boolValue;
                    e.Use();
                }
            }
            if (EditorGUI.EndChangeCheck())
            {
                if (behavior.boolValue)
                    Undo.RegisterCompleteObjectUndo(this, behavior.displayName + " Show");
                else
                    Undo.RegisterCompleteObjectUndo(this, behavior.displayName + " Hide");
            }

            EditorGUI.showMixedValue = false;

            if (e.type == EventType.Repaint && behavior.hasMultipleDifferentValues)
                EditorStyles.radioButton.Draw(foldoutRect, "", false, false, true, false);
            else
                EditorGUI.Foldout(foldoutRect, behavior.boolValue, "");

            if (behavior.hasMultipleDifferentValues)
                return true;
            else
                return behavior.boolValue;
        }

        private bool HandleBehavior(string title, ref SerializedProperty behavior, ref SerializedProperty feature)
        {
            var rect = GUILayoutUtility.GetRect(16f, 22f, GuiStyles.header);
            rect.x -= 10;
            rect.width += 10;
            var e = Event.current;

            GUI.Box(rect, title, GuiStyles.header);

            var foldoutRect = new Rect(EditorGUIUtility.currentViewWidth * 0.5f, rect.y + 2, 13f, 13f);
            if (behavior.hasMultipleDifferentValues)
            {
                foldoutRect.x -= 13;
                foldoutRect.y -= 2;
            }

            EditorGUI.showMixedValue = feature.hasMultipleDifferentValues;
            var toggleRect = new Rect(rect.x + 4f, rect.y + ((feature.hasMultipleDifferentValues) ? 0.0f : 4.0f), 13f, 13f);
            bool fn = feature.boolValue;
            EditorGUI.BeginChangeCheck();

            fn = EditorGUI.Toggle(toggleRect, "", fn, GuiStyles.headerCheckbox);

            if (EditorGUI.EndChangeCheck())
            {
                feature.boolValue = fn;
                if (feature.boolValue)
                    Undo.RegisterCompleteObjectUndo(this, feature.displayName + " enabled");
                else
                    Undo.RegisterCompleteObjectUndo(this, feature.displayName + " disabled");
            }
            EditorGUI.showMixedValue = false;

            EditorGUI.showMixedValue = behavior.hasMultipleDifferentValues;
            EditorGUI.BeginChangeCheck();
            if (e.type == EventType.MouseDown)
            {
                if (rect.Contains(e.mousePosition))
                {
                    if (behavior.hasMultipleDifferentValues)
                        behavior.boolValue = false;
                    else
                        behavior.boolValue = !behavior.boolValue;
                    e.Use();
                }
            }
            if (EditorGUI.EndChangeCheck())
            {
                if (behavior.boolValue)
                    Undo.RegisterCompleteObjectUndo(this, behavior.displayName + " Show");
                else
                    Undo.RegisterCompleteObjectUndo(this, behavior.displayName + " Hide");
            }

            EditorGUI.showMixedValue = false;

            if (e.type == EventType.Repaint && behavior.hasMultipleDifferentValues)
                EditorStyles.radioButton.Draw(foldoutRect, "", false, false, true, false);
            else
                EditorGUI.Foldout(foldoutRect, behavior.boolValue, "");

            if (behavior.hasMultipleDifferentValues)
                return true;
            else
                return behavior.boolValue;
        }
    }
}
#endif