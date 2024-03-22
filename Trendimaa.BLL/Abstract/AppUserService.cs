using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Orde.BLL.Extension.Token;
using Trendeimaa.Entities;
using Trendimaa.BLL.Extension;
using Trendimaa.BLL.Extension.Token;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;
using Trendimaa.DTO;

namespace Trendimaa.BLL.Abstract
{
    public class AppUserService : Service<AppUser>, IAppUserService
    {
        public readonly IValidator<AppUser> _validator;
        public readonly IUOW _uow;
        public readonly TrendimaaContext _context;
        public readonly IMapper _mapper;
        public AppUserService(IValidator<AppUser> validator, IUOW uow, TrendimaaContext context, IMapper mapper) : base(validator, uow, context)
        {
            _validator = validator;
            _uow = uow;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IResponse<JwtTokenResponse>> Login(CLoginDto dto)
        {
            var user =await _context.AppUsers.FirstOrDefaultAsync(i => i.Email == dto.Email);
            if (user == null)
                return new Response<JwtTokenResponse>(ResponseType.NotFound, "Kullanıcı adı veya sifre hatalı");
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
            if (isValidPassword == false)
                return new Response<JwtTokenResponse>(ResponseType.ValidationError, "Kullanıcı adı veya sifre hatalı");
            dto.Id = user.Id;
            dto.Role = "AppUser";
            var token = JwtTokenGenerator.GenerateToken(dto);
            return new Response<JwtTokenResponse>(ResponseType.Success, token);
        }

        public async Task<IResponse<AppUser>> SignUp(AppUser appUser)
        {
            var result = _validator.Validate(appUser);
            if (!result.IsValid)
                return new Response<AppUser>(appUser, errors: result.ConvertToCustomValidationError());

            var rnd = new Random();
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            appUser.Password = BCrypt.Net.BCrypt.HashPassword(appUser.Password, salt);
            appUser.UserIdentity = "TRD"+rnd.Next(000000000, 99999999).ToString();

            var data = await _uow.GetRepository<AppUser>().CreateAsync(appUser);
            string jsonText = File.ReadAllText("dosya_yolu.json");
            dynamic datas = JsonConvert.DeserializeObject(jsonText);


            return new Response<AppUser>(ResponseType.Success, data);
        }
    }
}
