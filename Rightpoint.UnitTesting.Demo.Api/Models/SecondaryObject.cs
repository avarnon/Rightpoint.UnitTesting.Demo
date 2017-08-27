using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rightpoint.UnitTesting.Demo.Api.Models
{
    public class SecondaryObject
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public PrimaryObject PrimaryObject { get; set; }
    }
}