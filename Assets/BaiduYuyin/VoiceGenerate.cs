using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace MyTest
{
    public class VoiceGenerate
    {
        public string APIKey = "yq4CGgAIB18r6gvzSAE3CfwS";
        public string SecretKey = "mMWL3CgTFzt07v4ouZMqxgbwXWqusoGh";
        public string per = "111";
        public string pit = "5";
        public string spd = "5";
        private string Token;

        [Serializable]
        public class TtsResponse
        {
            public int err_no;
            public string err_msg;
            public string sn;
            public int idx;

            public bool Success
            {
                get { return err_no == 0; }
            }

            public AudioClip clip;
        }

        // 用于解析返回的json
        [Serializable]
        class TokenResponse
        {
            public string access_token = null;
        }


        public IEnumerator InitToken()
        {
            // 拼接请求的URL
            var uri =
                $"https://openapi.baidu.com/oauth/2.0/token?grant_type=client_credentials&client_id={APIKey}&client_secret={SecretKey}";
            var www = UnityWebRequest.Get(uri);
            yield return www.SendWebRequest();

            if (www.isHttpError || www.isNetworkError)
            {
                Debug.LogError("[BaiduAip]" + www.error);
                Debug.LogError("[BaiduAip]Token was fetched failed. Please check your APIKey and SecretKey");
            }
            else
            {
                Debug.Log("[BaiduAip]" + www.downloadHandler.text);
                var result = JsonUtility.FromJson<TokenResponse>(www.downloadHandler.text);
                Token = result.access_token;
                Debug.LogWarning(Token);
                Debug.Log("[WitBaiduAip]Token has been fetched successfully");
            }
        }

        public IEnumerator Generate(string name, string text, Action<TtsResponse> callback)
        {
            var url = "http://tsn.baidu.com/text2audio";

            var param = new Dictionary<string, string>
            {
                {"tex", text},
                {"tok", Token},
                {"cuid", SystemInfo.deviceUniqueIdentifier},
                {"ctp", "1"},
                {"lan", "zh"},
                {"spd", spd},
                {"pit", pit},
                {"vol", "10"},
                {"per", per},
                {"aue", "3"}
            };

            var i = 0;
            foreach (var p in param)
            {
                url += i != 0 ? "&" : "?";
                url += p.Key + "=" + p.Value;
                i++;
            }

            var www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG);

            Debug.Log("[WitBaiduAip]" + www.url);
            yield return www.SendWebRequest();

            if (www.isHttpError || www.isNetworkError)
                Debug.LogError(www.error);
            else
            {
                var type = www.GetResponseHeader("Content-Type");

                Debug.Log("[WitBaiduAip]response type: " + type);

                if (type.Contains("audio"))
                {
                    File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), $"{name}.mp3"),
                        www.downloadHandler.data);

                    var response = new TtsResponse {clip = DownloadHandlerAudioClip.GetContent(www)};
                    callback?.Invoke(response);
                }
                else
                {
                    var textBytes = www.downloadHandler.data;
                    var errorText = Encoding.UTF8.GetString(textBytes);
                    Debug.LogError("[WitBaiduAip]" + errorText);
                    callback?.Invoke(JsonUtility.FromJson<TtsResponse>(errorText));
                }
            }
        }
    }
}