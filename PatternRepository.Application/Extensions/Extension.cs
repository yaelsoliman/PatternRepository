
using Microsoft.IdentityModel.Tokens;



namespace PatternRepository.Application.Extensions
{
    public static class Extension
    {
        public  static bool IsEmpty(this string? value)
        {
            if (value is null) return true;

            return value.IsNullOrEmpty();
        }
       
        public static bool HasValue(this string value)
        {
            return !value.IsEmpty() || value.Length>0;
        }
     
       

    }
}
