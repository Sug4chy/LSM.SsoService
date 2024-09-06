namespace LSM.SsoService.Infrastructure.Persistence.Extensions;

public static class StringExtensions
{
    public static string ToSnakeCase(this string str)
    {
        var chars = str.ToCharArray().ToList();
        chars[0] = char.ToLowerInvariant(chars[0]);

        for (int i = 1; i < chars.Count; i++)
        {
            if (char.IsUpper(chars[i]) is false)
            {
                continue;
            }
            
            chars[i] = char.ToLowerInvariant(chars[i]);
            chars.Insert(i, '_');
        }
        
        return new string(chars.ToArray());
    }
}