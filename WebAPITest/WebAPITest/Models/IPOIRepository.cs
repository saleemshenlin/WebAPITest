using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPITest.Models
{
    interface IPOIRepository
    {
        IEnumerable<POI> GetAllPOIs();
        POI GetByID(int id);
        POI AddPOI(POI item);
        void RemovePOI(int id);
        bool UpdatePOI(POI item);
    }
}