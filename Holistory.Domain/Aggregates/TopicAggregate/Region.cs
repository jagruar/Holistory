using Holistory.Domain.Seedwork;

namespace Holistory.Domain.Aggregates.TopicAggregate
{
    public class Region : Enumeration
    {
        public Region(int id, string name)
            : base(id, name)
        {
        }

        public static Region Africa => new Region(1, "Africa");

        public static Region Europe => new Region(2, "Europe");

        public static Region MiddleEast => new Region(3, "The Middle East");

        public static Region Americas => new Region(4, "The Americas");

        public static Region Asia => new Region(5, "Asia");

        public static Region Oceania => new Region(6, "Oceania");
    }
}
