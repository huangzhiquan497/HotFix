using XLua;

namespace XLuaFramework
{
    [LuaCallCSharp]
    public class Util
    {
        private static IServerTime _serverTime;
        public static IServerTime ServerTime => _serverTime ?? (_serverTime = new ServerTime());
    }
}