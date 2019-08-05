using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using XLua;

namespace XLuaFramework
{
    [LuaCallCSharp]
    public class LuaHelper
    {
        public static Dictionary<string, Action<JsonData>> _eventMaps = new Dictionary<string, Action<JsonData>>();

        public static void AddEventListener(string eventType, Action<JsonData> eventData)
        {
        }

        public static void RemoveEventsListener()
        {
        }
    }
}