using UnityEngine;

namespace Scripts.Save
{
    public class AssetBundleSave
    {
        public static string fileName = "assetBundleSave.JSON";

        public string AssetBundleHash;

        public AssetBundleSave(Hash128 hash)
        {
            AssetBundleHash = hash.ToString();
        }
    }
}