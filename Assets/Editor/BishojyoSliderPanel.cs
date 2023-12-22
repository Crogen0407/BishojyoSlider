using System;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Editor
{
    public class BishojyoSliderPanel : EditorWindow
    {
        private Rect windowRect;
        private Vector2 _screenSize = new Vector2(1920, 1080);
        private EditorWindow _window;
        [MenuItem("BishojyoSlider/BishojyoSliderPanel")]
        private static void ShowWindow()
        {
            var window = GetWindow<BishojyoSliderPanel>();
            window.titleContent = new GUIContent("BishojyoSlider");
            window.minSize = new Vector2(960, 540);
            window.Show();
        }
        
        float _sliderValue = 0;
        private float _sliderCount = 10;
        private float _mulSize = 0.7f;

        private float _currentActivePanelIndex;
        
        private Vector2 scrollPos = Vector2.zero;
        float currentScrollViewHeight;
        bool resize = false;
        Rect cursorChangeRect;
        
        private void OnGUI()
        {
            BishojyoEditorData.Init();
            float gap = 25;
            float mm = 50;
            float aa = position.width > position.height ? position.height - mm  : position.width - mm;
            Vector2 windowSize = new Vector2(position.width, position.height);

            #region MainScreen
            
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
                
            #endregion
            
            _currentActivePanelIndex = BishojyoEditorData.activePanelIndex;

            #region UnderToolbar
            
                GUILayout.BeginArea(new Rect(new Vector2(0,position.height / 2), new Vector2(position.width, position.height)));
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
                GUILayout.EndArea();
            
            #endregion
            Repaint();
        }
	
       
    }
}