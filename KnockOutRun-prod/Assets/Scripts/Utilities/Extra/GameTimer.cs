using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Euphrates
{
    public static class GameTimer
    {
        static bool _isRunning = false;
        static readonly List<Timer> _timers = new List<Timer>();

        static List<Timer> _toBeAdded = new List<Timer>();
        static List<string> _toBeRemoved = new List<string>();

        public static void CreateTimer(string name, float duration, 
            Action onFinish = null, Action<TickInfo> onTick = null, Action onCancle = null, 
            bool useScaledTime = false)
        {
            float start = useScaledTime ? Time.time : Time.realtimeSinceStartup;
            float end = start + duration;

            Timer timer = new Timer(name, start, end, useScaledTime)
            {
                OnTick = onTick,
                OnFinish = onFinish,
                OnCancle = onCancle
            };

            _toBeAdded.Add(timer);

            if (!_isRunning)
                RunTimers();
        }


        public static void CancleTimer(string name)
        {
            int indx = -1;
            for (int i = 0; i < _timers.Count; i++)
                if (_timers[i].Name == name)
                    indx = i;

            if (indx == -1)
                return;

            _timers[indx].OnCancle?.Invoke();
            _toBeRemoved.Add(name);
        }

        static async void RunTimers()
        {
            _isRunning = true;
            float last = Time.realtimeSinceStartup;
            float lastScaled = Time.time;

            while (_timers.Count > 0 || _toBeAdded.Count > 0)
            {
                foreach (var tmr in _toBeAdded)
                    _timers.Add(tmr);
                _toBeAdded.Clear();

                foreach (var nm in _toBeRemoved)
                    _timers.RemoveAll(t => t.Name == nm);
                _toBeRemoved.Clear();

                float now = Time.realtimeSinceStartup;
                float nowScaled = Time.time;

                float deltaTime = now - last;
                float deltaScaled = nowScaled - lastScaled;

                foreach (var timer in _timers)
                {
                    float usedNow = (timer.TimeScaled ? nowScaled : now);
                    float usedDeltaTime = (timer.TimeScaled ? deltaScaled : deltaTime);

                    if (timer.End < usedNow)
                    {
                        _toBeRemoved.Add(timer.Name);
                        timer.OnFinish?.Invoke();
                        continue;
                    }

                    if (timer.OnTick == null)
                        continue;

                    var tInfo = new TickInfo(timer.Name, timer.Start, timer.End, usedNow, usedDeltaTime);
                    timer.OnTick.Invoke(tInfo);
                }

                foreach (var nm in _toBeRemoved)
                    _timers.RemoveAll(t => t.Name == nm);
                _toBeRemoved.Clear();

                last = Time.realtimeSinceStartup;
                lastScaled = Time.time;
                await Task.Yield();
            }
            _isRunning = false;
        }
    }

    struct Timer
    {
        public readonly string Name;
        public readonly float Start;
        public readonly float End;


        public Action<TickInfo> OnTick;
        public Action OnFinish;
        public Action OnCancle;

        public bool TimeScaled;

        public Timer(string name, float start, float end, bool useScaledTime)
        {
            Name = name;
            Start = start;
            End = end;

            OnTick = null;
            OnFinish = null;
            OnCancle = null;

            TimeScaled = useScaledTime;
        }
    }

    public struct TickInfo
    {
        public readonly string Name;
        public readonly float Start;
        public readonly float End;
        public readonly float Now;
        public readonly float Duration => End - Now;
        public readonly float TimePassed => Now - Start;
        public readonly float TimeLeft => End - Now;
        public readonly float DeltaTime;

        public TickInfo(string name, float start, float end, float now, float deltaTime)
        {
            Name = name;
            Start = start;
            End = end;
            Now = now;
            DeltaTime = deltaTime;
        }
    }
}
