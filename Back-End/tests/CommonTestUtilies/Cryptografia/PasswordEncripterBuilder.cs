using controleDespesa.Application.Service.Cryptografia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilies.Cryptografia
{
    public class PasswordEncripterBuilder
    {
        public static PasswordEncripter Build() => new PasswordEncripter();
    }
}
