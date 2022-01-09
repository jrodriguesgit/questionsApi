using System;
using System.Collections.Generic;

namespace Shared.ResponsesDtos
{
    public class ResponseQuestionDto
    {
        public int Id { get; set; }
        public string image_url { get; set; }
        public string thumb_url { get; set; }
        public DateTime? published_at { get; set; }
        public string question { get; set; }

        public List<ResponseChoiceDto> choices { get; set; }
    }
}
