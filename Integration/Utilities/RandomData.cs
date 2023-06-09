namespace Integration.Utilities
{
    public static class RandomData
    {
        private static Random s_random = new Random();

        public static string GetRandomString(int length = 11) => new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length).Select(s => s[s_random.Next(s.Length)]).ToArray());
        public static int GetRandomInt(int minLength = 0, int maxLength = 9) => s_random.Next(minLength, maxLength);

        public static string GeneratePassword(int lowercase = 3, int uppercase = 3, int numerics = 3, int specific = 3)
        {
            const string lowers = "abcdefghijklmnopqrstuvwxyz";
            const string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string number = "0123456789";
            const string symbol = "!@#$%&*()_";

            string generated = "!";
            for (int i = 1; i <= uppercase; i++)
                generated = generated.Insert(s_random.Next(generated.Length), uppers[s_random.Next(uppers.Length - 1)].ToString());

            for (int i = 1; i <= numerics; i++)
                generated = generated.Insert(s_random.Next(generated.Length), number[s_random.Next(number.Length - 1)].ToString());

            for (int i = 1; i <= specific; i++)
                generated = generated.Insert(s_random.Next(generated.Length), symbol[s_random.Next(symbol.Length - 1)].ToString());

            for (int i = 1; i <= lowercase; i++)
                generated = generated.Insert(s_random.Next(generated.Length), lowers[s_random.Next(lowers.Length - 1)].ToString());

            return generated.Replace("!", string.Empty);
        }

        public static List<int> GetListOfUniqueNumbers(int length = 3, int generateNumberLength = 17)
        {
            List<int> randomList = new List<int>();
            int number = 0;

            while (randomList.Count != length)
            {
                number = s_random.Next(0, generateNumberLength);
                if (!randomList.Contains(number))
                    randomList.Add(number);
            }
            return randomList;
        }

        public static DateTime GetRandomDay()
        {
            DateTime start = new DateTime(1900, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(s_random.Next(range));
        }
    }
}
