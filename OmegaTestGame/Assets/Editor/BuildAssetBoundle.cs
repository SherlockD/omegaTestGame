using UnityEditor;


public class BuildAssetBoundle
{
    [MenuItem("Assets/BuildBundle")]
    static void BuildBundle()
    {
        string path = EditorUtility.SaveFolderPanel("Save Bundle", "", "");
        if (path.Length != 0)
        {
            BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.Android);
        }
    }
}
