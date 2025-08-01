using UnityEngine;

namespace SuperEasyRPG
{
    public static class ObjectUtil
    {
        public static T FindSingletonObjectOfType<T>(bool includeInactive = false) where T : Object
        {
            T[] objects = Object.FindObjectsOfType<T>(includeInactive);
            if (objects.Length > 1)
            {
                throw new System.Exception($"Multiple {nameof(T)} detected. There should be only one ${nameof(T)} in a scene.");
            }
            else if (objects.Length == 0)
            {
                return null;
            }
            return objects[0];
        }
    }
}