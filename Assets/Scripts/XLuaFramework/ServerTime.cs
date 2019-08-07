using System;
using UniRx;
using UnityEngine;

namespace XLuaFramework
{
    public interface IServerTime
    {
        long Now { get; }

        IObservable<long> ObservableNow { get; }


        long Left(long endTime);
        IObservable<long> ObserveCountDown(long endTime); //一次性倒计时 时间到触发OnCompleted

        IObservable<long> ObserveCountDown(long endTime, Action<long> onNext, Action onComplete, GameObject gameObject);
        void Set(long time);
    }


    public class ServerTime : IServerTime
    {
        private long _lastSetServerTime;
        private float _lastSetServerTimeRealTime;

        public long Now => _lastSetServerTime + (long) (Time.realtimeSinceStartup - _lastSetServerTimeRealTime);

        public IObservable<long> ObservableNow { get; }

        public ServerTime()
        {
            var delta = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var deltaSeconds = delta.TotalSeconds;
            Set(Convert.ToInt64(deltaSeconds));

            ObservableNow = Observable
                .Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1), Scheduler.MainThreadIgnoreTimeScale)
                .Select(_ => Now);
        }

        public long Left(long endTime)
        {
            return endTime - Now;
        }

        public IObservable<long> ObserveCountDown(long endTime)
        {
            var observer = ObservableNow.Select(now => endTime - now).TakeWhile(left => left > 0);
            Debug.Log("observable");
            observer.Subscribe();
            return observer;
        }

        public IObservable<long> ObserveCountDown(long endTime, Action<long> onNext, Action onComplete,
            GameObject gameObject)
        {
            var observer = ObservableNow.Select(now => endTime - now).TakeWhile(left => left > 0);
            observer.Subscribe(left => onNext?.Invoke(left), () => onComplete?.Invoke()).AddTo(gameObject);
            return observer;
        }

        public void Set(long time)
        {
            _lastSetServerTime = time;
            _lastSetServerTimeRealTime = Time.realtimeSinceStartup;
        }
    }
}