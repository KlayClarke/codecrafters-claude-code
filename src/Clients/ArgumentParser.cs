using System.Text.RegularExpressions;

namespace ArgumentParser
{
    public static class ArgumentParser
    {
        public static string ParseArgs(string[] args)
        {
            if (args.Length == 0)
            {
                throw new Exception("Args is empty");
            }
            
            string requiredFlag = "-p";
            string argsString = string.Join(" ", args);
            string pattern = @"(?<!\S)-[^\s-]+(?:\s[^\s-]+)*";
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = r.Match(argsString);

            string? prompt = null;
            while (m.Success)
            {
                string currentMatch = m.Groups[0].Value;
                if (currentMatch.StartsWith(requiredFlag + " "))
                {
                    prompt = currentMatch.Split(" ", 2)[1];
                    break;                
                }

                m = m.NextMatch();
            }

            if (prompt == null) throw new Exception("Missing -p flag or value");

            return prompt;
        }
    }
}