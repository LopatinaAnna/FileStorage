namespace FileStorage.PL.Models
{
    /// <summary>
    /// SharedViewModel, display shared file
    /// </summary>
    public class SharedViewModel
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string Type { get; set; }

        public int Length { get; set; }

        public string SharedLink { get; set; }
    }
}