using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace BishojyoSlider
{
    public class BishojyoSliderPanel : EditorWindow
    {
        private Rect windowRect;
        private Vector2 _screenSize = new Vector2(1920, 1080);
        private EditorWindow _window;
        
        float _sliderValue = 0;
        private int _currentActivePanelIndex;
        private Vector2 _inspectorScrollVector;
        private float _currentScrollViewHeight;
        private bool resize = false;
        Rect cursorChangeRect;
        private Vector2 timelineScrollVec;

        public List<GameObject> bishojyoGameObjects;
        public List<GameObject> bishojyoSubSelectedGameObjects;
        private BishojyoEditorData _currentProjectData;

        private string callName;
        
        //ProjectSetting
        public int _activeObjectCount;

        //SceneSetting
        
        #region ShowPanel

            [MenuItem("BishojyoSlider/BishojyoSliderPanel")]
            private static void ShowWindow()
            {
                var window = GetWindow<BishojyoSliderPanel>();
                window.titleContent = new GUIContent("BishojyoSlider");
                window.minSize = new Vector2(960/2f, 540/2f);
                window.Show();
            }

        #endregion

        #region Init

            private void OnEnable()
            {
                //Transforms
                bishojyoGameObjects = new List<GameObject>();
                bishojyoSubSelectedGameObjects = new List<GameObject>();
            }

        #endregion

        private GameObject objectKK;
        private void OnGUI()
        {
            //선택된 에디터 객체
            object selectionObject = Selection.activeObject;
            if(selectionObject != null && selectionObject.GetType() == (typeof(BishojyoEditorData)))
            {
                _currentProjectData = selectionObject as BishojyoEditorData;
            }

            if (_currentProjectData != null)
            {
                float gap = 25;
                float widthSetting = 1920;
                Vector2 windowSize = new Vector2(position.width, position.height);
                Vector2 sideWindowSize = new Vector2(windowSize.x / 5, windowSize.y*0.7f);
                Rect mainScreenRect = new Rect(Vector2.right * sideWindowSize.x,
                    new Vector2(windowSize.x - sideWindowSize.x * 2, windowSize.y * 0.7f));
                _currentActivePanelIndex = _currentProjectData.activePanelIndex;
                Rect timelineRect = new Rect(new Vector2(0, windowSize.y * 0.7f),
                    new Vector2(windowSize.x, windowSize.y - windowSize.y * 0.7f));
                
                #region ProjectSetting

                    GUILayout.BeginArea(new Rect(Vector2.zero, new Vector2(sideWindowSize.x, mainScreenRect.size.y)), GUI.skin.window);
                    {
                        GUILayout.BeginArea(new Rect(Vector2.zero, new Vector2(sideWindowSize.x, 25)), GUI.skin.box);
                        {
                            GUI.TextField(new Rect(5, 0, 100, 20), "ProjectSetting", GUI.skin.label);
                            GUILayout.Space(10);
                        }
                        GUILayout.EndArea();
                        //현재 작업중...
                        _currentProjectData.projectCallType = EditorGUILayout.DelayedTextField(_currentProjectData.projectCallType);
                        GUILayout.Space(10);
                        _currentProjectData.sliderCount = int.Parse(EditorGUILayout.DelayedTextField(_currentProjectData.sliderCount.ToString()));
                        GUILayout.Space(20);
                        GUI.color = Color.red;
                        if (GUILayout.Button("SceneClear"))
                        {
                            for (int i = 0; i < _currentProjectData.sliderCount; i++)
                            {
                                _currentProjectData.sceneInfomation[i].list.Clear();
                            }
                        }
                        GUI.color = Color.white;
                    }     
                    GUILayout.EndArea();

                #endregion

                #region SceneSetting

                    GUILayout.BeginArea(new Rect(new Vector2(sideWindowSize.x, 0), new Vector2(sideWindowSize.x, mainScreenRect.size.y)), GUI.skin.window);
                    {
                        GUILayout.BeginArea(new Rect(Vector2.zero, new Vector2(sideWindowSize.x, 25)), GUI.skin.box);
                        {
                            GUI.TextField(new Rect(5, 0, 100, 20), "SceneSetting", GUI.skin.label);
                        }
                        GUILayout.EndArea();
                        try
                        {
                            _activeObjectCount = _currentProjectData.sceneInfomation[_currentActivePanelIndex].list.Count;
                            _activeObjectCount = int.Parse(EditorGUILayout.DelayedTextField(_activeObjectCount.ToString()));
                            while (_currentProjectData.sceneInfomation[_currentActivePanelIndex].list.Count < _activeObjectCount)
                            {
                                _currentProjectData.sceneInfomation[_currentActivePanelIndex].list.Add(null);
                            }
                            while (_currentProjectData.sceneInfomation[_currentActivePanelIndex].list.Count > _activeObjectCount)
                            {
                                _currentProjectData.sceneInfomation[_currentActivePanelIndex].list.RemoveAt(_currentProjectData.sceneInfomation[_currentActivePanelIndex].list.Count - 1);
                            }
                            
                            for (int i = 0; i < _activeObjectCount; i++)
                            {
                                _currentProjectData.sceneInfomation[_currentActivePanelIndex].list[i] = EditorGUILayout.ObjectField(_currentProjectData.sceneInfomation[_currentActivePanelIndex].list[i], typeof(GameObject), true) as GameObject;
                            }
                        }
                        catch
                        {
                            
                        }

                        
                    }     
                    GUILayout.EndArea();

                #endregion

                #region Timeline

                    GUILayout.BeginArea(timelineRect, GUI.skin.window);
                    {
                        Vector2 panelSize = new Vector2(timelineRect.size.y * _currentProjectData.percentX, timelineRect.size.y) * 0.7f;
                        timelineScrollVec = GUILayout.BeginScrollView(timelineScrollVec, false, false, GUILayout.Width(timelineRect.size.x));
                        {
                            GUILayout.BeginHorizontal();

                            for (int i = 0; i < _currentProjectData.sliderCount; i++)
                            {
                                if (_currentProjectData.sceneInfomation.Count <= _currentProjectData.sliderCount)
                                {
                                    _currentProjectData.sceneInfomation.Add(new BishojyoSceneData());
                                }
                                else
                                {
                                    _currentProjectData.sceneInfomation.RemoveAt(_currentProjectData.sceneInfomation.Count-1);
                                }
                            }
                            
                            for (int i = 0; i < _currentProjectData.sliderCount; i++)
                            {
                                
                                if (GUILayout.Button((i + 1).ToString(), GUILayout.Width(panelSize.x), GUILayout.Height(panelSize.y)))
                                {
                                    _currentProjectData.activePanelIndex = i;
                                    if (_currentProjectData.sceneInfomation[i].list != null)
                                    {
                                        bishojyoGameObjects = _currentProjectData.sceneInfomation[i].list;
                                    }
                                }
                               
                            }
                            GUILayout.EndHorizontal();
                        }
                        GUILayout.EndScrollView();
                    }
                    GUILayout.EndArea();

                #endregion
                
                //창 초기화
                Repaint();
            }
        }
    }
}