#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Core;

namespace Shop.EditorTools
{
    [CustomPropertyDrawer(typeof(StatPlusBinding))]
    public sealed class StatPlusBindingDrawer : PropertyDrawer
    {
        private const string InfoPropName  = "Info";
        private const string OpPropName    = "Operation";
        private const string ParamPropName = "Param";

        // Небольшой отступ между элементами массива/списка
        private const float BottomSpacing = 6f;   // подбери по вкусу (4–10)

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var infoProp  = property.FindPropertyRelative(InfoPropName);
            var opProp    = property.FindPropertyRelative(OpPropName);
            var paramProp = property.FindPropertyRelative(ParamPropName);

            float line = EditorGUIUtility.singleLineHeight;
            float pad  = EditorGUIUtility.standardVerticalSpacing;

            // Сужаем position, чтобы снизу остался визуальный "хвост" (BottomSpacing)
            var contentRect = new Rect(position.x, position.y, position.width, position.height - BottomSpacing);

            // 1) Info
            var r1 = new Rect(contentRect.x, contentRect.y, contentRect.width, line);
            EditorGUI.PropertyField(r1, infoProp);

            // 2) Operation
            var r2 = new Rect(contentRect.x, r1.yMax + pad, contentRect.width, line);
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(r2, opProp);
            bool opChanged = EditorGUI.EndChangeCheck();

            var opObj = opProp.objectReferenceValue as PlayerDataOperation;

            if (opChanged)
            {
                if (opObj is IOperationWithParameter withParam)
                {
                    var cur = paramProp?.managedReferenceValue as IOperationParameter;
                    if (cur == null || !withParam.IsSupports(cur))
                    {
                        paramProp.managedReferenceValue = withParam.CreateDefaultParam();
                        property.serializedObject.ApplyModifiedProperties();
                    }
                }
                else
                {
                    if (paramProp != null && paramProp.managedReferenceValue != null)
                    {
                        paramProp.managedReferenceValue = null;
                        property.serializedObject.ApplyModifiedProperties();
                    }
                }
            }

            // 3) Param (если нужен)
            if (opObj is IOperationWithParameter && paramProp != null && paramProp.managedReferenceValue != null)
            {
                var ph = EditorGUI.GetPropertyHeight(paramProp, includeChildren: true);
                var r3 = new Rect(contentRect.x, r2.yMax + pad, contentRect.width, ph);
                EditorGUI.PropertyField(r3, paramProp, includeChildren: true);
            }

            // (необязательно) тонкая разделительная линия внизу элемента
            // var lineRect = new Rect(position.x, position.yMax - BottomSpacing + 2f, position.width, 1f);
            // EditorGUI.DrawRect(lineRect, new Color(0,0,0,0.15f));

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float h   = EditorGUIUtility.singleLineHeight; // Info
            float pad = EditorGUIUtility.standardVerticalSpacing;

            h += pad + EditorGUIUtility.singleLineHeight;  // Operation

            var opProp    = property.FindPropertyRelative(OpPropName);
            var paramProp = property.FindPropertyRelative(ParamPropName);

            var opObj = opProp?.objectReferenceValue as PlayerDataOperation;
            if (opObj is IOperationWithParameter && paramProp != null && paramProp.managedReferenceValue != null)
            {
                h += pad + EditorGUI.GetPropertyHeight(paramProp, includeChildren: true); // Param
            }

            // Хвостовой отступ между элементами
            h += BottomSpacing;
            return h;
        }
    }
}
#endif
