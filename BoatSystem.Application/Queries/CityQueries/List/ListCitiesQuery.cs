namespace BoatSystem.Application.Queries.CityQueries.List
{
    using AutoMapper;
    using BoatSystem.Core.Interfaces;
    using BoatSystem.Application;
    using BoatSystem.Application.ViewModels;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using BoatRentalSystem.Core.Interfaces;

    public class ListCitiesQuery : IQuery<IEnumerable<CityViewModel>>
    {
    }

    public class ListCitiesHandler : IQueryHandler<ListCitiesQuery, IEnumerable<CityViewModel>>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public ListCitiesHandler(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CityViewModel>> Handle(ListCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await _cityRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CityViewModel>>(cities);
        }
    }
}
