using AutoMapper;
using CPUMiroservice.Dtos;
using CPUMiroservice.Models;
using CPUMiroservice.Protos;

namespace CPUMiroservice.Profiles
{
    public class CPUProfile : Profile
    {
        public CPUProfile() 
        {
            CreateMap<CPU, CPUReadDTO>();
            CreateMap<CPUCreateDTO, CPU>();
            CreateMap<CPUReadDTO, CPUPublishedDTO>();
            CreateMap<CPU, GrpcCPUModel>()
                .ForMember(dest => dest.CPUId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => src.Frequency))
                .ForMember(dest => dest.Cache, opt => opt.MapFrom(src => src.Cache))
                .ForMember(dest => dest.TechnicalProcess, opt => opt.MapFrom(src => src.TechnicalProcess))
                .ForMember(dest => dest.NumberOfCores, opt => opt.MapFrom(src => src.NumberOfCores))
                .ForMember(dest => dest.NumebrOfThreads, opt => opt.MapFrom(src => src.NumebrOfThreads));
        }
    }
}
