using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileStorage.DAL.Entities
{
    /// <summary>
    /// The user profile class, contain user data
    /// </summary>
    public class ClientProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}