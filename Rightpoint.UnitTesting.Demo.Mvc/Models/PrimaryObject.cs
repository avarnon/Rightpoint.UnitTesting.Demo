using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rightpoint.UnitTesting.Demo.Mvc.Models
{
    public class PrimaryObject
    {
        public Guid? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public IEnumerable<SecondaryObject> SecondaryObjects { get; set; }
    }
}