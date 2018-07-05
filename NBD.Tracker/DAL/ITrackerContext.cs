using NBD.SDK;
using System.Linq;

namespace NBD.Tracker.DAL
{
    public interface ITrackerContext
    {
        IQueryable<Goal> Goals { get; set; }
    }
}
