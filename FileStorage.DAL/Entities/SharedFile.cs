namespace FileStorage.DAL.Entities
{
    /// <summary>
    /// Shared file entity
    /// </summary>
    public class SharedFile : BaseEntity
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string Type { get; set; }

        public int Length { get; set; }

        public string SharedLink { get; set; }
    }
}