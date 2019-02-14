using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleManager : MonoBehaviour
{
    public event Action<AssetBundle> response;
    
    private void Awake()
    {
        response += GetContent;
        StartCoroutine(LoadBundleFromServer("https://s3.amazonaws.com/omegatestgame/gamedata",response));
    }

    private void GetContent(AssetBundle bundle)
    {
        var gameObjects = bundle.LoadAllAssets(typeof(GameObject));

        foreach (var gameObject in gameObjects)
        {
            Debug.Log(gameObject.name);
        }
    }
    
    IEnumerator LoadBundleFromServer(string url,Action<AssetBundle> response)
    {
        while (!Caching.ready)
        {
            yield return null;
        }

        var request = UnityWebRequest.Get(url + ".manifest");

        yield return request.SendWebRequest();

        if (!request.isHttpError && !request.isNetworkError)
        {
            Hash128 hash;
            
            var hashRow = request.downloadHandler.text.Split("\n".ToCharArray())[5];
            
            hash = Hash128.Parse(hashRow.Split(':')[1].Trim());
            
            Debug.Log(hash);

            if (hash.isValid)
            {
                request.Dispose();

                request = UnityWebRequestAssetBundle.GetAssetBundle(url, hash, 0);

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
        }
        else
        {
            response(null);
        }
        
        request.Dispose();
    }
    
}
