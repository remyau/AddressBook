using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace AddressBook.ViewModels
{
    public class Countries
    {
        public List<string> GetAllCountryNames()
        {
            List<string> CountryList = new List<string>();
            CultureInfo[] cInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo cInfo in cInfoList)
            {
                RegionInfo rInfo = new RegionInfo(cInfo.LCID);
                if(!(CountryList.Contains(rInfo.EnglishName)))
                {
                    CountryList.Add(rInfo.EnglishName);
                }
            }

            CountryList.Sort();
            return CountryList;
        }
    }
}