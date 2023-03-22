using UnityEngine;

namespace Euphrates
{
    [CreateAssetMenu(fileName = "New Bool SO", menuName = "SO Variables/Bool")]
    public class BoolSO : SOVariable<bool>
	{
        public BoolSO(bool val) => this.Value = val;

        public static implicit operator bool(BoolSO so) => so.Value;
        public static explicit operator BoolSO(bool value)
        {
            BoolSO rval = ScriptableObject.CreateInstance<BoolSO>();
            rval.Value = value;
            return rval;
        }
    }
}
