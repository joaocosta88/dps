using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPS.Common.Exceptions;
public class ForbiddenOperationException(string message)  : Exception(message) {
}
