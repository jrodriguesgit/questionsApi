using System.ComponentModel.DataAnnotations;

namespace Shared.RequestsDtos
{
    public class RequestUpdateChoiceDto
    {
        [Required]
        public string choice { get; set; }
        [Required]
        public int votes { get; set; }
    }
}
