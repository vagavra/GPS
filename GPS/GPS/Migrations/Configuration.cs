namespace GPS.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GPSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GPSContext context)
        {
            context.Elements.AddOrUpdate(x => x.Name,
                new Node() { Name = "Glavni kolodvor", IsNode = true, X = 300, Y = 110 },
                new Node() { Name = "Trg bana J.Jelacica", IsNode = true, X = 300, Y = 60 },
                new Node() { Name = "Kaptol", IsNode = true, X = 300, Y = 40 },
                new Node() { Name = "PMF", IsNode = true, X = 350, Y = 25 },
                new Node() { Name = "Britanski trg", IsNode = true, X = 150, Y = 60 },
                new Node() { Name = "Lisinski", IsNode = true, X = 310, Y = 130 },
                new Node() { Name = "Zagrepcanka", IsNode = true, X = 180, Y = 130 },
                new Node() { Name = "Studentski dom S.Radic", IsNode = true, X = 180, Y = 290 },
                new Node() { Name = "Avenue Mall", IsNode = true, X = 310, Y = 290 }
            );

            context.SaveChanges();

            context.Elements.AddOrUpdate(x => x.Name,
                new Edge()
                {
                    Name = "Ulica Zrinjevac",
                    IsNode = false,
                    StartId = GetNodeId("Trg bana J.Jelacica", context),
                    EndId = GetNodeId("Glavni kolodvor", context),
                    SingleDirection = false,
                    Distance = Util.Distance(300, 110, 300, 60)
                });

            context.SaveChanges();

            context.Elements.AddOrUpdate(x => x.Name,
            new Edge()
            {
                Name = "Kaptol ulica",
                IsNode = false,
                StartId = GetNodeId("Trg bana J.Jelacica", context),
                EndId = GetNodeId("Kaptol", context),
                SingleDirection = true,
                Distance = Util.Distance(300, 60, 300, 40)
            });

            context.SaveChanges();

            context.Elements.AddOrUpdate(x => x.Name,
                new Edge()
                {
                    Name = "Ulica N.Grskovica",
                    IsNode = false,
                    StartId = GetNodeId("Kaptol", context),
                    EndId = GetNodeId("PMF", context),
                    SingleDirection = false,
                    Distance = Util.Distance(300, 40, 350, 25)
                });

            context.SaveChanges();

            context.Elements.AddOrUpdate(x => x.Name,
                new Edge()
                {
                    Name = "Ilica",
                    IsNode = false,
                    StartId = GetNodeId("Trg bana J.Jelacica", context),
                    EndId = GetNodeId("Britanski trg", context),
                    SingleDirection = true,
                    Distance = Util.Distance(300, 60, 150, 60)
                });

            context.SaveChanges();

            context.Elements.AddOrUpdate(x => x.Name,
                new Edge()
                {
                    Name = "Paromlinska cesta",
                    IsNode = false,
                    StartId = GetNodeId("Glavni kolodvor", context),
                    EndId = GetNodeId("Lisinski", context),
                    SingleDirection = false,
                    Distance = Util.Distance(300, 110, 310, 130)
                });

            context.SaveChanges();

            context.Elements.AddOrUpdate(x => x.Name,
                new Edge()
                {
                    Name = "Ulica grada Vukovara",
                    IsNode = false,
                    StartId = GetNodeId("Lisinski", context),
                    EndId = GetNodeId("Zagrepcanka", context),
                    SingleDirection = false,
                    Distance = Util.Distance(310, 130, 180, 130)
                });

            context.SaveChanges();

            context.Elements.AddOrUpdate(x => x.Name,
                new Edge()
                {
                    Name = "Savska cesta",
                    IsNode = false,
                    StartId = GetNodeId("Zagrepcanka", context),
                    EndId = GetNodeId("Studentski dom S.Radic", context),
                    SingleDirection = false,
                    Distance = Util.Distance(180, 130, 180, 290)
                });

            context.SaveChanges();

            context.Elements.AddOrUpdate(x => x.Name,
                new Edge()
                {
                    Name = "Avenija Dubrovnik",
                    IsNode = false,
                    StartId = GetNodeId("Studentski dom S.Radic", context),
                    EndId = GetNodeId("Avenue Mall", context),
                    SingleDirection = false,
                    Distance = Util.Distance(180, 290, 310, 290)
                });

            context.SaveChanges();

            context.Elements.AddOrUpdate(x => x.Name,
                new Edge()
                {
                    Name = "Avenija V.Holjevca",
                    IsNode = false,
                    StartId = GetNodeId("Lisinski", context),
                    EndId = GetNodeId("Avenue Mall", context),
                    SingleDirection = false,
                    Distance = Util.Distance(310, 130, 310, 290)
                }
            );

            context.SaveChanges();

            context.Characteristics.AddOrUpdate(x => x.Id,
                new Characteristic()
                {
                    Id = 1,
                    ElementId = GetNodeId("Trg bana J.Jelacica", context),
                    CharacteristicType = Characteristic.CharacteristicTypes.Trgovina
                },
                new Characteristic()
                {
                    Id = 2,
                    ElementId = GetNodeId("Trg bana J.Jelacica", context),
                    CharacteristicType = Characteristic.CharacteristicTypes.Restoran
                },
                new Characteristic()
                {
                    Id = 3,
                    ElementId = GetNodeId("Trg bana J.Jelacica", context),
                    CharacteristicType = Characteristic.CharacteristicTypes.Ljekarna
                },
                new Characteristic()
                {
                    Id = 4,
                    ElementId = GetNodeId("Britanski trg", context),
                    CharacteristicType = Characteristic.CharacteristicTypes.Trgovina
                },
                new Characteristic()
                {
                    Id = 5,
                    ElementId = GetNodeId("Glavni kolodvor", context),
                    CharacteristicType = Characteristic.CharacteristicTypes.Trgovina
                },
                new Characteristic()
                {
                    Id = 6,
                    ElementId = GetNodeId("Glavni kolodvor", context),
                    CharacteristicType = Characteristic.CharacteristicTypes.Pošta
                },
                new Characteristic()
                {
                    Id = 7,
                    ElementId = GetNodeId("Glavni kolodvor", context),
                    CharacteristicType = Characteristic.CharacteristicTypes.Benzinska
                },
                new Characteristic()
                {
                    Id = 8,
                    ElementId = GetNodeId("Avenue Mall", context),
                    CharacteristicType = Characteristic.CharacteristicTypes.Trgovina
                },
                new Characteristic()
                {
                    Id = 9,
                    ElementId = GetNodeId("Avenue Mall", context),
                    CharacteristicType = Characteristic.CharacteristicTypes.Garaža
                },
                new Characteristic()
                {
                    Id = 10,
                    ElementId = GetNodeId("Avenue Mall", context),
                    CharacteristicType = Characteristic.CharacteristicTypes.Restoran
                }




            );


        }

        private int GetNodeId(string Name, GPSContext context)
        {
            var node = context.Elements.Where(el => el.Name == Name).FirstOrDefault();

            return node.ElementId;
        }
    }
}