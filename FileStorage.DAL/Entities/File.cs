namespace FileStorage.DAL.Entities
{
    /// <summary>
    /// File entity
    /// </summary>
    public class File : BaseEntity
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string Type { get; set; }

        public string CreationDate { get; set; }

        public int Length { get; set; }

        public bool IsRemove { get; set; }

        public int StorageId { get; set; }

        public Storage Storage { get; set; }
    }
}