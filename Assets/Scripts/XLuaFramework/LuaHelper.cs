using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace XLuaFramework
{
    [LuaCallCSharp]
    public static class LuaHelper
    {
        public static void Test()
        {
            Debug.Log("test lua call cs");
//            Object.Instantiate(Resources.Load("UITest"));
        }
    }
}