using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BishojyoObject
{
    public string type;
    public Vector2 position;
    public Vector2 scale = Vector3.one;
    public Texture2D image;
    public bool active = true;
    public UnityEvent enableEvent;
    public UnityEvent disableEvent;
    
}
