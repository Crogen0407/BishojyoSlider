using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class BishojyoHierarchy : EditorWindow
    {
        [MenuItem("BishojyoSlider/SliderToolbar")]
        private static void ShowWindow()
        {
            var window = GetWindow<SliderToolbar>();
            window.titleContent = new GUIContent("SliderToolbar");
            window.Show();
        }

        private void OnGUI()
        {
            
        }
    }
}