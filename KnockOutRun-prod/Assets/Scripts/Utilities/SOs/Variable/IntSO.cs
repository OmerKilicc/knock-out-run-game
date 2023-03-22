using System;
using UnityEngine;

namespace Euphrates
{
    [CreateAssetMenu(fileName = "New Integer SO", menuName = "SO Variables/Integer")]
	public class IntSO : SOVariable<int>
    {
        protected override int SubtractInternal(int x, int y) => x - y;

        public static implicit operator int(IntSO so) => so.Value;
        public static explicit operator IntSO(int value)
        {
            IntSO rval = ScriptableObject.CreateInstance<IntSO>();
            rval.Value = value;

            return rval;
        }

        public static IntSO operator +(IntSO var1, IntSO var2) => DoOp(() => var1.Value + var2.Value);
        public static IntSO operator +(IntSO var1, int var2) => DoOp(() => var1.Value + var2);
        public static IntSO operator +(int var1, IntSO var2) => DoOp(() => var1 + var2);
        public static IntSO operator -(IntSO var1, IntSO var2) => DoOp(() => var1.Value - var2.Value);
        public static IntSO operator -(IntSO var1, int var2) => DoOp(() => var1.Value - var2);
        public static IntSO operator -(int var1, IntSO var2) => DoOp(() => var1 - var2.Value);
        public static IntSO operator *(IntSO var1, IntSO var2) => DoOp(() => var1.Value * var2.Value);
        public static IntSO operator *(IntSO var1, int var2) => DoOp(() => var1.Value * var2);
        public static IntSO operator *(int var1, IntSO var2) => DoOp(() => var1 * var2.Value);
        public static IntSO operator /(IntSO var1, IntSO var2) => DoOp(() => var1.Value / var2.Value);
        public static IntSO operator /(IntSO var1, int var2) => DoOp(() => var1.Value / var2);
        public static IntSO operator /(int var1, IntSO var2) => DoOp(() => var1 / var2.Value);
        public static IntSO operator %(IntSO var1, IntSO var2) => DoOp(() => var1.Value % var2.Value);
        public static IntSO operator %(IntSO var1, int var2) => DoOp(() => var1.Value % var2);
        public static IntSO operator %(int var1, IntSO var2) => DoOp(() => var1 % var2.Value);

        static IntSO DoOp(Func<int> op)
        {
            IntSO rval = CreateInstance<IntSO>();
            rval.Value = op.Invoke();
            return rval;
        }

        public void Add(int amt) => Value += amt;

        public void SetTo(IntSO var) => Value = var.Value;
    }
}