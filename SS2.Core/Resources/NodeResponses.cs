using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS2.Core.Resources
{
    public class NodeResponses
    {
        // Success
        public static string NodeInflitrated = "Node inflitrated.";
        public static string PasswordSpoofSuccessful = "Password spoof \nsuccessful.";
        public static string DataTransferInitiated = "Data transfer initiated.";

        // Failed
        public static string IllegalCoreAccess = "Illegal core access!";
        public static string BufferOverflow = "Buffer overflow.";
        public static string AccessViolationDetected = "Access violation detected!";

        public static string GetRandomResponse(bool success)
        {
            if (success)
            {
                return NodeInflitrated;
            } else
            {
                return IllegalCoreAccess;
            }
        }
    }
}
