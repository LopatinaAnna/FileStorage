using System.Collections.Generic;

namespace FileStorage.PL.Models
{
    /// <summary>
    /// IndexPageViewModel, display files with paging
    /// </summary>
    public class IndexPageViewModel
    {
        public IEnumerable<FileViewModel> Files { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}