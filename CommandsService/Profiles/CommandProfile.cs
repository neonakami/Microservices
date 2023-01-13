using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;
using CommandsService.Protos;

namespace CommandsService.Profiles
{
    public class CommandProfile : Profile
    {
        public CommandProfile() 
        {
            CreateMap<CPU, CPUReadDTO>();
            CreateMap<CommandCreateDTO, Command>();
            CreateMap<Command, CommandReadDTO>();
            CreateMap<CPUPublishedDTO, CPU>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id));
            CreateMap <GrpcCPUModel, CPU> ()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.CPUId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Commands, opt => opt.Ignore());
        }
    }
}
