using EmailGateway.Configurations;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EmailGateway.AssignKudos
{
    public class EmailCheckerService
    {
        readonly IAvailableEmailList _availableEmailList;

        public EmailCheckerService(IAvailableEmailList availableEmailList)
        {
            _availableEmailList = availableEmailList;
        }
        
        public bool IsEmailInText(string text)
        {
            var result = GetEmailsFromText(text);
            return result != null && result.Count() > 0;
        }

        public bool IsBeginningEmailInText(string text)
        {
            var result = GetBeginningEmailsFromText(text);
            return result != null && result.Count() > 0;
        }

        private List<string> GetEmailsFromText(string text)
        {
            const string MatchEmailPattern =
                @"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})";
            var rx = new Regex(MatchEmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            // Find matches.
            MatchCollection matches = rx.Matches(text);
            // Report the number of matches found.
            int noOfMatches = matches.Count;

            if(noOfMatches > 0)
            {
                return matches.Select(x => x.Value).ToList();
            }

            return null;
        }

        private List<string> GetBeginningEmailsFromText(string text)
        {
            var listWords = text.Split(" ");

            return listWords.Where(word => word.Contains('.')).ToList();
        }

        public List<string> GetEmailsFromListByBeginning(string text)
        {
            return GetBeginningEmailsFromText(text)
                .Where(email => _availableEmailList.EmailList.Select(x => x.Split("@")[0]).Contains(email))
                .Select(preperEmail => $"{preperEmail}@rockwool.com")
                .ToList();
        }

        public List<string> GetEmailsFromListByEmails(string text)
        {
            return GetEmailsFromText(text)
                .Where(email => _availableEmailList.EmailList.Contains(email))
                .ToList();
        }

        public List<string> GetRockwoolEmails(string text)
        {
            return GetEmailsFromText(text)
                .Where(email => email.Contains("@rockwool.com"))
                .ToList();
        }
    }
}
