using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace MyTest
{
    public class BuildVoiceAsset : Editor
    {
        [MenuItem("Tool/Build Voice Scriptable Asset")]
        public static void ExecuteBuild()
        {
            var holder = CreateInstance<VoiceMenu>();

            //查询excel表中数据，赋值给asset文件
            var reader = new ExcelReader();
            holder.menus = reader.SelectMenuTable(Path.Combine(Directory.GetCurrentDirectory(), "Voice.xls"), "sheet1");

            string path = "Assets/BaiduYuyin/Resources/voice.asset";

            AssetDatabase.CreateAsset(holder, path);
            AssetDatabase.Refresh();

            Debug.Log("BuildAsset Success!");
        }


        [MenuItem("Tool/GenerateVoice")]
        public static void GenerateVoice()
        {
            var go = new GameObject().AddComponent<Fake>();

            go.StartCoroutine(DoGenerateVoice());
        }

        private static IEnumerator DoGenerateVoice()
        {
            var generator = new VoiceGenerate();
            yield return generator.InitToken();

            var asset = Resources.Load<VoiceMenu>("voice");
            foreach (var gd in asset.menus)
            {
                Debug.Log(gd.Name);
                yield return generator.Generate($"{gd.Name}_sc", gd.Chinese, null);
                yield return generator.Generate($"{gd.Name}_en", gd.English, null);
            }
        }
    }
}