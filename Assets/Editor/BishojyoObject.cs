using System;
using UnityEngine;
using UnityEngine.Events;

namespace BishojyoSlider
{
    [Serializable]
    public class BishojyoObject : BishojyoTransform
    {
        public Texture2D image;
        public bool active = true;
    }
}

