using Newtonsoft.Json;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Domain.Interfaces.Repositories;
using WorkTimeTracker.Domain.Interfaces.Services;

namespace WorkTimeTracker.Infrastructure.Services
{
    internal class HolidaysProvider : IHolidaysProvider
    {
        private readonly IHolidaysRepository _repository;
        private readonly HttpClient _httpClient;

        public HolidaysProvider(IHolidaysRepository repository, HttpClient httpClient)
        {
            _repository = repository;
            _httpClient = httpClient;
        }

        public async Task FetchHolidaysAsync(int year)
        {
            var response = await _httpClient.GetAsync($"https://date.nager.at/api/v3/PublicHolidays/{year}/PL");
            var content = await response.Content.ReadAsStringAsync();
            var holidays = JsonConvert.DeserializeObject<IEnumerable<Holiday>>(content) ?? [];

            await _repository.CreateHolidaysAsync(holidays);
        }

        public async Task<IEnumerable<Holiday>> GetHolidaysAsync(int year)
        {
            var holidays = await _repository.GetHolidaysAsync(year);
            
            if(!holidays.Any())
            {
                await FetchHolidaysAsync(year);
                holidays = await _repository.GetHolidaysAsync(year);
            }

            return holidays;
        }
    }
}
