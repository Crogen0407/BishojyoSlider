﻿using System.Collections.Generic;
using UnityEngine;

namespace BishojyoSlider
{
    public class BishojyoTransform
    {
        public string type;
        public bool active = true;
        public List<BishojyoTransform> children;
        public Vector2 position;
        public Vector2 scale = Vector3.one;
    }
}