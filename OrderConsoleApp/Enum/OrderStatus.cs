﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderConsoleApp.Enum
{
    public enum OrderStatus
    {
        New,
        InWarehouse,
        InShipment,
        ReturnedToClient,
        Error,
        Closed
    }
}
