using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace XLuaFramework
{
    public class GameManger : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.AddComponent<LuaManager>();
        }

        private void Start()
        {
            LuaManager.Instance.AddLoader(CustomLoader);

            LuaManager.Instance.DoString("require 'Main'"); // 通过自定义 loader 加载
        }

        private byte[] CustomLoader(ref string filepath)
        {
            var path = Path.Combine(Application.dataPath, "XLuaLogic", $"{filepath}.lua.txt"); // 从bundle中下载后保存的沙盒路径
            return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(path));
        }
    }
}