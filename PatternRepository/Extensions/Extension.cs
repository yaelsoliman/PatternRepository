using Mapster;
using Microsoft.IdentityModel.Tokens;
using PatternRepository.Rsponsess;
using rm.Extensions;

namespace PatternRepository.Extensions
{
    public static class Extension
    {
        public static bool IsEmpty(this string? value)
        {
            if (value is null) return true;

            return value.IsNullOrEmpty();
        }
       
        public static bool HasValue(this string value)
        {
            return !value.IsEmpty() || value.Length>0;
        }
        public static string UpperCaseFirstWord(this string value) {
            string result = value.ToTitleCase();
            return result;
        }
        public static Response<T> AsRespons<T>(this object dto)
        {
            return new Response<T>
            {
                Message = null,
                Result=dto.Adapt<T>()
                //Result=(T)dto,
            };
        }

        public static Response<DataList<T>> AsResponsList<T>(this object dto)
        {
            return new ResponseList<T>
            {
                Message = null,
                Result = dto.Adapt<DataList<T>>()
                //Result=(T)dto,
            };
        }

    }
}
