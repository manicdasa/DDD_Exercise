using FluentResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Entities
{
    public class Degree
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Degree stage (the higher the stage, the higher the degree). Used for comparing degrees.
        /// </summary>
        public int Stage { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        public static Degree Bachelor => new Degree() { Id = 1, Stage = 1, Value = "Bachelor", Description = "Bachelor" };

        public static Result<Degree> From(int degreeId)
        {
            return Result.Ok(Degree.Bachelor);
        } 
    }
}
