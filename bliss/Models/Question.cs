using System;
using System.Collections.Generic;

namespace Models
{
    public class Question : BaseEntity
    {
        private string _imageUrl;
        private string _thumbUrl;
        private DateTime _publishedAt;
        private string _description;

        public Question(
                    string imageUrl,
                    string thumbUrl,
                    string description
                    )
        {
            _imageUrl = imageUrl;
            _thumbUrl = thumbUrl;
            _description = description;
            _publishedAt = DateTime.UtcNow;

            Choices = new List<PossibleAnswer>();
        }

        public string ImageUrl
        {
            get => _imageUrl;
            private set => _imageUrl = value;
        }
        public string ThumbUrl
        {
            get => _thumbUrl;
            private set => _thumbUrl = value;
        }
        public DateTime PublishedAt
        {
            get => _publishedAt;
            private set => _publishedAt = value;
        }
        public string Description
        {
            get => _description;
            private set => _description = value;
        }
        public virtual ICollection<PossibleAnswer> Choices { get; set; }

        public void UpdateImageUrl(string imageUrl)
        {
            _imageUrl = imageUrl;
        }
        public void UpdateThumbUrl(string thumbUrl)
        {
            _thumbUrl = thumbUrl;
        }
        public void UpdateDescription(string description)
        {
            _description = description;
        }
        public void AddChoice(PossibleAnswer ans)
        {
            Choices.Add(ans);
        }
        public void RemoveChoices()
        {
            Choices = new List<PossibleAnswer>();
        }
    }
}
