using System;
using System.Collections.Generic;

namespace Application.Mappings
{
    public abstract class MapperBase
    {
        protected List<Tout> ConvertList<Tin, Tout>(List<Tin> list, Func<Tin, Tout> convert)
        {
            var listResponse = new List<Tout>();

            foreach (Tin tin in list)
            {
                listResponse.Add(convert.Invoke(tin));
            }

            return listResponse;
        }
    }
}
