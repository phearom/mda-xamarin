using System;
namespace xapp001
{
    public class IPConstants
    {
#if DEBUG
        public const string CoverageServiceEndpoint = "https://sandbox-mda.mekongsms.com/ipcheck";
        public const string AuthServiceEndpoint = "https://sandbox-mda.mekongsms.com/verify/submit";
        public const string RedirectUri = "https://mekongnet.com.kh";
        public const string ClientId = "f7157293c7805312a29d88cd5f4725...";
#else
        public const string CoverageServiceEndpoint = "https://sandbox-mda.mekongsms.com/ipcheck";
        public const string AuthServiceEndpoint = "https://sandbox-mda.mekongsms.com/verify/submit";
        public const string RedirectUri = "https://mekongnet.com.kh";
        public const string ClientId = "f7157293c7805312a29d88cd5f4725....";
#endif
    }
}