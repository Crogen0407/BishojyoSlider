using UnityEditor;
using UnityEngine;

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

    [ContextMenu("Init")]
    public  void Init()
    {
        windowRect = new Rect(new Vector2(0, 0), screenSize);
        percentY = screenSize.y / screenSize.x;
        percentX = screenSize.x / screenSize.y;
    }
}
