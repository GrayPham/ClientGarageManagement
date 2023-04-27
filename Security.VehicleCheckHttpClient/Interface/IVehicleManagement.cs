using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.VehicleCheckHttpClient.Interface
{
    public interface IVehicleManagement
    {
        Task<string> CheckInVehicleAsync(string platenum, string typeTransport, string typeLP);
    }
}
