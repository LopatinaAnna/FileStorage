namespace FileStorage.BLL.DTO
{
    /// <summary>
    /// The user dto, contains properties of the ApplicationUser and ClientProfile models.
    /// </summary>
    public class UserDto
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }
    }
}