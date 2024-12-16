
using WorstMovie.ViewModels.Response;

namespace WorstMovie.Services.Interfaces
{
    public interface IProducerRangeAwardsService
    {
        Task<ProducerRangeAwardsResponse> GetRangeAwards();
    }
}
