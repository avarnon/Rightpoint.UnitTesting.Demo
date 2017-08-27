using System;
using System.Collections.Generic;

namespace Rightpoint.UnitTesting.Demo.Mvc.Contracts.Models
{
    public class PrimaryObject
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<SecondaryObject> SecondaryObjects { get; set; }
    }
}