namespace FileStorage.PL.Models
{
    /// <summary>
    /// FileViewModel, display file info
    /// </summary>
    public class FileViewModel
    {
        public string Name { get; set; }

        public int Length { get; set; }

        public string CreationDate { get; set; }
    }
}