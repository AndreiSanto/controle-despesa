using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace controleDespesa.Domain.Enums
{
    public enum SituacaoParcelaEnum : byte
    {
        [Description("Pago")]
        [EnumMember(Value = "Pago")]
        PAGO = 1,
        [Description("Vencido")]
        [EnumMember(Value = "Vencido")]
        VENCIDO = 2,
        [Description("A vencer")]
         [EnumMember(Value = "A vencer")]
         A_VENCER = 3,
    }
}

