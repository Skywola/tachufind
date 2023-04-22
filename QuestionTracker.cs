using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tachufind
{
    // NOTE - Since the dictionary does not need ZERO like an array would, beginning key is ONE
    // This keeps track of the sets which contain one question and one answer (currentQA)
    public class QuestionTracker
    {
        public int currentQueueKey; 

        public string question { get; set; }

        public string answer { get; set; }

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

        public QuestionTracker(int currentQueueKey)
        {
            this.currentQueueKey = currentQueueKey;
        }

        public QuestionTracker()
        {
            this.currentQueueKey = 0;
        }
    }
}


