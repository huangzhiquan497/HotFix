using XLua;

namespace XLuaFramework
{
    [LuaCallCSharp]
    public static class LongExtension
    {
        public static string DateTimeTextMaxWithHour(long duration, bool fullDisplay = true)
        {
            if (duration < 0)
                return "";
            var hours = ((duration) / 3600);
            var minutes = ((duration - hours * 3600) / 60);
            var seconds = (duration - hours * 3600 - minutes * 60);
            var hoursDisplay = (hours >= 10) ? hours.ToString() : "0" + hours.ToString();
            var minutesDisplay = (minutes >= 10) ? minutes.ToString() : "0" + minutes.ToString();
            var secondsDisplay = (seconds >= 10) ? seconds.ToString() : "0" + seconds.ToString();

            if (fullDisplay)
            {
                return hoursDisplay + ":" + minutesDisplay + ":" + secondsDisplay;
            }

            return hours > 2 ? hoursDisplay + ":" + minutesDisplay : minutesDisplay + ":" + secondsDisplay;
        }
    }
}