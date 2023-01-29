using DevLounge.Data.Models;
using DevLounge.Service.Models.ForumUsers;

namespace DevLounge.Service.Mapping.ForumUsers
{
    public static class ForumUsersMappings
    {
        public static DevLoungeUser ToEntity(this DevLoungeUserDto devLoungeUserDto)
        {
            return new DevLoungeUser
            {
                Id = devLoungeUserDto.Id,
                UserName = devLoungeUserDto.UserName,
                Email = devLoungeUserDto.Email,
            };
        }

        public static DevLoungeUserDto ToDto(this DevLoungeUser devLoungeUser)
        {
            return new DevLoungeUserDto
            {
                Id = devLoungeUser.Id,
                UserName = devLoungeUser.UserName,
                Email = devLoungeUser.Email
            };
        }
    }
}
