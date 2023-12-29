using System;
using System.Collections;
using System.Collections.Generic;
using BishojyoSlider;
using UnityEngine;

public class BishojyoRunTimeManager : MonoBehaviour
{
    public BishojyoEditorData bishojyoEditorData;
    public Transform BishojyoSliderGroupTransform { get; private set; }

    private int sceneCount;

    public int SceneCount
    {
        get => sceneCount;
        set
        {
            sceneCount = value;
            bishojyoEditorData.activePanelIndex = sceneCount;
        }
    }
    
    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        //Transform
        BishojyoSliderGroupTransform = GameObject.Find("BishojyoSliderGroup").transform;
        if (BishojyoSliderGroupTransform != null)
        {
            BishojyoSliderGroupTransform = new GameObject("BishojyoSliderGroup").transform;
        }
        BishojyoSliderGroupTransform.position = Vector3.zero;
        BishojyoSliderGroupTransform.rotation = Quaternion.identity;

    }
}
