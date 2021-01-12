namespace FileStorage.BLL.DTO
{
    /// <summary>
    /// Shared file dto
    /// </summary>
    public class SharedFileDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string Type { get; set; }

        public int Length { get; set; }

        public string SharedLink { get; set; }
    }
}