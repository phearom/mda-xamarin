using ObjCRuntime;

namespace IPificationSDK
{
	[Native]
	public enum GCDAsyncSocketError : long
	{
		NoError = 0,
		BadConfigError,
		BadParamError,
		ConnectTimeoutError,
		ReadTimeoutError,
		WriteTimeoutError,
		ReadMaxedOutError,
		ClosedError,
		OtherError
	}

	[Native]
	public enum CellularError : long
	{
		NotActive = 0,
		Callback_nil = 1,
		Request_nil = 2,
		Connection_error = 3,
		General = 4,
		Cannot_connect = 5,
		Validation = 6,
		Authorized_failed = 7
	}
}
