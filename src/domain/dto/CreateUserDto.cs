namespace Testes.src.domain.dto
{
    public class CreateUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";

        public CreateUserDto() {}

        public CreateUserDto(int Id, string Email, string Password)
        {
            this.Id = Id;
            this.Email = Email;
            this.Password = Password;
        }
    }
}

