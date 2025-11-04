using AutoMapper;
using controleDespesa.Application.Service.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilies.Mapper
{
    public  class MapperBuilder
    {
        public static IMapper Build()
        {
            return new MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper();
        }

    }
}
