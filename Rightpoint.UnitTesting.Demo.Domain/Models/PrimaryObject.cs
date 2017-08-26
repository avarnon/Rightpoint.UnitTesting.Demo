using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EnsureThat;

namespace Rightpoint.UnitTesting.Demo.Domain.Models
{
    public class PrimaryObject : IIdentifiable<Guid>
    {
        public PrimaryObject(Guid id)
            : this()
        {
            Ensure.That(id, nameof(id)).IsNotEmpty();

            this.Id = id;
        }

        private PrimaryObject()
        {
            this.SecondaryObjects = new Collection<SecondaryObject>();
        }

        public Guid Id { get; private set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<SecondaryObject> SecondaryObjects { get; set; }
    }
}
