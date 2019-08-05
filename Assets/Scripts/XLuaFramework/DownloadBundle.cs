using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class DownloadBundle : MonoBehaviour
{
    [SerializeField] private Image _progressImg;
    [SerializeField] private Text _progressTxt;
    private UnityWebRequest _request;

    private void Start()
    {
        StartCoroutine(DownloaderBundle("XLuaLogic", () => { SceneManager.LoadScene("Test"); }));
    }


    private void Update()
    {
        if (_request == null || _request.isDone || _request.isHttpError || _request.isNetworkError)
        {
            return;
        }

        _progressImg.fillAmount = _request.downloadProgress;
        _progressTxt.text = _request.downloadProgress.ToString("P");
    }

    IEnumerator DownloaderBundle(string bundleName, Action onSuccess)
    {
        _request = UnityWebRequestAssetBundle.GetAssetBundle(GetEmbeddedBundlePath(bundleName),
            Hash128.Parse(GetBundleMd5(bundleName)));

        yield return _request.SendWebRequest();

        if (_request.isNetworkError || _request.isHttpError || !string.IsNullOrEmpty(_request.error))
        {
            Debug.LogWarning(_request.error);
        }
        else if (_request.isDone)
        {
            var handler = (DownloadHandlerAssetBundle) _request.downloadHandler;
            if (handler == null)
            {
                Debug.Log("handler null");
            }
            else
            {
                var bundle = handler.assetBundle;
                var load = bundle.LoadAllAssetsAsync<TextAsset>();
                yield return new WaitUntil(() => load.isDone);
                var assets = load.allAssets;
                foreach (var asset in assets)
                {
                    Debug.Log(asset.name);
                    
                    var luaTxt = asset as TextAsset;
                    if (luaTxt == null)
                    {
                        Debug.LogWarning("assets is null");
                        continue;
                    }

                    var path = Path.Combine(Application.persistentDataPath, "XLuaLogic");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var stream = File.Create(Path.Combine(path, $"{asset.name}.txt"));
                    stream.Write(luaTxt.bytes, 0, luaTxt.bytes.Length);

                    stream.Dispose();
                }
                bundle.Unload(true);

                onSuccess?.Invoke();
            }
        }
        else
        {
            Debug.LogWarning("fail");
            _request.Dispose();
        }
    }

    private string GetEmbeddedBundlePath(string bundleName)
    {
        var path = Application.streamingAssetsPath + "/AssetBundle/" + bundleName + ".assetBundle";

#if UNITY_IOS
                    path = "file://" + path;
#else
        if (Application.isEditor)
        {
            path = "file://" + path;
        }
#endif

        return path;
    }

    private string GetBundleMd5(string bundleName)
    {
        return GetMd5(Application.streamingAssetsPath + "/AssetBundle/" + bundleName + ".assetBundle");
    }

    private string GetMd5(string fileName)
    {
        var hashMD5 = String.Empty;
        if (File.Exists(fileName))
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var calculator = MD5.Create();
                var buffer = calculator.ComputeHash(fs);
                calculator.Clear();
                var stringBuilder = new StringBuilder();
                for (var i = 0; i < buffer.Length; i++)
                {
                    stringBuilder.Append(buffer[i].ToString("x2"));
                }

                hashMD5 = stringBuilder.ToString();
            }
        }

        return hashMD5;
    }
}