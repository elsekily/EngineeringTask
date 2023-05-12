
using AutoMapper;
using UserAPI.Entities.Models;
using UserAPI.Entities.Resources;

namespace UserAPI.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserResource>()
            .ForMember(ur => ur.Address, opt => opt.Ignore())
            .ForMember(ur => ur.BirthDate, opt => opt.Ignore());

        CreateMap<BirthdateAddressCombination, UserResource>()
            .ForMember(ur => ur.UserName, opt => opt.Ignore())
            .ForMember(ur => ur.FirstName, opt => opt.Ignore())
            .ForMember(ur => ur.FatherName, opt => opt.Ignore())
            .ForMember(ur => ur.FamilyName, opt => opt.Ignore());



        CreateMap<UserSaveResource, User>()
            .ForMember(u => u.Id, opt => opt.Ignore())
            .ForMember(u => u.HashedPassword, opt => opt.Ignore())
            .ForMember(u => u.EncryptedData, opt => opt.Ignore());

        CreateMap<UserSaveResource, BirthdateAddressCombination>();

    }
}