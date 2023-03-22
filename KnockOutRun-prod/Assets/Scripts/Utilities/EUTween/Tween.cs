using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Euphrates
{
	public static class Tween
	{
        static List<TweenData<object>> _tweens = new List<TweenData<object>>();
        static bool _working;

        #region Lerp
        public static int Lerp(float var, float endVal, float duration, Action<object> onStep = null, Action onFinish = null)
        {
            object LerpFunc(TweenData<object> data, float time)
            {
                float step = (time - data.Start) / data.Duration;
                return Mathf.Lerp((float)data.From, (float)data.To, step);
            }

            TweenData<object> floatLerp = new TweenData<object>()
            {
                ID = UnityEngine.Random.Range(100000, 999999),
                From = var,
                To = endVal,
                Duration = duration,
                Start = Time.realtimeSinceStartup,
                OnStep = onStep,
                OnFinish = onFinish,
                Operation = LerpFunc
            };

            _tweens.Add(floatLerp);

            if (!_working)
                WorkTweens();

            return floatLerp.ID;
        }

        public static int Lerp(this Color var, Color endVal, float duration, Action<object> onStep = null, Action onFinish = null)
        {
            TweenData<object> colLerp = new TweenData<object>()
            {
                ID = UnityEngine.Random.Range(100000, 999999),
                From = var,
                To = endVal,
                Duration = duration,
                Start = Time.realtimeSinceStartup,
                OnStep = onStep,
                OnFinish = onFinish,
                Operation = TweenOps.FloatLerp
            };

            AddTween(colLerp);
            return colLerp.ID;
        }

        public static bool StopTween(int ID)
        {
            foreach (var tw in _tweens)
            {
                if (tw.ID != ID)
                    continue;

                _tweens.Remove(tw);
                return true;
            }

            return false;
        }
        #endregion

        static void AddTween(TweenData<object> data)
        {
            _tweens.Add(data);

            if (!_working)
                WorkTweens();
        }

        async static void WorkTweens()
        {
            if (_working)
                return;

            _working = true;

            while (_tweens.Count > 0)
            {
                for (int i = _tweens.Count - 1; i >= 0; i--)
                {
                    TweenData<object> tw = _tweens[i];

                    if (tw.Start + tw.Duration < Time.realtimeSinceStartup)
                    {
                        tw.OnStep?.Invoke(tw.To);
                        tw.OnFinish?.Invoke();
                        _tweens.RemoveAt(i);
                        continue;
                    }

                    object val = tw.Operation(tw, Time.realtimeSinceStartup);
                    tw.OnStep?.Invoke(val);
                }

                await Task.Yield();
            }

            _working = false;
        }
	}

	public struct TweenData<T>
    {
        public int ID;
        public object From;
        public object To;

        public float Start;
        public float Duration;
        public float End => Start + Duration;

        public Action<object> OnStep;
        public Action OnFinish;

        public Func<TweenData<object>, float, object> Operation;
    }
}
