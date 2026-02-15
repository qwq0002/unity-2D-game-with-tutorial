using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public static class InspectorHotkeys {
    // 折叠所有组件：快捷键 Alt + W （& 是 Alt, # 是 Shift, % 是 Ctrl）
    [MenuItem("MyTools/Inspector/Collapse All &w")]
    public static void CollapseAll() {
        SetAllComponentsExpanded(false);
    }

    // 展开所有组件：快捷键 Alt + Q
    [MenuItem("MyTools/Inspector/Expand All &q")]
    public static void ExpandAll() {
        SetAllComponentsExpanded(true);
    }

    private static void SetAllComponentsExpanded(bool expanded) {
        // 遍历当前选中的所有对象（支持多选）
        foreach (var obj in Selection.objects) {
            if (obj is GameObject go) {
                // 获取对象上的所有组件
                Component[] components = go.GetComponents<Component>();

                foreach (var component in components) {
                    // 设置组件的展开/折叠状态
                    InternalEditorUtility.SetIsInspectorExpanded(component, expanded);
                }

                // 刷新Inspector
                ActiveEditorTracker.sharedTracker.ForceRebuild();
            }
        }
    }
}
