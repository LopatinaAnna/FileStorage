namespace FileStorage.BLL.DTO
{
    /// <summary>
    /// File dto
    /// </summary>
    public class FileDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string SharedLink { get; set; }

        public string Type { get; set; }

        public string CreationDate { get; set; }

        public int Length { get; set; }

        public bool IsRemove { get; set; }

        public int StorageId { get; set; }
    }
}