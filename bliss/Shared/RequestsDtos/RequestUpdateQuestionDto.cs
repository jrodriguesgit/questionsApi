using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shared.RequestsDtos
{
    public class RequestUpdateQuestionDto
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string image_url { get; set; }
        [Required]
        public string thumb_url { get; set; }
        [Required]
        public string question { get; set; }

        [Required]
        public List<RequestUpdateChoiceDto> choices { get; set; }
    }
}
