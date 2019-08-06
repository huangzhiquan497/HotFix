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
        public static Dictionary<string, Action<string>> _eventMaps = new Dictionary<string, Action<string>>();


        public static void AddEventListener(string eventType, Action<string> action)
        {
            Debug.LogWarning("add type=====>" + eventType);
            _eventMaps[eventType] = action;
        }

        public static void RemoveEventsListener(string eventType)
        {
            if (!_eventMaps.ContainsKey(eventType))
            {
                Debug.LogWarning("not contains the key===>" + eventType);
                return;
            }

            _eventMaps.Remove(eventType);
        }

        public static void ExecuteEvent(JsonData data)
        {
            for (var i = 0; i < data.Count; i++)
            {
                var type = data[i]["type"].ToString();
                Debug.LogWarning("type=====>" + type);

                if (!_eventMaps.ContainsKey(type)) continue;

                Debug.LogWarning("execute event");
                _eventMaps[type].Invoke(JsonMapper.ToJson(data[i]));
            }
        }
    }
}