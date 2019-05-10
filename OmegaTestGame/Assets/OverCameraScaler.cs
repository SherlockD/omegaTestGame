using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class OverCameraScaler : MonoBehaviour
{
    [SerializeField] private RectTransform _overCameraObject;

    private void Awake()
    {
        //_overCameraObject.offsetMin = new Vector2(715,0);
        _overCameraObject.offsetMin = new Vector2(Camera.main.scaledPixelWidth,0);
        _overCameraObject.offsetMax = new Vector2(Camera.main.scaledPixelWidth,0);
    }
}
