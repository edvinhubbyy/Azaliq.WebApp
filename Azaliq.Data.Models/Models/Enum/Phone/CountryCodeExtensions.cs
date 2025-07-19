using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azaliq.Data.Models.Models.Enum.Phone
{
    public static class CountryCodeExtensions
    {
        public static string ToPhonePrefix(this CountryCode code)
        {
            return code switch
            {
                CountryCode.Bulgaria => "+359",
                CountryCode.UnitedStates => "+1",
                CountryCode.Germany => "+49",
                CountryCode.France => "+33",
                CountryCode.Italy => "+39",
                CountryCode.Spain => "+34",
                CountryCode.UnitedKingdom => "+44",
                CountryCode.Australia => "+61",
                CountryCode.India => "+91",
                CountryCode.China => "+86",
                CountryCode.Japan => "+81",
                CountryCode.Russia => "+7",
                CountryCode.Brazil => "+55",
                CountryCode.Mexico => "+52",
                CountryCode.SouthAfrica => "+27",
                CountryCode.Netherlands => "+31",
                CountryCode.Sweden => "+46",
                CountryCode.Norway => "+47",
                CountryCode.Denmark => "+45",
                CountryCode.Finland => "+358",
                CountryCode.Poland => "+48",
                CountryCode.Greece => "+30",
                CountryCode.Turkey => "+90",
                CountryCode.Argentina => "+54",
                CountryCode.Belgium => "+32",
                CountryCode.Switzerland => "+41",
                CountryCode.Austria => "+43",
                CountryCode.Portugal => "+351",
                CountryCode.Ireland => "+353",
                CountryCode.NewZealand => "+64",
                CountryCode.Israel => "+972",
                CountryCode.SaudiArabia => "+966",
                CountryCode.Thailand => "+66",
                CountryCode.SouthKorea => "+82",
                CountryCode.Vietnam => "+84",
                CountryCode.Malaysia => "+60",
                CountryCode.Singapore => "+65",
                CountryCode.Philippines => "+63",
                CountryCode.Colombia => "+57",
                CountryCode.Chile => "+56",
                CountryCode.Peru => "+51",
                CountryCode.Hungary => "+36",
                CountryCode.CzechRepublic => "+420",
                CountryCode.Ukraine => "+380",
                _ => ""
            };
        }
    }
}
