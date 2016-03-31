using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NinjaDomain.Classes;
using NinjaDomain.DataModel;
using System.Data.Entity;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //disable database initialization
            Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>());

            //InsertNinja();
            //InsertMultipleNinjas();
            //SimpleNinjaQueries();
            //QueryAndUpdateNinja();
            //RetrieveDataWithFind();
            //RetrieveDataWithStoredProc();
            //InstertRelatedData_1();
            RetrieveRelatedData_1();

        }

        private static void InsertNinja()
        {
            var ninja = new Ninja
            {
                Name = "SampsonSan",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(2008, 1, 28),
                ClanId = 1
            };

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.Add(ninja);
                context.SaveChanges();
            }
        }

        private static void InsertMultipleNinjas()
        {
            var ninja1 = new Ninja
            {
                Name = "Leonardo",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1984, 1, 1),
                ClanId = 1
            };

            var ninja2 = new Ninja
            {
                Name = "Raphael",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1985, 1, 1),
                ClanId = 1
            };

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.AddRange(new List<Ninja> { ninja1, ninja2 });
                context.SaveChanges();
            }
        }

        private static void SimpleNinjaQueries()
        {
            using (var context = new NinjaContext())
            {
                var ninja = context.Ninjas.Where(n => n.DateOfBirth >= new DateTime(1984,1,1)).FirstOrDefault();

                //foreach (var item in ninjas)
                //{
                    Console.WriteLine(ninja.Name);
                //}
            }
        }

        private static void QueryAndUpdateNinja()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.FirstOrDefault();
                ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);
                context.SaveChanges();
            }
        }

        private static void QueryAndUpdateDisconected()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

            }
        }

        private static void RetrieveDataWithFind()
        {
            var keyVal = 4;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var ninja = context.Ninjas.Find(keyVal);
                Console.WriteLine("After find #1 {0}", ninja.Name);

                var someNinja = context.Ninjas.Find(keyVal);
                Console.WriteLine("After find #2 {0}", ninja.Name);

                ninja = null;
            }
        }

        private static void RetrieveDataWithStoredProc()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninjas = context.Ninjas.SqlQuery("exec GetOldNinjas {0}", new DateTime(1980,1,1)).ToList();
                Console.WriteLine(ninjas.FirstOrDefault().Name);
                //foreach(var ninja in ninjas)
                //{
                //    Console.WriteLine(ninja.Name);
                //}
                            
                
            }
        }

        private static void InstertRelatedData_1()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var ninja = new Ninja()
                {
                    Name = "George Tekelis",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1990,1,14),
                    ClanId = 1
                };

                var muscles = new NinjaEquipment()
                {
                    Name = "Muscles",
                    Type = EquipmentType.Tool
                };

                var spunk = new NinjaEquipment()
                {
                    Name = "Spunk",
                    Type = EquipmentType.Weapon
                };

                context.Ninjas.Add(ninja);
                ninja.EquipmentOwned.Add(muscles);
                ninja.EquipmentOwned.Add(spunk);
                context.SaveChanges();

            }
        }

        private static void RetrieveRelatedData_1()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var ninja = context.Ninjas                           
                    .FirstOrDefault(n => n.Name.StartsWith("George"));

                Console.WriteLine(ninja.Name);

                //Console.WriteLine(ninja.EquipmentOwned.Count());
            }
        }
    }
}
