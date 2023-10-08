using UnityEditor;
using UnityEngine;

namespace Game.Messages
{
    public class POIActivated
    {
        public bool isFinal;

        public POIActivated(bool isFinal)
        {
            this.isFinal = isFinal;
        }
    }
}