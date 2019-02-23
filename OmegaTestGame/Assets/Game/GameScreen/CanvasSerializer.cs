using UnityEditor;
using UnityEngine;

public class CanvasSerializer : MonoBehaviour
{
    [SerializeField] private RectTransform[] _transforms;

    private void Awake()
    {
        UnSerialize();
    }

    private void Serialize()
    {
        var anchorsSave = new AnchorsSave();
        
        foreach (var rectTransform in _transforms)
        {
           anchorsSave.Anchors.Add(new Anchor(rectTransform.anchorMin,rectTransform.anchorMax));
        }

        JsonSerializator.SerializeClass(anchorsSave,"anchorsSave.json",Application.dataPath);
    }

    private void UnSerialize()
    {
        AnchorsSave anchorsSave =
            JsonSerializator.ReadSerialize<AnchorsSave>("AnchorsSave.json", Application.persistentDataPath);
        for (int i = 0; i < _transforms.Length; i++)
        {
            _transforms[i].anchorMin = anchorsSave.Anchors[i].MinAnchor;
            _transforms[i].anchorMax = anchorsSave.Anchors[i].MaxAnchor;
        }
    }
}
