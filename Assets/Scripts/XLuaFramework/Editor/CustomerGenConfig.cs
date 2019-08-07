using System;
using System.Collections;
using System.Collections.Generic;
using XLua;

namespace XLuaFramework
{
    public static class CustomerGenConfig
    {
        [CSharpCallLua] 
        public static List<Type> CSharpCallLua = new List<Type>()
        {
            typeof(Action),
            typeof(Func<double, double, double>),
            typeof(Action<string>),
            typeof(Action<double>),
            typeof(Action<long>),
            typeof(UnityEngine.Events.UnityAction),
            typeof(System.Collections.IEnumerator),
        };

        [LuaCallCSharp] 
        public static List<Type> LuaCallCSharp = new List<Type>()
        {
        };
    }
}