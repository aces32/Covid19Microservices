namespace Covid19.IndividualService.Core.Features.Command.BookCovidTest
{
    using System.Runtime.Serialization;

    public enum TestType
    {
        [EnumMember(Value = "pcrtest")]
        PcrTest,

        [EnumMember(Value = "rapidtest")]
        RapidTest
    }

}
