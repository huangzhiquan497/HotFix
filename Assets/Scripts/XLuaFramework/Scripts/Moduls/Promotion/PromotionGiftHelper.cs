using System;
using UnityEngine;
using XLua;

namespace XLuaFramework
{
    [LuaCallCSharp]
    public class PromotionGiftHelper
    {
        public static void Purchase(string sku, Action onSuccess = null)
        {
            Debug.LogWarning("cs request purchase ======>" + sku);
            onSuccess?.Invoke();
        }

        public static string RefreshData;
    }
}