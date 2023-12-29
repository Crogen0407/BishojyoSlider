using System;
using System.Collections;
using System.Collections.Generic;
using BishojyoSlider;
using UnityEngine;

public class BishojyoRunTimeManager : MonoBehaviour
{
    public BishojyoEditorData bishojyoEditorData;
    public Transform BishojyoSliderGroupTransform { get; private set; }
    public Transform[] BishojyoSceneTransforms { get; private set; }
    private int sceneCount;

    public int SceneCount
    {
        get => sceneCount;
        set
        {
            sceneCount = value;
            bishojyoEditorData.activePanelIndex = sceneCount;
            List<GameObject> list = bishojyoEditorData.sceneInfomation[SceneCount].list;
            if (list.Count != 0)
            {
                foreach (var item in list)
                {
                    if (item != null)
                    {
                        Instantiate(item);
                    }
                }
            }
        }
    }
    
    private void OnEnable()
    {
        Init();
        Instantiate(bishojyoEditorData.sceneInfomation[SceneCount].list[0]);
    }

    private void Init()
    {
        //Transform
        try
        {
            BishojyoSliderGroupTransform = GameObject.Find("BishojyoSliderGroup").transform;
        }
        catch (NullReferenceException e)
        {
            if (BishojyoSliderGroupTransform == null)
            {
                BishojyoSliderGroupTransform = new GameObject("BishojyoSliderGroup").transform;
            }
        }
        BishojyoSliderGroupTransform.position = Vector3.zero;
        BishojyoSliderGroupTransform.rotation = Quaternion.identity;
        List<GameObject> list = bishojyoEditorData.sceneInfomation[SceneCount].list;
        for (int i = 0; i < list.Count; i++)
        {
            try
            {
                Transform trm = new GameObject($"Take{i}").transform;
                trm.SetParent(BishojyoSliderGroupTransform);
                trm.position = Vector3.zero;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
