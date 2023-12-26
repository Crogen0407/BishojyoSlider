using System;
using TMPro;
using UnityEngine;

namespace BishojyoSlider
{
    [Serializable]
    public class BishojyoText : BishojyoTransform
    {
        public TMP_FontAsset font;
        public bool bold;
        public Color color;
        public TextAlignmentOptions alignment;
        public string text;
    }
}