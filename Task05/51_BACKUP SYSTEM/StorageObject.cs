using System.Collections.Generic;

namespace _51_BACKUP_SYSTEM
{
    public class StorageObject
    {
        public bool IsDirectory { get; } = false;

        public string FullName { get; } = string.Empty;

        public string Contest { get; } = string.Empty;

        public StorageObject(string fullName, string contest, bool isDirectory)
        {
            FullName = fullName;
            Contest = contest;
            IsDirectory = isDirectory;
        }

        public override bool Equals(object obj)
        {
            var strObj = obj as StorageObject;

            if (strObj.IsDirectory != IsDirectory)
            {
                return false;
            }

            if (strObj.FullName != FullName)
            {
                return false;
            }

            if (strObj.Contest != Contest)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = 467590022;
            hashCode = hashCode * -1521134295 + IsDirectory.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FullName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Contest);

            return hashCode;
        }
    }
}
