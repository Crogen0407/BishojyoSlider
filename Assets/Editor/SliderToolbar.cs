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
            BishojyoEditorData.Init();
        }

        private void OnGUI()
        {
            BishojyoEditorData.Init();
            Vector2 windowSize = new Vector2(position.width, position.height);
            float gap = BishojyoEditorData.gap;
            Rect toolBarRect = new Rect(new Vector2(0, windowSize.y + gap), windowSize);
            
            //Toolbar
            Vector2 panelSize = new Vector2(windowSize.y * BishojyoEditorData.percentX - gap * 5, windowSize.y - gap * 5);
            Vector2 panelPos = new Vector2((windowSize.y * BishojyoEditorData.percentX - gap * 4), 0.5f);
            BishojyoEditorData.SliderValue = 
                GUI.HorizontalScrollbar(
                    new Rect(0, panelSize.y, position.width - gap * 2, 10),
                    BishojyoEditorData.SliderValue, 
                    200f, 0, BishojyoEditorData.SliderCount * panelSize.x / BishojyoEditorData.percentX);

            for (int i = 0; i < BishojyoEditorData.SliderCount; i++)
            {
                GUILayout.BeginHorizontal();
                if (GUI.Button(
                        new Rect(
                            panelPos.x * i - BishojyoEditorData.SliderValue,
                            panelPos.y,
                            panelSize.x,
                            panelSize.y),
                        i.ToString()))
                {
                    BishojyoEditorData.activePanelIndex = i;
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}