using XLua;

namespace XLuaFramework
{
    [Hotfix]
    public class TestHotfix
    {
        public static int value = 30;

        public static int Add(int a, int b)
        {
            return a - b;
        }

        public int Mul(int a, int b)
        {
            return a / b;
        }
    }
    
    
}