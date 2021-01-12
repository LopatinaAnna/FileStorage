namespace FileStorage.BLL.DTO
{
    /// <summary>
    /// Storage dto
    /// </summary>
    public class StorageDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string CreationDate { get; set; }

        public long StorageSize { get; set; }
    }
}