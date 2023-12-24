using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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
        private Vector2 timelineScrollVec;
        private void OnGUI()
        {
            BishojyoEditorData.Init();
            float gap = 25;
            float mm = 50;
            float aa = position.width > position.height ? position.height - mm  : position.width - mm;
            Vector2 windowSize = new Vector2(position.width, position.height);
            Vector2 sideWindowSize = new Vector2(windowSize.x / 5, windowSize.y*0.7f);
            Rect mainScreenRect = new Rect(Vector2.right * sideWindowSize.x,
                new Vector2(windowSize.x - sideWindowSize.x * 2, windowSize.y * 0.7f));
            _currentActivePanelIndex = BishojyoEditorData.activePanelIndex;
            Rect timelineRect = new Rect(new Vector2(0, windowSize.y * 0.7f),
                new Vector2(windowSize.x, windowSize.y - windowSize.y * 0.7f));
            
            GUILayout.BeginArea(new Rect(Vector2.zero, new Vector2(sideWindowSize.x, mainScreenRect.size.y)), GUI.skin.window); //ProjectSetting
            {
                GUILayout.BeginArea(new Rect(Vector2.zero, new Vector2(sideWindowSize.x, 25)), GUI.skin.box);
                {
                    GUI.TextField(new Rect(5, 0, 100, 20), "ProjectSetting", GUI.skin.label);
                }
                GUILayout.EndArea();
            }
            GUILayout.EndArea();
            GUILayout.BeginArea(new Rect(Vector2.right * (windowSize - sideWindowSize), new Vector2(sideWindowSize.x, mainScreenRect.size.y)), GUI.skin.window); //Hierarchy
            {
                
            }
            GUILayout.EndArea();
            GUILayout.BeginArea(mainScreenRect, GUI.skin.window); //MainScreen
            {
                #region MainScreen
            
                windowRect = new Rect(
                    new Vector2(
                        (mainScreenRect.size.x - _screenSize.x) / 2,
                        (mainScreenRect.size.y - _screenSize.y) / 2), 
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
                GUI.TextField(new Rect(5, 0, 50, 20), "Scale", GUI.skin.label);
                GUI.TextField(new Rect(mainScreenRect.size.x * 0.3f + 55, 0, 50, 20), _mulSize.ToString(), 4, GUI.skin.label);

                _mulSize = GUI.HorizontalSlider(new Rect(45, 2.5f, mainScreenRect.size.x * 0.3f, 10), _mulSize, 0.2f, 3);
                _mulSize = Mathf.Clamp(_mulSize, 0.2f, 3);
                #endregion
            }
            GUILayout.EndArea();
            GUILayout.BeginArea(timelineRect, GUI.skin.window); //Timeline
            {
                #region UnderToolbar
            
                Vector2 panelSize = new Vector2(timelineRect.size.y * BishojyoEditorData.percentX, timelineRect.size.y) * 0.7f;
                timelineScrollVec = GUILayout.BeginScrollView(timelineScrollVec, true, false, GUILayout.Width(timelineRect.size.x));
                {
                    GUILayout.BeginHorizontal();

                    for (int i = 0; i < BishojyoEditorData.SliderCount; i++)
                    {
                        if (GUILayout.Button(i.ToString(), GUILayout.Width(panelSize.x), GUILayout.Height(panelSize.y)))
                        {
                            BishojyoEditorData.activePanelIndex = i;
                        }
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndScrollView();

                #endregion
            }
            GUILayout.EndArea();
            
            Repaint();
        }
    }
}