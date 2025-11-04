using controleDespesa.Domain.Interface;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilies.MemoryCache
{
    public class MemoryCacheBuilder
    {

        public static IMemoryCache Build()
        {
            var mock = new Mock<IMemoryCache>();

            return mock.Object;
        }
    }
}
