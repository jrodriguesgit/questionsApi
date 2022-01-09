namespace Models
{
    public class PossibleAnswer : BaseEntity
    {
        private string _answer;
        private int _votes;
        private int _questionId;
        public PossibleAnswer(
                    string answer,
                    int questionId)
        {
            _answer = answer;
            _votes = 0;
            _questionId = questionId;
        }
        public PossibleAnswer(
                    string answer,
                    int votes,
                    int questionId)
        {
            _answer = answer;
            _votes = votes;
            _questionId = questionId;
        }

        public string Answer
        {
            get => _answer;
            private set => _answer = value;
        }
        public int Votes
        {
            get => _votes;
            private set => _votes = value;
        }
        public int QuestionId
        {
            get => _questionId;
            private set => _questionId = value;
        }
        public virtual Question Question { get; set; }
    }
}