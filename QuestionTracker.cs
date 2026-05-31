using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tachufind
{
    public class QuestionTracker
    {
        public int currentQueueKey;

        public string question { get; set; } = string.Empty;

        public string answer { get; set; } = string.Empty;

        private int _questionOrAnswerPosition;
        public int questionOrAnswerPosition
        {
            get { return _questionOrAnswerPosition; }
            set { _questionOrAnswerPosition = value; }
        }

        public void ToggleQuestionOrAnswerPosition()
        {
            int newValue = (_questionOrAnswerPosition == 0) ? 1 : 0;
            questionOrAnswerPosition = newValue;
        }

        // Constructor with parameter
        public QuestionTracker(int currentQueueKey)
        {
            this.currentQueueKey = currentQueueKey;
            this.question = string.Empty; // Ensuring non-nullable field is initialized
            this.answer = string.Empty;   // Ensuring non-nullable field is initialized
        }

        // Default constructor
        public QuestionTracker()
        {
            this.currentQueueKey = 0;
            this.question = string.Empty; // Ensuring non-nullable field is initialized
            this.answer = string.Empty;   // Ensuring non-nullable field is initialized
        }
    }
}
