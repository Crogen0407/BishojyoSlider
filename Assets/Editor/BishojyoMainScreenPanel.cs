using System;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Editor
{
    public class BishojyoMainScreenPanel : EditorWindow
    {
        private Rect windowRect;
        private Vector2 _screenSize = new Vector2(1920, 1080);
        private EditorWindow _window;
        [MenuItem("BishojyoSlider/OpenSliderPanel")]
        private static void ShowWindow()
        {
            var window = GetWindow<BishojyoMainScreenPanel>();
            window.titleContent = new GUIContent("BishojyoSlider");
            window.minSize = new Vector2(200, 200);
            window.Show();
        }
        
        float _sliderValue = 0;
        private float _sliderCount = 10;
        private float _mulSize = 0.7f;

        private float _currentActivePanelIndex;
        private void OnGUI()
        {
            float gap = 25;

            float mm = 50;
            float aa = position.width > position.height ? position.height - mm  : position.width - mm;
            
            windowRect = new Rect(
                new Vector2(
                    (position.width - _screenSize.x) / 2,
                    (position.height - _screenSize.y) / 2), 
                        _screenSize);
            
            GUILayout.BeginArea(windowRect, GUI.skin.window);
            {
                if (_currentActivePanelIndex != BishojyoEditorData.activePanelIndex)
                {
                    
                }
                GUILayout.TextArea(BishojyoEditorData.activePanelIndex.ToString(), GUIStyle.none);
            }
            GUILayout.EndArea();
            
            Event e = Event.current;
            if (e.isScrollWheel)
            {
                if (e.delta.y < 0)
                {
                    _mulSize += 0.2f * Time.deltaTime;
                }
                else
                {
                    _mulSize -= 0.2f * Time.deltaTime;
                }
            }
            _screenSize = new Vector2(aa, aa * BishojyoEditorData.percentY) * 1.5f * _mulSize;
            _mulSize = Mathf.Clamp(_mulSize, 0.2f, 3);
            GUILayout.TextField(_mulSize.ToString(), 3);
            // myString = EditorGUILayout.TextField ("Text Field", myString);
            //
            // groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
            // myBool = EditorGUILayout.Toggle ("Toggle", myBool);
            // myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
            // EditorGUILayout.EndToggleGroup ();
            
            _currentActivePanelIndex = BishojyoEditorData.activePanelIndex;
            Repaint();
        }
        
        void DoMyWindow(int windowID)
        {
            // This button will size to fit the window
            if (GUILayout.Button("Hello World"))
            {
                Debug.Log("Got a click");
            }
        }
    }
}