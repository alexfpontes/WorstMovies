using WorstMovie.Domain;

namespace WorstMovie.ViewModels.Response
{
    public class ProducerRangeAwardsResponse
    {
        public List<ProducerRangeAwards> Min { get; set; }
        public List<ProducerRangeAwards> Max { get; set; }
    }
}
