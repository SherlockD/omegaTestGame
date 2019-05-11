using UnityEngine;

namespace Scripts
{
    public class OverCameraScaler : MonoBehaviour
    {
        [SerializeField] private RectTransform _overCameraObject;

        private void Awake()
        {
            _overCameraObject.offsetMin = new Vector2(Camera.main.scaledPixelWidth, 0);
            _overCameraObject.offsetMax = new Vector2(Camera.main.scaledPixelWidth, 0);
        }
    }
}