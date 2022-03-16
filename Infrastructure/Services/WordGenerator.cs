using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models;
using System;
using System.Linq;

namespace GhostWriter.Infrastructure.Services
{
    public class WordGenerator : IWordGenerator
    {

        #region Public Methods

        public string GenerateRandomString()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

            return new string(Enumerable.Repeat(chars, 13)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string GenerateUsernameSuggestion(UsernameAvailabilityInputModel model)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            var methodPicker = random.Next(0, 3);

            var username = string.Empty;

            switch (methodPicker)
            {
                case 0:
                    username = DefaultUsernameGenerator(model);
                    break;
                case 1:
                    username = UsernameGenerator1(model);
                    break;
                case 2:
                    username = UsernameGenerator2(model);
                    break;
                default:
                    username = DefaultUsernameGenerator(model);
                    break;
            }

            return username;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates username based on parameters Username
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string DefaultUsernameGenerator(UsernameAvailabilityInputModel model)
        {
            const string chars = "1234567890";
            int length = 3;
            var random = new Random(DateTime.Now.Millisecond);
            string uname = model.Username;

            var sufix = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

            return $"{uname}{sufix}";
        }

        /// <summary>
        /// Creates username based on parameters Username, FirstName and LastName. If FirstName and LastName is null, DefaultUsernameGenerator method is called 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string UsernameGenerator1(UsernameAvailabilityInputModel model)
        {
            if (string.IsNullOrEmpty(model.FirstName) && string.IsNullOrEmpty(model.LastName))
                return DefaultUsernameGenerator(model);

            var random = new Random(DateTime.Now.Millisecond);
            const string signs = "._-";
            const string chars = "1234567890";
            string uname = !string.IsNullOrWhiteSpace(model.FirstName) ? model.FirstName : model.LastName;
          
            var separator = new string(Enumerable.Repeat(signs, 1).Select(s => s[random.Next(s.Length)]).ToArray());

            if(random.Next(2) == 1)
                uname += separator + (!string.IsNullOrWhiteSpace(model.LastName) ? model.LastName : string.Empty);
            else
                uname += (!string.IsNullOrWhiteSpace(model.LastName) ? model.LastName : string.Empty);

            if (random.Next(2) == 0)
            {
                var sufix = new string(Enumerable.Repeat(chars, 2).Select(s => s[random.Next(2)]).ToArray());

                uname += sufix;
            }

            return uname;
        }

        /// <summary>
        /// Creates username based on parameters Username, FirstName, LastName and Birthday. If Birthday is null, UsernameGenerator1 method is called 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string UsernameGenerator2(UsernameAvailabilityInputModel model)
        {
            if (model.Birthday == null)
                return UsernameGenerator1(model);

            var random = new Random(DateTime.Now.Millisecond);
            const string signs = "._-";

            var uname = model.Username;

            if (random.Next(2) == 0)
            {
                var separator = new string(Enumerable.Repeat(signs, 1).Select(s => s[random.Next(s.Length)]).ToArray());
                uname += separator;
            }
            uname += model.Birthday.Value.ToString(random.Next(2) == 0 ? "yyyy" : "yy");

            return uname;
        }

        #endregion
    }
}
