using System;
using System.Runtime.InteropServices;

namespace ValidateDomain
{
    class Program
    {
        public enum NET_API_STATUS : uint
        {
            NERR_Success = 0,

            /// <summary>
            /// The DNS name contains an invalid character. This error is returned if the NameType parameter
            /// specified is NetSetupDnsMachine and the DNS name in the lpName parameter contains an invalid character.
            /// </summary>
            DNS_ERROR_INVALID_NAME_CHAR = 9560,

            /// <summary>
            /// The DNS name does not comply with RFC specifications. This error is returned if the NameType
            /// parameter specified is NetSetupDnsMachine and the DNS name in the lpName parameter does not
            /// comply with RFC specifications.
            /// </summary>
            DNS_ERROR_NON_RFC_NAME = 9556,

            /// <summary>
            /// A duplicate name already exists on the network.
            /// </summary>
            ERROR_DUP_NAME = 52,

            /// <summary>
            /// The format of the specified computer name is not valid.
            /// </summary>
            ERROR_INVALID_COMPUTERNAME = 1210,

            /// <summary>
            /// A parameter is incorrect. This error is returned if the lpName parameter is
            /// NULL or the NameType parameter is specified as NetSetupUnknown or an unknown nametype.
            /// </summary>
            ERROR_INVALID_PARAMETER = 87,

            /// <summary>
            /// The specified domain does not exist.
            /// </summary>
            ERROR_NO_SUCH_DOMAIN = 1355,

            /// <summary>
            /// The request is not supported. This error is returned if a remote computer was specified in
            /// the lpServer parameter and this call is not supported on the remote computer.
            /// </summary>
            ERROR_NOT_SUPPORTED = 50,

            /// <summary>
            /// This computer name is invalid.
            /// </summary>
            NERR_InvalidComputer = 2351,

            /// <summary>
            /// The specified workgroup name is invalid.
            /// </summary>
            NERR_InvalidWorkgroupName = 2695,

            /// <summary>
            /// The RPC server is not available. This error is returned if a remote computer was specified in
            /// the lpServer parameter and the RPC server is not available.
            /// </summary>
            RPC_S_SERVER_UNAVAILABLE = 2147944122,

            /// <summary>
            /// Remote calls are not allowed for this process. This error is returned if a remote computer
            /// was specified in the lpServer parameter and remote calls are not allowed for this process.
            /// </summary>
            RPC_E_REMOTE_DISABLED = 2147549468
        }
        enum NET_SETUP_NAMETYPE
        {
            NetSetupUnknown,
            NetSetupMachine,
            NetSetupWorkgroup,
            NetSetupDomain,
            NetSetupNonExistentDomain,
            NetSetupDnsMachine
        }

        [DllImport("netapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern UInt32 NetValidateName(string lpServer, string lpName, string lpAccount, string lpPassword, NET_SETUP_NAMETYPE NameType);

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: ValidateDomain.exe DomainName");
                return;
            }

            var result = (NET_API_STATUS)NetValidateName(null, args[0], null, null, NET_SETUP_NAMETYPE.NetSetupDomain);

            Console.WriteLine($"Result: {result}");
        }
    }
}
