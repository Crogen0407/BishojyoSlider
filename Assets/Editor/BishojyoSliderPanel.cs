using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    public class BishojyoSliderPanel : EditorWindow
    {
        private Rect windowRect;
        private Vector2 _screenSize = new Vector2(1920, 1080);
        private EditorWindow _window;
        
        float _sliderValue = 0;
        private float _sliderCount = 10;
        private float _mulSize = 0.2f;
        private float _currentActivePanelIndex;
        private Vector2 scrollPos = Vector2.zero;
        private Vector2 _inspectorScrollVector;
        private float _currentScrollViewHeight;
        private bool resize = false;
        Rect cursorChangeRect;
        private Vector2 timelineScrollVec;

        public List<BishojyoObject> BishojyoObjects;
        public List<BishojyoObject> BishojyoSubSelectedObjects;
        private BishojyoEditorData _currentProjectData;

        public BishojyoObject currentSelectObject;
        private string callName;

        #region ShowPanel

            [MenuItem("BishojyoSlider/BishojyoSliderPanel")]
            private static void ShowWindow()
            {
                var window = GetWindow<BishojyoSliderPanel>();
                window.titleContent = new GUIContent("BishojyoSlider");
                window.minSize = new Vector2(960, 540);
                window.Show();
            }

        #endregion

        #region Init

            private void OnEnable()
            {
                BishojyoObjects = new List<BishojyoObject>();
                BishojyoSubSelectedObjects = new List<BishojyoObject>();
                _mulSize = 0.2f;
            }

        #endregion
        
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

                    GUILayout.BeginArea(new Rect(Vector2.zero, new Vector2(sideWindowSize.x, mainScreenRect.size.y / 2)), GUI.skin.window);
                    {
                        GUILayout.BeginArea(new Rect(Vector2.zero, new Vector2(sideWindowSize.x, 25)), GUI.skin.box);
                        {
                            GUI.TextField(new Rect(5, 0, 100, 20), "ProjectSetting", GUI.skin.label);
                        }
                        GUILayout.EndArea();
                        //현재 작업중...
                        _currentProjectData.projectCallType = EditorGUILayout.DelayedTextField(_currentProjectData.projectCallType);
                    
                    }     
                    GUILayout.EndArea();

                #endregion

                #region Inspector

                    GUILayout.BeginArea(new Rect(new Vector2(0, mainScreenRect.size.y / 2), new Vector2(sideWindowSize.x, mainScreenRect.size.y / 2)), GUI.skin.window);
                    {
                        GUILayout.BeginArea(new Rect(Vector2.zero, new Vector2(sideWindowSize.x, 25)), GUI.skin.box);
                        {
                            GUI.TextField(new Rect(5, 0, 100, 20), "Inspector", GUI.skin.label);
                        }
                        GUILayout.EndArea();
                        _inspectorScrollVector = GUILayout.BeginScrollView(_inspectorScrollVector, false, false);
                        //Object Setting Data
                        if (BishojyoObjects != null && BishojyoObjects.Count != 0)
                        {
                            #region Inspector Option
                    
                                currentSelectObject.active = GUILayout.Toggle(currentSelectObject.active, "Active");
                                currentSelectObject.type = EditorGUILayout.DelayedTextField(currentSelectObject.type);
                                currentSelectObject.position = EditorGUILayout.Vector3Field("Position", currentSelectObject.position);
                                currentSelectObject.scale = EditorGUILayout.Vector3Field("Scale", currentSelectObject.scale);
                                currentSelectObject.image = EditorGUILayout.ObjectField(currentSelectObject.image, typeof(Texture2D)) as Texture2D;
                    
                            #endregion
                    
                            #region MyRegion Hierarchy Control
                    
                                GUILayout.Space(20);
                                int selectObjectIndex = BishojyoObjects.LastIndexOf(currentSelectObject);
                                if(GUILayout.Button("Up"))
                                {
                                    BishojyoObjects[selectObjectIndex] = BishojyoObjects[selectObjectIndex - 1];
                                    BishojyoObjects[selectObjectIndex - 1] = currentSelectObject;
                                }
                                if (GUILayout.Button("Down"))
                                {
                                    BishojyoObjects[selectObjectIndex] = BishojyoObjects[selectObjectIndex + 1];
                                    BishojyoObjects[selectObjectIndex + 1] = currentSelectObject;
                                }
                                GUI.color = Color.yellow;
                                if (GUILayout.Button("CreateChild"))
                                {
                                    
                                }
                                GUILayout.Space(20);
                                GUI.color = Color.red;
                                if (GUILayout.Button("Delete"))
                                {
                                    BishojyoObjects.Remove(currentSelectObject);
                                    currentSelectObject = BishojyoObjects[selectObjectIndex - 1];
                                }
                                GUI.color = Color.white;
                    
                            #endregion
                        }
                        GUILayout.EndScrollView();
                    }     
                    GUILayout.EndArea();

                #endregion

                #region Hierarchy

                    GUILayout.BeginArea(new Rect(Vector2.right * (windowSize - sideWindowSize), new Vector2(sideWindowSize.x, mainScreenRect.size.y)), GUI.skin.window);
                    {
                        GUILayout.BeginArea(new Rect(Vector2.zero, new Vector2(sideWindowSize.x, 25)), GUI.skin.box);
                        {
                            GUILayout.BeginHorizontal();
                            GUI.TextField(new Rect(5, 0, 100, 20), "Hierarchy", GUI.skin.label);
                            
                            //새로운 객체 추가
                            bool add = GUI.Button(new Rect(new Vector2(sideWindowSize.x - 50, 0), new Vector2(50, 20)), "Add"); 
                            if (add) //새로운 오브젝트를 추가할 때 해야 하는 것
                            {
                                BishojyoObject obj = new BishojyoObject();
                                BishojyoObjects.Add(obj);
                                obj.type = "New Object";
                                currentSelectObject = obj;
                            }
                            GUILayout.EndHorizontal();
                        }
                        GUILayout.EndArea();
                        if (BishojyoObjects != null)
                        {
                            for (int i = 0; i < BishojyoObjects.Count; i++)
                            {
                                //active 활성화 및 비활성화(비활성화된 오브젝트는 다음 현재씬과 다음에 나오는 모든 씬에서 삭제가 됨)
                                if (!BishojyoObjects[i].active) GUI.color = Color.gray;
                                
                                //현재 선택된 오브젝트를 시각적으로 보여주기
                                else if (BishojyoObjects[i] == currentSelectObject) GUI.color = Color.green;
                                
                                //다 아니면 그냥 보여주기
                                else GUI.color = Color.white;
                                
                                if (GUILayout.Button(BishojyoObjects[i].type))
                                {
                                    Event e = Event.current;
                                    if (e.control)
                                    {
                                        //부속으로 선택된 오브젝트
                                        BishojyoSubSelectedObjects.Add(BishojyoObjects[i]);
                                    }
                                    else
                                    {
                                        BishojyoSubSelectedObjects.Clear();
                                        currentSelectObject = BishojyoObjects[i];
                                    }
                                }
                                GUI.color = Color.white;
                            }
                        }
                        
                    }
                    GUILayout.EndArea();

                #endregion

                #region MainScreen

                    GUILayout.BeginArea(mainScreenRect, GUI.skin.window);
                    {
                        #region MainScreen
                        _screenSize = new Vector2(widthSetting, widthSetting * _currentProjectData.percentY) * 1.5f * _mulSize;
                    
                        windowRect = new Rect(
                            new Vector2(
                                (mainScreenRect.size.x - _screenSize.x) / 2,
                                (mainScreenRect.size.y - _screenSize.y) / 2), 
                            _screenSize);
                        GUILayout.BeginArea(windowRect, GUI.skin.window);
                        {
                            //Hierarchy창에 있는 오브젝트 랜더링
                            if (BishojyoObjects != null)
                            {
                                for (int i = BishojyoObjects.Count - 1; i >= 0 ; i--) 
                                {
                                    BishojyoObject obj = BishojyoObjects[i];
                                    Vector2 scale = obj.scale * 250 * _mulSize;
                                    GUI.Button(new Rect((windowRect.size / 2 - new Vector2(-obj.position.x, obj.position.y) * _mulSize - scale / 2),scale), obj.image as Texture, GUI.skin.window);
                                }
                                if (_currentActivePanelIndex != _currentProjectData.activePanelIndex)
                                {
                                }
                            }
                            
                            //GUILayout.TextArea(_currentData.activePanelIndex.ToString(), GUIStyle.none);
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
                        GUI.TextField(new Rect(5, 0, 50, 20), "Scale", GUI.skin.label);
                        GUI.TextField(new Rect(mainScreenRect.size.x * 0.3f + 55, 0, 50, 20), _mulSize.ToString(), 4, GUI.skin.label);
                    
                        _mulSize = GUI.HorizontalSlider(new Rect(45, 2.5f, mainScreenRect.size.x * 0.3f, 10), _mulSize, 0.2f, 3);
                        _mulSize = Mathf.Clamp(_mulSize, 0.2f, 3);
                        #endregion
                    }
                    GUILayout.EndArea();

                #endregion

                #region Timeline

                    GUILayout.BeginArea(timelineRect, GUI.skin.window);
                    {
                        #region UnderToolbar
                    
                        Vector2 panelSize = new Vector2(timelineRect.size.y * _currentProjectData.percentX, timelineRect.size.y) * 0.7f;
                        timelineScrollVec = GUILayout.BeginScrollView(timelineScrollVec, false, false, GUILayout.Width(timelineRect.size.x));
                        {
                            GUILayout.BeginHorizontal();
                    
                            for (int i = 0; i < _currentProjectData.sliderCount; i++)
                            {
                                if (GUILayout.Button(i.ToString(), GUILayout.Width(panelSize.x), GUILayout.Height(panelSize.y)))
                                {
                                    _currentProjectData.activePanelIndex = i;
                                }
                            }
                            GUILayout.EndHorizontal();
                        }
                        GUILayout.EndScrollView();
                    
                        #endregion
                    }
                    GUILayout.EndArea();

                #endregion
                
                //창 초기화
                Repaint();
            }
           
        }
    }
}