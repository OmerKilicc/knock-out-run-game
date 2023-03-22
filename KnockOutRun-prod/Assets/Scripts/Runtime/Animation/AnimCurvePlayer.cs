using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Euphrates
{
	public class AnimCurvePlayer : MonoBehaviour
	{
		bool _play = false;

		[SerializeField] float _animDuration;
		[SerializeField] bool _keepEndTransform;

		[SerializeField] bool _doPositionAnim = false;
		[SerializeField] AnimationCurve3DSO _positionAnim;
		[SerializeField] bool _additivePosition;

		[SerializeField] bool _doRotationAnim = false;
		[SerializeField] AnimationCurve3DSO _rotationAnim;
		[SerializeField] bool _additiveRotation;

		[SerializeField] bool _doScaleAnim = false;
		[SerializeField] AnimationCurve3DSO _scaleAnim;
		[SerializeField] bool _additiveScale;

		[SerializeField] public UnityEvent OnAnimationEnd;

		// Cache
		Vector3 _pos;
		Vector3 _rot;
		Vector3 _scale;

        float _startTime;
		float _now;
		float _passedTime = 0f;
		float _step;
        void FixedUpdate()
        {
			if (!_play)
				return;

			_now = Time.time;

			if (_now >= _startTime + _animDuration)
			{
				_play = false;
				if (!_keepEndTransform)
					LoadTransformCache();

				OnAnimationEnd?.Invoke();

				return;
			}

			_passedTime = _now - _startTime;
			_step = Mathf.Clamp01(_passedTime / _animDuration);

			float xStep;
			float yStep;
			float zStep;

			if (_doPositionAnim)
            {
				Vector3 p = Vector3.zero;
				if (_additivePosition)
					p = _pos;

				xStep = _positionAnim.X.Evaluate(_step);
				yStep = _positionAnim.Y.Evaluate(_step);
				zStep = _positionAnim.Z.Evaluate(_step);

				p += new Vector3(xStep, yStep, zStep);
				transform.position = p;
            }

			if (_doRotationAnim)
			{
				Vector3 r = Vector3.zero;
				if (_additiveRotation)
					r = _rot;

				xStep = _rotationAnim.X.Evaluate(_step);
				yStep = _rotationAnim.Y.Evaluate(_step);
				zStep = _rotationAnim.Z.Evaluate(_step);

				r += new Vector3(xStep, yStep, zStep);
				transform.eulerAngles = r;
			}

			if (_doScaleAnim)
			{
				Vector3 s = Vector3.zero;
				if (_additiveScale)
					s = _scale;

				xStep = _scaleAnim.X.Evaluate(_step);
				yStep = _scaleAnim.Y.Evaluate(_step);
				zStep = _scaleAnim.Z.Evaluate(_step);

				s += new Vector3(xStep, yStep, zStep);
				transform.localScale = s;
			}
		}

		public void Play()
        {
			if (_play)
				LoadTransformCache();

			CacheTransform();

			_startTime = Time.time;
			_passedTime = 0f;
			_now = Time.time;
			_step = 0f;

            _play = true;

            FixedUpdate();
        }

		public void PlayReverse()
        {

        }

		void CacheTransform()
        {
			_pos = transform.position;
			_rot = transform.eulerAngles;
			_scale = transform.localScale;
		}

		void LoadTransformCache()
        {
			transform.position = _pos;
			transform.eulerAngles = _rot;
			transform.localScale = _scale;
		}

		public bool AnimationState() => _play;
    }
}
