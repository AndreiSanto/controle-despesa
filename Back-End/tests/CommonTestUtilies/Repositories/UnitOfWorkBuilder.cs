using controleDespesa.Domain.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilies.Repositories
{
    public class UnitOfWorkBuilder
    {
        public static IUnitOfWork Build()
        {
            var mock = new Mock<IUnitOfWork>();

            return mock.Object; 
        }
    }
}
