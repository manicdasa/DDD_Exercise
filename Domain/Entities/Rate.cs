using GhostWriter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Entities
{
    public class Rate
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Star rating (1-5)
        /// </summary>
        public int StarRating { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Rate Writer
        /// </summary>
        public RateWriter RateWriter { get; set; }

        /// <summary>
        /// Booking
        /// </summary>
        public virtual Booking Booking { get; set; }
    }
}
