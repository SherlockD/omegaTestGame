using UnityEditor;

namespace Scripts.Editor
{
    public class BuildAssetBoundle
    {
        [MenuItem("Assets/BuildBundle")]
        private static void BuildBundle()
        {
            var path = EditorUtility.SaveFolderPanel("Save Bundle", "", "");
            if (path.Length != 0)
                BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.Android);
        }
    }
}