using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WorstMovie.Domain;
using WorstMovie.Infra;
using WorstMovie.Services.Interfaces;
using WorstMovie.ViewModels.Response;

namespace WorstMovie.Services
{
    public class ProducerRangeAwardsService(WorstMovieDbContext context) : IProducerRangeAwardsService
    {
        private readonly WorstMovieDbContext _context = context;

        public async Task<ProducerRangeAwardsResponse> GetRangeAwards()
        {
            var movies = await _context.Movies
                .Where(m => m.Winner.ToLower().Trim() == "yes")
                .ToListAsync();

            var producersList = GetProducers(movies).ToList();
            var producersRangeAwardsList = GetProducerRangeAwards(producersList);

            var producerRangeAwardsResponse = new ProducerRangeAwardsResponse
            {
                Min = MinRangeAwards(producersRangeAwardsList),
                Max = MaxRangeAwards(producersRangeAwardsList)
            };

            return producerRangeAwardsResponse;
        }

        private IEnumerable<Producer> GetProducers(List<Movie> movies)
        {
            List<Producer> producerList = new List<Producer>();
            foreach (var movie in movies)
            {
                var producerNameList = movie.Producers.Split([", and ", ", ", " and "], StringSplitOptions.TrimEntries);
                for (int i = 0; i < producerNameList.Length; i++)
                {
                    producerList.Add(new Producer
                    {
                        Name = producerNameList[i],
                        Year = movie.Year
                    });
                }
            }

            return producerList;
        }

        private List<ProducerRangeAwards> GetProducerRangeAwards(List<Producer> producersList)
        {
            List<ProducerRangeAwards> ProducerRangeAwardsList = new List<ProducerRangeAwards>();

            var producersGroupedList = producersList.GroupBy(p => p.Name).OrderBy(p => p.Key).ToList();

            foreach (var producerGrouped in producersGroupedList)
            {
                var name = producerGrouped.Key;
                List<int> years = new List<int>();

                foreach (var item in producerGrouped)
                {
                    years.Add(item.Year);
                }

                var yearsOrdered = years.Order().ToList();

                for (int i = 0; yearsOrdered.Count > 1 && i < yearsOrdered.Count-1; i++)
                {
                    ProducerRangeAwardsList.Add(new ProducerRangeAwards
                    {
                        Producer = name,
                        Interval = yearsOrdered[i + 1] - yearsOrdered[i],
                        PreviousWin = yearsOrdered[i],
                        FollowingWin = yearsOrdered[i + 1]
                    });
                }
            }

            return ProducerRangeAwardsList;
        }

        public List<ProducerRangeAwards> MinRangeAwards(List<ProducerRangeAwards> ProducerRangeAwardsList)
        {
            var minInterval = ProducerRangeAwardsList.Min(p => p.Interval);
            return ProducerRangeAwardsList.Where(p => p.Interval == minInterval).ToList();
        }

        public List<ProducerRangeAwards> MaxRangeAwards(List<ProducerRangeAwards> ProducerRangeAwardsList)
        {
            var maxInterval = ProducerRangeAwardsList.Max(p => p.Interval);
            return ProducerRangeAwardsList.Where(p => p.Interval == maxInterval).ToList();
        }
    }
}