using System;
using EnsureThat;

namespace Rightpoint.UnitTesting.Demo.Domain.Models
{
    public class SecondaryObject : IIdentifiable<Guid>
    {
        public SecondaryObject(Guid id)
            : this()
        {
            Ensure.That(id, nameof(id)).IsNotEmpty();

            this.Id = id;
        }

        private SecondaryObject()
        {
        }

        public Guid Id { get; private set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid PrimaryObject_Id { get; set; }

        public PrimaryObject PrimaryObject { get; set; }
    }
}
