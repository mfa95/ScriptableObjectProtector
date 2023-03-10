
using UnityEngine;

namespace MFA.SO
{
    public abstract class ScriptableObjectBase : ScriptableObject
    {
#if UNITY_EDITOR

        /// <summary>
        /// Play modundan cikinca eski degerlerine dondurulsun mu?
        /// </summary>
        [SerializeField] protected bool canResetAfterPlayMode = false;
        [Space(10)]

        /// <summary>
        /// Datanin json formatinda gecici yedegi
        /// </summary>
        private string _reserveBackup = string.Empty;

        /// <summary>
        /// Play Moduna girerken scriptableobjecti yedekle
        /// </summary>
        public bool ExitingEditMode()
        {
            if (!canResetAfterPlayMode) return false;
            _reserveBackup = JsonUtility.ToJson(this);
            return true;
        }

        /// <summary>
        /// Play Modundan cikarken yedegi, scriptableobjectin uzerine yaz.
        /// </summary>
        public bool ExitingPlayMode()
        {
            if (!canResetAfterPlayMode || string.IsNullOrEmpty(_reserveBackup)) return false;
            JsonUtility.FromJsonOverwrite(_reserveBackup, this);
            _reserveBackup = string.Empty;
            ResetSO();
            return true;
        }

#endif

        /// <summary>
        /// Veriler datanin uzerine yazildiktan (ExitingPlayMode) hemen sonra calisir! (istege bagli) [Serilestirilemeyen verileri manuel olarak sifirlamak isteyebilirsiniz]
        /// </summary>
        protected virtual void ResetSO() {}
    }
}