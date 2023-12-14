using System;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

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
        private void OnGUI()
        {
            float gap = 25;

            float mm = 50;
            float aa = position.width > position.height ? position.height - mm  : position.width - mm;
            _screenSize = new Vector2(aa, aa * BishojyoEditorData.percentY) * 1.5f * _mulSize;
            
            windowRect = new Rect(
                new Vector2(
                    (position.width - _screenSize.x) / 2,
                    (position.height - _screenSize.y) / 2), 
                        _screenSize);
            
            GUILayout.BeginArea(windowRect, GUI.skin.window);
            {
                
            }
            GUILayout.EndArea();
            
            _mulSize = GUI.VerticalSlider(new Rect(Vector2.zero, new Vector2(1, position.height)), _mulSize, 3, 0.2f);
            _mulSize += Input.GetAxis("Mouse ScrollWheel");
            Event e = Event.current;
            if (e.isScrollWheel)
            {
                _mulSize += 0.1f;
            }
            GUILayout.TextField(_mulSize.ToString(), 3);
            // myString = EditorGUILayout.TextField ("Text Field", myString);
            //
            // groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
            // myBool = EditorGUILayout.Toggle ("Toggle", myBool);
            // myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
            // EditorGUILayout.EndToggleGroup ();

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