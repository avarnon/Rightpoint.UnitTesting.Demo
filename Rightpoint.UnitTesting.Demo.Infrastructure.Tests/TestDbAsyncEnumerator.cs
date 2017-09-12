using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Rightpoint.UnitTesting.Demo.Infrastructure.Tests
{
    /// <summary>
    /// Helper class for mocking IDbAsyncEnumerators.
    /// </summary>
    /// <typeparam name="T">The type of the inner objects</typeparam>
    /// <remarks>From https://msdn.microsoft.com/en-us/data/dn314431</remarks>
    [ExcludeFromCodeCoverage]
    public class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestDbAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public T Current
        {
            get { return _inner.Current; }
        }

        object IDbAsyncEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }
    }
}
