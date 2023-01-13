using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer.Classes
{
    public class Container
    {
        static Random random = new Random();
        public int gewicht { get; set; }
        public ContainerType type { get; set; }

        public Container(int gewicht, ContainerType type)
        {
            this.gewicht = gewicht;
            this.type = type;
        }

        public static ContainerType RandomType()
        {
            int i = random.Next(3);
            if (i == 0)
            {
                return ContainerType.waardevol;
            }
            if (i == 1)
            {
                return ContainerType.gekoeld;
            }
            return ContainerType.normaal;
        }
    }
}
