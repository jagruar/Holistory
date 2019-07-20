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

        public static Region NorthAmerica => new Region(4, "North America");

        public static Region SouthAmerica => new Region(5, "South America");

        public static Region Oceania => new Region(6, "South America");

        public static Region EasternAsia => new Region(7, "South America");

        public static Region SouthernAsia => new Region(8, "Southern Asia");

        public static Region NorthernAsia => new Region(9, "Northern Asia");
    }
}
