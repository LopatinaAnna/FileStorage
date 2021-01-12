using System.Collections.Generic;

namespace FileStorage.DAL.Entities
{
    /// <summary>
    /// Storage entity
    /// </summary>
    public class Storage : BaseEntity
    {
        public string UserName { get; set; }

        public long StorageSize { get; set; }

        public string CreationDate { get; set; }

        public List<File> StorageFiles { get; set; }
    }
}