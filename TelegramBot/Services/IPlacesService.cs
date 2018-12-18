using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Services
{
    public interface IPlacesService
    {
        string FindPlace(double latitude, double longitude);

        string GetMap(string placeLoc);
    }
}
