namespace AaDS.String;

static class Time
{
    static string TimeInWords(int h, int m)
    {
        string[] minutes = new string[] {
            "o' clock", "one minute", "two minutes", "three minutes", "four minutes", "five minutes", "six minutes", "seven minutes", "eight minutes", "nine minutes", "ten minutes", "eleven minutes", "twelve minutes", "thirteen minutes", "four minutesteen minutes", "quarter", "sixteen minutes", "seventeen minutes", "eighteen minutes", "nineteen minutes", "twenty minutes", "twenty one minutes", "twenty two minutes", "twenty three minutes", "twenty four minutes", "twenty five minutes", "twenty six minutes", "twenty seven minutes", "twenty eight minutes", "twenty nine minutes", "half"
        };
        string[] hour = new string[] {
            "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve"
        };
        if (m == 0) return new string($"{hour[h - 1]} {minutes[m]}");
        return m > 30 ? new string($"{minutes[60 - m]} to {hour[h % hour.Length]}") 
            : new string($"{minutes[m]} past {hour[h - 1]}"); 
    }
}