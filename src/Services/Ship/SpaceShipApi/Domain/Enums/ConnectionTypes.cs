using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums;
[Flags]
public enum ConnectionType
{
    None = 0,      // 0000
    Up = 1 << 0,   // 0001 (1)
    Down = 1 << 1, // 0010 (2)
    Left = 1 << 2, // 0100 (4)
    Right = 1 << 3 // 1000 (8)
}
