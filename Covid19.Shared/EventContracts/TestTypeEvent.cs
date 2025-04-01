namespace Covid19.Shared.EventContracts
{
    using System.Runtime.Serialization;

    public enum TestTypeEvent
    {
        [EnumMember(Value = "pcrtest")]
        PcrTest,

        [EnumMember(Value = "rapidtest")]
        RapidTest
    }

}
    