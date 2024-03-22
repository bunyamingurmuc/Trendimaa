using AutoMapper;
using Trendimaa.BLL.Mapping;

namespace Trendimaa.BLL.Helper
{
    public static class ProfileHelper
    {
        public static List<Profile> GetProfiles()
        {

            return new List<Profile>
            {
                new Profiles()
            };
        }
    }
}
