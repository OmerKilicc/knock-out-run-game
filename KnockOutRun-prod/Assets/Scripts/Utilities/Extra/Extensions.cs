using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Euphrates
{
    public static class Extensions
    {
        public static T GetRandomItem<T>(this List<T> list) => list[Random.Range(0, list.Count)];
        public static bool Swap<T>(this List<T> list, int first, int second)
        {
            if (first == second || first < 0 || first >= list.Count || second < 0 || second >= list.Count)
                return false;

            (list[second], list[first]) = (list[first], list[second]);
            return true;
        }
        public static void SetLayer(this GameObject gameObject, int layer, bool changeChildren = false)
        {
            gameObject.layer = layer;

            if (!changeChildren)
                return;

            void SetChildLayer(Transform parent, int layer)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Transform child = parent.GetChild(i);
                    child.gameObject.layer = layer;
                    SetChildLayer(child, layer);
                }
            }

            SetChildLayer(gameObject.transform, layer);
        }

        public static string GetNamePath(this GameObject gameObject, string divider = "_")
        {
            StringBuilder sb = new StringBuilder(gameObject.name);
            
            Transform current = gameObject.transform.parent;
            while (current != null)
            {
                sb.AppendFormat("{0}{1}", divider, current.gameObject.name);
                current = current.parent;
            }

            sb.AppendFormat("__sibling_index-{0}", gameObject.transform.GetSiblingIndex());

            return sb.ToString();
        }

        public static T GetFirstParentsComponent<T>(this Transform transform)
        {
            Transform cur = transform;
            
            while (cur != null)
            {
                if (!cur.TryGetComponent<T>(out T comp))
                {
                    cur = cur.parent;
                    continue;
                }

                return comp;
            }

            return default(T);
        }
    }
}
