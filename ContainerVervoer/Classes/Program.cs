using ContainerVervoer.Classes;

Crew crew = new();
crew.GenerateContainers(200);
Schip schip = crew.GenerateSchip(5, 7);
crew.SortHeavyToLight();
crew.SortGekoeldNormaalWaardevol();

List<Container> overig = new();

foreach (Container c in crew.containers)
{
    Console.WriteLine(c.type.ToString() + " | " + c.gewicht);
}
Console.ReadLine();
Console.WriteLine();



//----------------------------------------------------------------------------
crew.containers.Reverse();
for (int i = crew.containers.Count - 1; i >= 0; i--)
{
    schip.SortLightToHeavy();
    Container c = crew.containers[i];
    if (schip.ContainerFit(c))
    {
        crew.containers.Remove(c);
    }
    else
    {
        crew.containers.Remove(c);
        overig.Add(c);
    }
}
//----------------------------------------------------------------------------

foreach (Vak v in schip.vakken)
{
    Console.Write(v.kant + " | " + v.positie);
    if (v.TotaalGewicht() > 0)
    {
        foreach (Container c in v.containers) { Console.Write(" - " + c.type.ToString() + ":" + v.TotaalGewicht().ToString()); };
    }
    Console.WriteLine();
}
Console.WriteLine(overig.Count.ToString());
Console.ReadLine();
foreach (Container c in overig)
{
    Console.WriteLine(c.type.ToString() + " | " + c.gewicht);
}

Console.WriteLine("Links: " + schip.BerekenLinksGewicht().ToString());
Console.WriteLine("Midden: " + schip.BerekenMiddenGewicht().ToString());
Console.WriteLine("Rechts: " + schip.BerekenRechtsGewicht().ToString());

Console.ReadLine();


public class ProgramClass
{
    public Schip Master(Crew crew, Schip schip)
    {
        crew.SortHeavyToLight();
        crew.SortGekoeldNormaalWaardevol();
        crew.containers.Reverse();
        for (int i = crew.containers.Count - 1; i >= 0; i--)
        {
            schip.SortLightToHeavy();
            Container c = crew.containers[i];
            schip.ContainerFit(c);
            crew.containers.Remove(c);
        }
        return schip;
    }
}