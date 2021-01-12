namespace FileStorage.PL.Models
{
    /// <summary>
    /// StorageModel, display storage info
    /// </summary>
    public class StorageModel
    {
        public string UserName { get; set; }

        public string CreationDate { get; set; }

        public long StorageSize { get; set; }
    }
}