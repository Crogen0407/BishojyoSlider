using UnityEditor;
using UnityEngine;

public static class BishojyoEditorData
{
    public static int SliderCount { get; private set; }
    public static float SliderValue { get; set; }
    public static Vector2 screenSize = new Vector2(1920, 1080);
    public static Rect windowRect = new Rect(new Vector2(0, 0), screenSize);
    
    public static float gap = 2;
    public static float percentY = screenSize.y / screenSize.x;
    public static float percentX = screenSize.x / screenSize.y;
    public static int activePanelIndex;
    
    public static void Init()
    {
        SliderCount = 10;
        screenSize = new Vector2(1920, 1080);
        windowRect = new Rect(new Vector2(0, 0), screenSize);
        gap = 2;                               
        percentY = screenSize.y / screenSize.x;
        percentX = screenSize.x / screenSize.y;
    }
}
