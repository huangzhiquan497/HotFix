using System;
using UnityEngine;
using XLua;

namespace XLuaFramework
{
    [LuaCallCSharp]
    public class TestLua : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("cs start");
        }

        [CSharpCallLua]
        public delegate void LuaExecute();

        public void ExecuteEvent(LuaExecute events)
        {
            events?.Invoke();
        }
    }
}