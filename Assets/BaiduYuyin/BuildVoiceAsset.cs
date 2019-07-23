using System.Collections;
using UnityEditor;
using UnityEngine;

namespace MyTest
{
    public class BuildVoiceAsset : Editor
    {
        [MenuItem("Tool/Build Voice Scriptable Asset")]
        public static void ExecuteBuild()
        {
            VoiceMenu holder = CreateInstance<VoiceMenu>();

            //查询excel表中数据，赋值给asset文件
            var reader = new ExcelReader();
            holder.menus = reader.SelectMenuTable();

            string path = "Assets/BaiduYuyin/Resources/voice.asset";

            AssetDatabase.CreateAsset(holder, path);
            AssetDatabase.Refresh();

            Debug.Log("BuildAsset Success!");
        }

        [MenuItem("Tool/GenerateVoiceInitToken")]
        public static void GenerateVoiceInitToken()
        {
            var go = new GameObject().AddComponent<Fake>();

            go.StartCoroutine(VoiceGenerate.InitToken());
        }


        [MenuItem("Tool/GenerateVoice")]
        public static void GenerateVoice()
        {
            var asset = Resources.Load<VoiceMenu>("voice");
            foreach (var gd in asset.menus)
            {
                Debug.Log(gd.Name);

                var go = new GameObject().AddComponent<Fake>();

                go.StartCoroutine(VoiceGenerate.Generate($"{gd.Name}_sc", gd.Chinese, null));
                go.StartCoroutine(VoiceGenerate.Generate($"{gd.Name}_en", gd.English, null));
            }
        }
    }
}