using ContainerVervoer.Classes.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer.Classes
{
        public class Vak
        {
            public Positie positie { get; set; }
            public Kant kant { get; set; }
            public List<Container> containers = new List<Container>();

            public Vak(Positie positie, Kant kant)
            {
                this.positie = positie;
                this.kant = kant;
            }

        public int TotaalGewicht()
        {
            return containers.Sum(container => container.gewicht);
        }

        public bool ContainerFit(Container container)
        {
            if (containers.Count != 0)
            {
                if(TotaalGewicht() - containers[0].gewicht + container.gewicht > 120)
                {
                    return false;
                }
                if(containers.Last().type == ContainerType.waardevol)
                {
                    return false;
                }
            }
            return true;
        }

        public void PlaceContainer(Container container)
        {
            containers.Add(container);
        }
    }
}
