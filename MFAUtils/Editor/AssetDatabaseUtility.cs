using UnityEditor;

namespace MFA.Editor
{
    internal abstract class AssetDatabaseUtility
    {
        internal static T[] FindLoadAllAssets<T>() where T : MFA.SO.ScriptableObjectBase
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
            T[] result = new T[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                result[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }
            return result;
        }
    }
}