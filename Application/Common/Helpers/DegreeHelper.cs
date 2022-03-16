using GhostWriter.Domain.Entities;

namespace GhostWriter.Application.Common.Helpers
{
    public static class DegreeHelper
    {
        public static bool IsHigherThan(this Degree source, Degree degree)
        {
            if (degree == null)
                return true;

            return source.Stage > degree.Stage;
        }

        public static bool IsHigherOrEqualTo(this Degree source, Degree degree)
        {
            if (degree == null)
                return true;

            return source.Stage >= degree.Stage;
        }

        public static bool IsLowerThan(this Degree source, Degree degree)
        {
            if (degree == null)
                return true;

            return source.Stage < degree.Stage;
        }

        public static bool IsLowerorEqualTo(this Degree source, Degree degree)
        {
            if (degree == null)
                return true;

            return source.Stage <= degree.Stage;
        }
    }
}
