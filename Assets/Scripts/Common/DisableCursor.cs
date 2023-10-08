using UnityEngine;

namespace Game.Common
{
    public class DisableCursor : MonoBehaviour
    {
        private void Awake()
        {
            Cursor.visible = false;
        }
    }
}