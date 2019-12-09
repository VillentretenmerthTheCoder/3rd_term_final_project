using System;
using System.Collections.Generic;
using System.Text;

namespace UDPListener
{
    class Conversation
    {

        // Variables
        #region
        private string question;
        private string timeOfConversation;
        #endregion

        // Properties
        #region
        public string Question
        {
            get { return question; }
            set { question = value; }
        }

       
        public string TimeOfConversation
        {
            get { return timeOfConversation; }
            set { timeOfConversation = value; }
        }
        #endregion

        // Constructors
        #region
        public Conversation(string _question, string _timeOfConversation)
        {
            Question = _question;
            TimeOfConversation = _timeOfConversation;
        }

        // Empty constructor for JSON
        public Conversation()
        {

        }
        #endregion

        public override string ToString()
        {
            return $"Date and time: {TimeOfConversation}, Question: {Question}";
        }

    }

}

