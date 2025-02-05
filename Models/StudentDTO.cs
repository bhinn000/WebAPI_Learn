using System.ComponentModel.DataAnnotations;

namespace WebAPI_Learn.Models
{
    public class StudentDTO
    {
        [Required]
        public int ID { get; set; }
        public string StudentName { get; set; }
        public int Roll { get; set; }
        public string Symbol { get; set; }

        //[EmailAddress]
        //public string Email { get; set; }

    }
}
