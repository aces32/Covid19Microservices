using System.Runtime.Serialization;

namespace Covid19.IndividualLabsService.Domain.Entities
{

    public enum TestType
    {
        [EnumMember(Value = "pcrtest")]
        PcrTest,

        [EnumMember(Value = "rapidtest")]
        RapidTest
    }

}
