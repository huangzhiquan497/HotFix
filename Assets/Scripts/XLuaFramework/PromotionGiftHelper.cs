using UnityEngine;
using XLua;

namespace XLuaFramework
{
    [LuaCallCSharp]
    public class PromotionGiftHelper
    {
        public static void Purchase(string sku)
        {
            Debug.LogWarning("cs request purchase ======>" + sku);
        }
    }
}