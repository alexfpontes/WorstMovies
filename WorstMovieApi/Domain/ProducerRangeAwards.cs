namespace WorstMovie.Domain;

public class ProducerRangeAwards
{
    public string Producer { get; set; }
    public int Interval { get; set; }
    public int PreviousWin { get; set; } 
    public int FollowingWin { get; set; } 
}