using System;
using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace XLuaFramework
{
    [Serializable]
    [LuaCallCSharp]
    public class Injection
    {
        public string name;
        public GameObject value;
    }

    [LuaCallCSharp]
    public class LuaViewBehaviour : MonoBehaviour
    {
        [CSharpCallLua]
        public delegate void delLuaAwake(GameObject obj);

        private delLuaAwake luaAwake;

        [CSharpCallLua]
        public delegate void delLuaStart();

        private delLuaStart luaStart;

        [CSharpCallLua]
        public delegate void delLuaUpdate();

        private delLuaUpdate luaUpdate;

        [CSharpCallLua]
        public delegate void delLuaOnDestroy();

        private delLuaOnDestroy luaOnDestroy;

        public Injection[] injections;
        private LuaTable scriptEnv;
        private LuaEnv luaEnv;

        [CSharpCallLua]
        public class MyClass
        {
            
        }

        private MyClass _myClass;
        private void Awake()
        {
            luaEnv = LuaManager.LuaEnv;
            scriptEnv = luaEnv.NewTable();
            var meta = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global);
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();

            scriptEnv.Set("self", this);
            foreach (var injection in injections)
            {
                scriptEnv.Set(injection.name, injection.value);
            }

            var prefabName = name;
            if (prefabName.Contains("(Clone)"))
            {
                prefabName = prefabName.Split(new[] {"(Clone)"}, StringSplitOptions.RemoveEmptyEntries)[0];
            }

            prefabName = prefabName.Replace("Panel", "");

            luaAwake = scriptEnv.GetInPath<delLuaAwake>(prefabName + ".awake");
            luaStart = scriptEnv.GetInPath<delLuaStart>(prefabName + ".start");
            luaUpdate = scriptEnv.GetInPath<delLuaUpdate>(prefabName + ".update");
            luaOnDestroy = scriptEnv.GetInPath<delLuaOnDestroy>(prefabName + ".ondestroy");

            _myClass = luaEnv.Global.Get<MyClass>("MyClass");
            Debug.Log(_myClass == null);
            luaAwake?.Invoke(gameObject);
        }

        private void Start()
        {
            luaStart?.Invoke();
        }

        private void Update()
        {
            luaUpdate?.Invoke();
        }

        private void OnDestroy()
        {
            luaOnDestroy?.Invoke();

            luaAwake = null;
            luaStart = null;
            luaUpdate = null;
            luaOnDestroy = null;
        }
    }
}