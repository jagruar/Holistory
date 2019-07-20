using Holistory.Domain.Seedwork;

namespace Holistory.Domain.Aggregates.TopicAggregate
{
    public class Era : Enumeration
    {
        public Era(int id, string name)
            : base(id, name)
        {
        }

        public static Era PreHistory => new Era(1, "Pre History");

        public static Era BronzeAge => new Era(2, "Bronze Age");

        public static Era IronAge => new Era(3, "Iron Age");

        public static Era MiddleAges => new Era(4, "Middle Ages");

        public static Era Exploration => new Era(5, "Exploration");

        public static Era IndustrialRevolution => new Era(6, "Industrial Revolution");

        public static Era Modern => new Era(7, "Modern");
    }
}
