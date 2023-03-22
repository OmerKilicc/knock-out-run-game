using System.Collections.Generic;
using UnityEngine;

namespace Euphrates
{
    public static class TweenOps
    {
        public static object FloatLerp(TweenData<object> data, float time)
        {
            float step = (time - data.Start) / data.Duration;
            return Color.Lerp((Color)data.From, (Color)data.To, step);
        }

        public static object ColorLerp(TweenData<object> data, float time)
        {
            float step = (time - data.Start) / data.Duration;
            return Color.Lerp((Color)data.From, (Color)data.To, step);
        }
        
        public static object Vector2Lerp(TweenData<object> data, float time)
        {
            float step = (time - data.Start) / data.Duration;
            return Vector2.Lerp((Vector2)data.From, (Vector2)data.To, step);
        }

        public static object Vector3Lerp(TweenData<object> data, float time)
        {
            float step = (time - data.Start) / data.Duration;
            return Vector3.Lerp((Vector3)data.From, (Vector3)data.To, step);
        }
    }
}
