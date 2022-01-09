using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shared.RequestsDtos
{
    public class RequestQuestionDto
    {
        [Required]
        public string image_url { get; set; }
        [Required]
        public string thumb_url { get; set; }
        [Required]
        public string question { get; set; }
        [Required]
        public List<string> choices { get; set; }
    }
}
