using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Entities
{
    public class Message
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Message Text
        /// </summary>
        public string MessageText { get; set; }

        /// <summary>
        /// Detects weather the message is a system log Message, and isn't writen by user
        /// </summary>
        public bool IsLogMessage { get; set; }

        /// <summary>
        /// DateTime the message is sent
        /// </summary>
        public DateTime DateTimeSent { get; set; }

        /// <summary>
        /// User that sends the message
        /// </summary>
        public virtual ApplicationUser SentByUser { get; set; }

        /// <summary>
        /// Conversation
        /// </summary>
        public virtual Conversation Conversation { get; set; }

        /// <summary>
        /// Binary Document
        /// </summary>
        public virtual Document BinaryDocument { get; set; }
    }
}
