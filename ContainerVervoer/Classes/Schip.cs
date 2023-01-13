using ContainerVervoer.Classes.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer.Classes
{
    public class Schip
    {
        public int maxGewicht { get; set; }
        public int breedte { get; set; }
        public List<Vak> vakken = new List<Vak>();

        public Schip(int maxGewicht, int breedte)
        {
            this.maxGewicht = maxGewicht;
            this.breedte = breedte;
        }

        public void SortLightToHeavy()
        {
            List<Vak> middenVakken = vakken.Where(vak => vak.kant == Kant.midden).ToList();
            List<Vak> linksVakken = vakken.Where(vak => vak.kant == Kant.links).OrderBy(vak => vak.TotaalGewicht()).ToList();
            List<Vak> rechtsVakken = vakken.Where(vak => vak.kant == Kant.rechts).OrderBy(vak => vak.TotaalGewicht()).ToList();

            List<Vak> sortedVakken = new List<Vak>();
            sortedVakken.AddRange(middenVakken);

            if (linksVakken.Sum(vak => vak.TotaalGewicht()) < rechtsVakken.Sum(vak => vak.TotaalGewicht()))
            {
                sortedVakken.AddRange(linksVakken);
                sortedVakken.AddRange(rechtsVakken);
            }
            else
            {
                sortedVakken.AddRange(rechtsVakken);
                sortedVakken.AddRange(linksVakken);
            }

            vakken = sortedVakken;
        }



        public bool ContainerFit(Container container)
       {
            List<Vak> mogelijkeVakken = new List<Vak>();
            if (vakken.Sum(vak => vak.TotaalGewicht()) + container.gewicht < maxGewicht)
            {
                if (container.type == ContainerType.gekoeld)
                {
                    mogelijkeVakken = ReturnVoor();
                    foreach (Vak vak in mogelijkeVakken)
                    {
                        if (vak.ContainerFit(container) && IsBalanced(container.gewicht, vak))
                        {
                            vak.PlaceContainer(container);
                            return true;
                        }
                    }
                }
                else if (container.type == ContainerType.normaal)
                {
                    mogelijkeVakken = ReturnMiddenVoorAchter();
                    foreach (Vak vak in mogelijkeVakken)
                    {
                        if (vak.ContainerFit(container) && IsBalanced(container.gewicht, vak))
                        {
                            vak.PlaceContainer(container);
                            return true;
                        }
                    }
                }
                else if (container.type == ContainerType.waardevol)
                {
                    mogelijkeVakken = ReturnVoorAchter();
                    foreach (Vak vak in mogelijkeVakken)
                    {
                        if (vak.ContainerFit(container) && IsBalanced(container.gewicht, vak))
                        {
                            vak.PlaceContainer(container);
                            return true;
                        }
                    }
                }
                return false;
            }
            return false;
        }

        public List<Vak> ReturnVoorAchter()
        {
            List<Vak> voorAchter = new List<Vak>();
            voorAchter = vakken.Where(vak => vak.positie == Positie.achterkant).ToList();
            voorAchter.AddRange(vakken.Where(vak => vak.positie == Positie.voorkant).ToList());
            return voorAchter;
        }

        public List<Vak> ReturnMiddenVoorAchter()
        {
            List<Vak> all = new List<Vak>();
            all = vakken.Where(vak => vak.positie == Positie.midden).ToList();
            all.AddRange(vakken.Where(vak => vak.positie == Positie.voorkant).ToList());
            all.AddRange(vakken.Where(vak => vak.positie == Positie.achterkant).ToList());
            return all;
        }

        public List<Vak> ReturnVoor()
        {
            List<Vak> voor = new List<Vak>();
            voor = vakken.Where(vak => vak.positie == Positie.voorkant).ToList();
            return voor;
        }

        public int BerekenLinksGewicht()
        {
            return vakken.Where(v => v.kant == Kant.links).Sum(v => v.TotaalGewicht());
        }

        public int BerekenRechtsGewicht()
        {
            return vakken.Where(v => v.kant == Kant.rechts).Sum(v => v.TotaalGewicht()); 
        }

        public int BerekenMiddenGewicht()
        {
            return vakken.Where(v => v.kant == Kant.midden).Sum(v => v.TotaalGewicht());
        }

        public bool IsBalanced(int gewicht, Vak vak)
        {
            double totaalGewicht = 0;
            double linksGewicht = Convert.ToDouble(BerekenLinksGewicht()); ;
            double rechtsGewicht = Convert.ToDouble(BerekenRechtsGewicht()); ;

            if (breedte/2 == 1)
            {
                totaalGewicht = vakken.Sum(v => v.TotaalGewicht()) - vakken.Where(v=>v.kant == Kant.midden).Sum(v => v.TotaalGewicht());
            }
            else
            {
                totaalGewicht = vakken.Sum(v => v.TotaalGewicht());
            }

            

            if (totaalGewicht - vakken.Where(v => v.kant == Kant.midden).Sum(v => v.TotaalGewicht()) < 13)
            {
                return true;
            }

            switch (vak.kant)
            {
                case Kant.links:
                    if ((linksGewicht + (double)gewicht) / totaalGewicht * 100 <= 60)
                    {
                        return true;
                    }
                    return false;

                case Kant.rechts:
                    if ((rechtsGewicht + (double)gewicht) / totaalGewicht * 100 <= 60)
                    {
                        return true;
                    }
                    return false;

                case Kant.midden:
                    return true;
            }
            return false;
        }
    }
}