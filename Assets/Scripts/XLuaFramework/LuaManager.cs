using System;
using UnityEngine;
using XLua;

namespace XLuaFramework
{
    public class LuaManager : MonoBehaviour
    {
        public static LuaEnv LuaEnv; //全局只有一个
        public static LuaManager Instance;

        private void Awake()
        {
            Instance = this;
            LuaEnv = new LuaEnv();

            //第三方库
            LuaEnv.AddBuildin("rapidjson", XLua.LuaDLL.Lua.LoadRapidJson);
            LuaEnv.AddBuildin("lpeg", XLua.LuaDLL.Lua.LoadLpeg);
            LuaEnv.AddBuildin("pb", XLua.LuaDLL.Lua.LoadLuaProfobuf);
            LuaEnv.AddBuildin("ffi", XLua.LuaDLL.Lua.LoadFFI);
        }


        public void DoString(string str)
        {
            LuaEnv.DoString(str);
        }

        public void AddLoader(LuaEnv.CustomLoader customLoader)
        {
            LuaEnv.AddLoader(customLoader);
        }

        private void Update()
        {
            LuaEnv?.Tick();
        }

        private void OnDestroy()
        {
//            LuaEnv?.Dispose();
        }
    }
}