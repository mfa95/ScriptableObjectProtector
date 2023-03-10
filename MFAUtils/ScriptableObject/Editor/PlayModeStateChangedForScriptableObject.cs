
using UnityEditor;
using MFA.SO;

namespace MFA.Editor
{
    [InitializeOnLoadAttribute]
    internal static class PlayModeStateChanged
    {
        static PlayModeStateChanged() => EditorApplication.playModeStateChanged += LogPlayModeState;

        private static void LogPlayModeState(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.ExitingPlayMode: //PLAY modundan cikarken
                    ExitingPlayMode();
                    break;
                case PlayModeStateChange.ExitingEditMode: //PLAY moduna girerken
                    ExitingEditMode();
                    break;
            }
        }

        private static void ExitingEditMode()
        {
            var baseList = AssetDatabaseUtility.FindLoadAllAssets<ScriptableObjectBase>();
            for (int i = 0; i < baseList.Length; i++)
                _ = baseList[i].ExitingEditMode();

            AssetDatabase.SaveAssets();
        }

        private static void ExitingPlayMode()
        {
            var baseList = AssetDatabaseUtility.FindLoadAllAssets<ScriptableObjectBase>();
            for (int i = 0; i < baseList.Length; i++)
                _ = baseList[i].ExitingPlayMode();
        }
    }
}