using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BishojyoSlider
{
    [CreateAssetMenu(menuName = "BishojyoSlider/ProjectData", fileName = "ProjectData")]
    public class BishojyoEditorData : ScriptableObject
    {
        public string projectCallType;
        public int sliderCount;
        public float sliderValue;
        public Vector2 screenSize = new Vector2(1920, 1080);
        public Rect windowRect; 
    
        public float gap = 2;
        public float percentY;
        public float percentX;
        public int activePanelIndex;

        public List<BishojyoSceneData> sceneInfomation;
    
        [ContextMenu("Init")]
        public void Init()
        {
            sceneInfomation = new List<BishojyoSceneData>();
            windowRect = new Rect(new Vector2(0, 0), screenSize);
            percentY = screenSize.y / screenSize.x;
            percentX = screenSize.x / screenSize.y;
        }
    }

    [Serializable]
    public struct BishojyoSceneData
    {
        public List<BishojyoTransform> list;
    }
}