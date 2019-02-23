using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleManager : MonoBehaviour
{
    public static AssetBundleManager Instance;

    [SerializeField] private ErrorPanel _errorPanel;

    private string _dataPath = null;

    private List<string> _downloadedAssets  = new List<string>();
    
    private void Awake()
    {
#if UNITY_EDITOR
     _dataPath = Application.dataPath;
#else
     _dataPath = Application.persistentDataPath;
#endif 
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else if(Instance != this)
        {
            Destroy(gameObject);
        }      
    }

    public void LoadBundle(string pathesSaveUrl,Action<AssetBundle> response,LoadingScreen loadingScreen)
    {
        StartCoroutine(LoadBundleFromServer(pathesSaveUrl,response,loadingScreen));
    }
    
    IEnumerator LoadBundleFromServer(string pathesSaveUrl,Action<AssetBundle> response, LoadingScreen loadingScreen)
    {
        Hash128 hash;
        
        while (!Caching.ready)
        {
            yield return null;
        }

        if (_downloadedAssets.Contains(pathesSaveUrl))
        {
            loadingScreen.StatusText.text = "Data already download, load from cache";
            yield break;
        }
                       
        var request = UnityWebRequest.Get(pathesSaveUrl);
                
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            AssetBundleSave manifestData = null;
            if (File.Exists(_dataPath + "/" +AssetBundleSave.fileName))
            {
                _downloadedAssets.Add(pathesSaveUrl);
                
                manifestData = JsonSerializator.ReadSerialize<AssetBundleSave>(AssetBundleSave.fileName,
                    _dataPath);
            
                loadingScreen.StatusText.text = "Internet connection failed, load last downloaded data";
           
                hash = Hash128.Parse(manifestData?.AssetBundleHash);
                
                request = UnityWebRequestAssetBundle.GetAssetBundle(JsonSerializator.ReadSerialize<PathesSave>("pathesSave.json",Application.persistentDataPath).AssetBundleUrl, hash, 0);
                
                yield return request.SendWebRequest();

                response(DownloadHandlerAssetBundle.GetContent(request));

                yield break;
            }
            else
            {
                Debug.Log("Error. Check internet connection!");    
                var errorPanel = Instantiate(_errorPanel, loadingScreen.transform);
                errorPanel.CreatePanel(delegate { SceneManager.Instance.StopSceneLoad(); SceneManager.Instance.LoadScene("MainMenu",loadingScreen,null);},"Error. First run requires internet connection", "Main menu" );
                yield break;
            }
        }
        
        _downloadedAssets.Add(pathesSaveUrl);
        
        loadingScreen.StatusText.text = "Loading data from server...";               
        
        JsonSerializator.SerializeClass(request.downloadHandler.text,"pathesSave.json",Application.persistentDataPath);

        var pathesSave = JsonUtility.FromJson<PathesSave>(request.downloadHandler.text);
        
        request = UnityWebRequest.Get(pathesSave.AnchorsSaveJsonUrl);
        
        yield return request.SendWebRequest();
        
        JsonSerializator.SerializeClass(request.downloadHandler.text,"anchorsSave.json",Application.persistentDataPath);
        Debug.Log(Application.persistentDataPath);

        request = UnityWebRequest.Get(pathesSave.AssetBundleUrl + ".manifest");
        
        yield return request.SendWebRequest();

        var hashRow = request.downloadHandler.text.Split("\n".ToCharArray())[5];
            
            hash = Hash128.Parse(hashRow.Split(':')[1].Trim());


        if (hash.isValid)
        {
            JsonSerializator.SerializeClass(new AssetBundleSave(hash), AssetBundleSave.fileName, _dataPath);

            request.Dispose();

            request = UnityWebRequestAssetBundle.GetAssetBundle(pathesSave.AssetBundleUrl, hash, 0);


            yield return request.SendWebRequest();

            if (!request.isHttpError && !request.isNetworkError)
            {
                response(DownloadHandlerAssetBundle.GetContent(request));
            }
            else
            {
                response(null);
            }
        }
        else
        {
            response(null);
        }

        request.Dispose();
    }
    
}
