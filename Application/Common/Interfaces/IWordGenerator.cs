using GhostWriter.Application.Common.Models;

namespace GhostWriter.Application.Common.Interfaces
{
    public interface IWordGenerator
    {
        /// <summary>
        /// Generates username suggestion based on user's username, first name, last name and birthday
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string GenerateUsernameSuggestion(UsernameAvailabilityInputModel model);

        /// <summary>
        /// Generates random alphanumeric string of 13 characters
        /// </summary>
        /// <returns></returns>
        string GenerateRandomString();
    }
}
