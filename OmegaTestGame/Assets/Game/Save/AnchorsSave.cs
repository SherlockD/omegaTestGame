using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Anchor
{
    public Anchor(Vector2 minAnchor,Vector2 maxAnchor)
    {
        MinAnchor = maxAnchor;
        MaxAnchor = maxAnchor;
    }
    public Vector2 MinAnchor;
    public Vector2 MaxAnchor;

}

[System.Serializable]
public class AnchorsSave
{
    public List<Anchor> Anchors = new List<Anchor>();
}
