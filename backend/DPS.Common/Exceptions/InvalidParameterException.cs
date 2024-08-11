using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPS.Common.Exceptions;
public class InvalidParameterException(string message) : Exception(message) {

}
