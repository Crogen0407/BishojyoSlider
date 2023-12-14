using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class SliderToolbar : EditorWindow
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
            Vector2 windowSize = new Vector2(position.width, position.height);
            float gap = BishojyoEditorData.gap;
            Rect toolBarRect = new Rect(new Vector2(0, windowSize.y + gap), windowSize);
            
            //Toolbar
            Vector2 panelSize = new Vector2(windowSize.y * BishojyoEditorData.percentX - gap * 5, windowSize.y - gap * 5);
            Vector2 panelPos = new Vector2((windowSize.y * BishojyoEditorData.percentX - gap * 4), 0);
            BishojyoEditorData.sliderValue = GUI.HorizontalScrollbar(new Rect(0, panelSize.y, position.width - gap * 2, 10), BishojyoEditorData.sliderValue, 0.2f, 0, 1);
            
            for (int i = 0; i < BishojyoEditorData.sliderCount; i++)
            {
                GUILayout.BeginHorizontal();
                GUI.Button(
                    new Rect(   
                        panelPos.x * i - BishojyoEditorData.sliderCount * windowSize.x, 
                        panelPos.y,
                        panelSize.x,
                        panelSize.y),
                    (i+1).ToString());
                GUILayout.EndHorizontal();
            }
        }
    }
}