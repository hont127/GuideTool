using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Hont
{
    [CustomEditor(typeof(GuideList))]
    public class GuideListInspector : Editor
    {
        ReorderableList mReorderableList;
        public ReorderableList ReorderableList
        {
            get
            {
                if (mReorderableList == null)
                {
                    mReorderableList = new ReorderableList(serializedObject, serializedObject.FindProperty("guideList"), true, true, true, true);

                    mReorderableList.onAddCallback = (list) =>
                    {
                        CheckCacheTypeArrayIsCreated();
                        mCacheGenericMenu.ShowAsContext();
                    };

                    mReorderableList.onRemoveCallback = (list) =>
                    {
                        var guideList = base.target as GuideList;
                        var scriptableObject = guideList.guideList[list.index].guide;
                        guideList.guideList.RemoveAt(list.index);

                        DestroyImmediate(scriptableObject, true);

                        GUI.changed = true;
                        serializedObject.Update();
                        serializedObject.ApplyModifiedProperties();

                        var path = AssetDatabase.GetAssetPath(target);
                        var assetImporter = AssetImporter.GetAtPath(path);
                        assetImporter.SaveAndReimport();
                    };

                    mReorderableList.elementHeightCallback = (int index) =>
                    {
                        var prop = mReorderableList.serializedProperty.GetArrayElementAtIndex(index);

                        var height = 45f;
                        var guide = prop.FindPropertyRelative("guide");

                        height += EditorGUI.GetPropertyHeight(guide);
                        height += 35;

                        if (guide.objectReferenceValue)
                        {
                            var serializeObject = new SerializedObject(guide.objectReferenceValue);
                            var iterator = serializeObject.GetIterator();
                            bool enterChildren = true;
                            bool first = false;
                            while (iterator.NextVisible(enterChildren))
                            {
                                if (!first)
                                {
                                    first = true;
                                    continue;
                                }

                                enterChildren = false;
                                height += EditorGUI.GetPropertyHeight(iterator, true);
                            }
                        }

                        return height;
                    };

                    mReorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                    {
                        EditorGUI.DrawRect(new Rect(rect.x, rect.y, rect.width, rect.height - 8), new Color(0.5f, 0.5f, 0.5f, 0.2f));

                        var height = rect.yMin;

                        var propHeight = 17f;
                        EditorGUI.LabelField(new Rect(rect.x, height, rect.width, propHeight), "step " + (index + 1));
                        height += propHeight;

                        var prop = mReorderableList.serializedProperty.GetArrayElementAtIndex(index);

                        var guide = prop.FindPropertyRelative("guide");
                        var desc = prop.FindPropertyRelative("description");

                        propHeight = EditorGUI.GetPropertyHeight(guide);
                        EditorGUI.ObjectField(new Rect(rect.x, height, rect.width, propHeight), guide, new GUIContent("Guide"));
                        height += propHeight;

                        propHeight = 17;
                        EditorGUI.LabelField(new Rect(rect.x, height, rect.width, propHeight), "Description");
                        height += propHeight;

                        propHeight = 35;
                        desc.stringValue = EditorGUI.TextArea(new Rect(rect.x, height, rect.width, propHeight), desc.stringValue);
                        height += propHeight;

                        EditorGUI.DrawRect(new Rect(rect.x, height - 2, rect.width, rect.yMax - (height + 8)), new Color(0.5f, 0.5f, 0.5f, 0.2f));

                        if (guide.objectReferenceValue)
                        {
                            EditorGUI.indentLevel++;
                            var serializeObject = new SerializedObject(guide.objectReferenceValue);
                            var iterator = serializeObject.GetIterator();
                            bool enterChildren = true;
                            bool first = false;
                            while (iterator.NextVisible(enterChildren))
                            {
                                if (!first)
                                {
                                    first = true;
                                    continue;
                                }

                                enterChildren = false;
                                propHeight = EditorGUI.GetPropertyHeight(iterator);
                                EditorGUI.PropertyField(new Rect(rect.x, height, rect.width, propHeight), iterator);
                                height += propHeight;
                            }

                            serializeObject.ApplyModifiedProperties();

                            EditorGUI.indentLevel--;
                        }
                    };
                }

                return mReorderableList;
            }
        }

        GenericMenu mCacheGenericMenu;
        Type[] mCacheTypeArray;


        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();

            ReorderableList.DoLayoutList();

            if (EditorGUI.EndChangeCheck())
            {
                CheckList();

                GUI.changed = true;
                serializedObject.ApplyModifiedProperties();
            }
        }

        void CheckList()
        {
            for (int i = 0; i < ReorderableList.count; i++)
            {
                for (int j = ReorderableList.count - 1; j >= 0; j--)
                {
                    if (i == j) continue;

                    var prop = ReorderableList.serializedProperty.GetArrayElementAtIndex(j);
                    var x = prop.FindPropertyRelative("guide");

                    prop = ReorderableList.serializedProperty.GetArrayElementAtIndex(i);
                    var y = prop.FindPropertyRelative("guide");

                    if (y.objectReferenceValue != null && x.objectReferenceValue == y.objectReferenceValue)
                    {
                        x.objectReferenceValue = null;
                        i = 0;
                        break;
                    }
                }
            }
        }

        void CheckCacheTypeArrayIsCreated()
        {
            if (mCacheTypeArray == null)
            {
                mCacheTypeArray = GetChildrenClasses<GuideBase>();

                mCacheGenericMenu = new GenericMenu();

                for (int i = 0; i < mCacheTypeArray.Length; i++)
                {
                    var item = mCacheTypeArray[i];

                    mCacheGenericMenu.AddItem(new GUIContent("Empty"), false, () =>
                    {
                        var concertTarget = base.target as GuideList;
                        concertTarget.guideList.Add(new GuideList.GuideStep() { guide = default(GuideBase), description = "" });

                        GUI.changed = true;
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    });

                    mCacheGenericMenu.AddItem(new GUIContent(item.Name), false, () =>
                    {
                        var instanced = CreateInstance(item);
                        instanced.name = item.Name;
                        AssetDatabase.AddObjectToAsset(instanced, AssetDatabase.GetAssetPath(target));

                        var concertTarget = base.target as GuideList;
                        concertTarget.guideList.Add(new GuideList.GuideStep() { guide = instanced as GuideBase, description = "" });

                        GUI.changed = true;
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    });
                }
            }
        }

        Type[] GetChildrenClasses<T>()
        {
            var type = typeof(T);

            var assembly = Assembly.GetAssembly(type);

            var types = assembly.GetExportedTypes();

            var filterTypes = Array.FindAll(types, m => IsSubClassOf(m, type) && !m.IsGenericType && !m.IsAbstract && m != target.GetType());
            return filterTypes;
        }

        public static bool IsSubClassOf(Type type, Type baseType)
        {
            var b = type.BaseType;
            while (b != null)
            {
                if (b.Equals(baseType))
                {
                    return true;
                }
                b = b.BaseType;
            }
            return false;
        }
    }
}
