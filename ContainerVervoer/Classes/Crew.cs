using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer.Classes
{
    public class Crew
    {
        public List<Container> containers = new List<Container>();
        Random random = new Random();

        public Crew()
        {
        }

        public void SortHeavyToLight()
        {
            if (containers.Count == 0)
            {
                throw new Exception("Cannot sort an empty list of containers.");
            }

            bool swapped;

            do
            {
                swapped = false;

                for (int i = 0; i < containers.Count - 1; i++)
                {
                    if (containers[i].gewicht < containers[i + 1].gewicht)
                    {
                        Container temp = containers[i];
                        containers[i] = containers[i + 1];
                        containers[i + 1] = temp;
                        swapped = true;
                    }
                }
            } while (swapped);
        }

        public void SortGekoeldNormaalWaardevol()
        {
            if (containers.Count == 0)
            {
                throw new Exception("Cannot sort an empty list of containers.");
            }

            bool swapped;
            do
            {
                swapped = false;

                for (int i = 0; i < containers.Count - 1; i++)
                {
                    if ((int)containers[i].type > (int)containers[i + 1].type)
                    {
                        Container temp = containers[i];
                        containers[i] = containers[i + 1];
                        containers[i + 1] = temp;
                        swapped = true;
                    }
                }
            } while (swapped);
        }



        public void GenerateContainers(int aantal)
        {
            if (aantal <= 0)
            {
                throw new ArgumentOutOfRangeException("Number of containers must be greater than 0");
            }

            for (int i = 0; i < aantal; i++)
            {
                containers.Add(new(random.Next(4, 31), Container.RandomType()));
            }
        }

        public Schip GenerateSchip(int breedte, int varLengte)
        {
            if (breedte <= 0)
            {
                throw new ArgumentException("The breedte of the ship must be greater than 0.");
            }

            if (varLengte <= 0)
            {
                throw new ArgumentException("The lengte of the ship must be greater than 0.");
            }

            int aantalVakken = breedte * varLengte;
            int lengte = varLengte;
            int maxGewicht = aantalVakken * 150;
            Schip schip = new Schip(maxGewicht, breedte);

            List<Vak> vakken = new List<Vak>();
            bool heeftMidden = false;
            if (breedte % 2 == 1)
            {
                heeftMidden = true;
            }
            int aantalVakkenPerKant = 0;
            if (!heeftMidden)
            {
                aantalVakkenPerKant = aantalVakken / 2;
            }
            else
            {
                aantalVakkenPerKant = (aantalVakken - lengte) / 2;
            }

            if (heeftMidden)
            {
                //vakken op de voorkant toevoegen
                for (int i = 0; i < (int)Math.Floor((double)breedte / 2); i++)
                {
                    vakken.Add(new(Enums.Positie.voorkant, Enums.Kant.links));
                }
                vakken.Add(new(Enums.Positie.voorkant, Enums.Kant.midden));
                for (int i = 0; i < (int)Math.Floor((double)breedte / 2); i++)
                {
                    vakken.Add(new(Enums.Positie.voorkant, Enums.Kant.rechts));
                }

                //vakken op het middenstuk toevoegen
                for (int j = 0; j < (lengte - 2); j++)
                {
                    for (int i = 0; i < (int)Math.Floor((double)breedte / 2); i++)
                    {
                        vakken.Add(new(Enums.Positie.midden, Enums.Kant.links));
                    }
                    vakken.Add(new(Enums.Positie.midden, Enums.Kant.midden));
                    for (int i = 0; i < (int)Math.Floor((double)breedte / 2); i++)
                    {
                        vakken.Add(new(Enums.Positie.midden, Enums.Kant.rechts));
                    }
                }

                //Vakken op de achterkant toevoegen
                for (int i = 0; i < (int)Math.Floor((double)breedte / 2); i++)
                {
                    vakken.Add(new(Enums.Positie.achterkant, Enums.Kant.links));
                }
                vakken.Add(new(Enums.Positie.achterkant, Enums.Kant.midden));
                for (int i = 0; i < (int)Math.Floor((double)breedte / 2); i++)
                {
                    vakken.Add(new(Enums.Positie.achterkant, Enums.Kant.rechts));
                }
            }
            else
            {
                //vakken op de voorkant toevoegen
                for (int i = 0; i < breedte/2; i++)
                {
                    vakken.Add(new(Enums.Positie.voorkant, Enums.Kant.links));
                }
                for (int i = 0; i < breedte/2; i++)
                {
                    vakken.Add(new(Enums.Positie.voorkant, Enums.Kant.rechts));
                }

                //vakken op het middenstuk toevoegen
                for (int j = 0; j < (lengte - 2); j++)
                {
                    for (int i = 0; i < breedte/2; i++)
                    {
                        vakken.Add(new(Enums.Positie.midden, Enums.Kant.links));
                    }
                    for (int i = 0; i < breedte/2; i++)
                    {
                        vakken.Add(new(Enums.Positie.midden, Enums.Kant.rechts));
                    }
                }

                //Vakken op de achterkant toevoegen
                for (int i = 0; i < breedte/2; i++)
                {
                    vakken.Add(new(Enums.Positie.achterkant, Enums.Kant.links));
                }
                for (int i = 0; i < breedte/2; i++)
                {
                    vakken.Add(new(Enums.Positie.achterkant, Enums.Kant.rechts));
                }
            }

            schip.vakken = vakken;
            return schip;
        }
    }
}
