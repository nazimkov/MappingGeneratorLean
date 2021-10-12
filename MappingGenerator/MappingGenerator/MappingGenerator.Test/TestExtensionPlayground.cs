using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MappingGenerator.Test
{
    internal static class TestExtensionPlayground
    {
        private static B Map(A a)
        {
            return new B
            {
                Id = a.Id,
                Name = a.Name,
                Cs = a.Cs.Select(aC => new D
                {
                    Id = aC.Id
                }).ToList()
            };
        }

        private class A
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public IReadOnlyCollection<C> Cs { get; set; }
        }

        private class B
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public IReadOnlyCollection<D> Cs { get; set; }
        }

        private class C
        {
            public int Id { get; set; }
        }

        private class D
        {
            public int Id { get; set; }
        }
    }
}
