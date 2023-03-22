using System.Collections.Generic;
using UnityEngine;

namespace Euphrates
{
    [CreateAssetMenu(fileName = "New 3D Animation Curve SO", menuName = "SO Variables/3D Animation Curve")]
	public class AnimationCurve3DSO : ScriptableObject
	{
		public AnimationCurve X;
		public AnimationCurve Y;
		public AnimationCurve Z;
	}
}
