using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Rightpoint.UnitTesting.Demo.Infrastructure.Data;

namespace Rightpoint.UnitTesting.Demo.Infrastructure.Migrations
{
    internal static class SeedExtensions
    {
        public static void AddPrimaryObjects(this DemoContext context)
        {
            if (context.Set<Domain.Models.PrimaryObject>().Any()) return;

            var testModels = Enumerable.Range(1, 100)
                .Select(i => new Domain.Models.PrimaryObject(Guid.Parse($"00000000-0000-0000-{i.ToString("0000")}-000000000000"))
                {
                    Name = $"Primary Object {i}",
                    Description = $"This is primary object {i}",
                })
                .ToArray();

            context.Set<Domain.Models.PrimaryObject>().AddOrUpdate(s => s.Id, testModels);
        }

        public static void AddSecondaryObjects(this DemoContext context)
        {
            if (context.Set<Domain.Models.SecondaryObject>().Any()) return;

            var testModels = context.Set<Domain.Models.PrimaryObject>()
                .ToList()
                .SelectMany(o => Enumerable.Range(1, 100)
                .Select(i => new Domain.Models.SecondaryObject(Guid.Parse($"00000000-0000-0000-{o.Id.ToString().Split('-')[3]}-{i.ToString("000000000000")}"))
                {
                    Name = $"Primary Object {i}",
                    Description = $"This is primary object {i}",
                    PrimaryObject = o,
                    PrimaryObject_Id = o.Id,
                }))
                .ToArray();

            context.Set<Domain.Models.SecondaryObject>().AddOrUpdate(s => s.Id, testModels);
        }
    }
}
