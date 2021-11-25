using System;
using Foundation;
using ObjCRuntime;

namespace IPificationSDK
{


    // @interface AuthorizationRequest : NSObject
    [BaseType(typeof(NSObject))]
    [Protocol]
    interface AuthorizationRequest
    {
    }

    // @interface Builder : NSObject
    [BaseType(typeof(NSObject), Name = "_TtCC14IPificationSDK20AuthorizationRequest7Builder")]
    interface Builder
    {
        // -(void)setScopeWithValue:(NSString * _Nonnull)value;
        [Export("setScopeWithValue:")]
        void SetScopeWithValue(string value);

        // -(void)setStateWithValue:(NSString * _Nonnull)value;
        [Export("setStateWithValue:")]
        void SetStateWithValue(string value);

        // -(void)addQueryParamWithKey:(NSString * _Nonnull)key value:(NSString * _Nonnull)value;
        [Export("addQueryParamWithKey:value:")]
        void AddQueryParamWithKey(string key, string value);

        // -(void)setConnectTimeoutWithValue:(NSTimeInterval)value;
        [Export("setConnectTimeoutWithValue:")]
        void SetConnectTimeoutWithValue(double value);

        // -(void)setReadTimeoutWithValue:(NSTimeInterval)value;
        [Export("setReadTimeoutWithValue:")]
        void SetReadTimeoutWithValue(double value);

        // -(AuthorizationRequest * _Nonnull)build __attribute__((warn_unused_result("")));
        [Export("build")]
        AuthorizationRequest Build { get; }
    }

    // @interface AuthorizationResponse : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface AuthorizationResponse
    {
        // -(NSString * _Nonnull)getPlainResponse __attribute__((warn_unused_result("")));
        [Export("getPlainResponse")]
        string PlainResponse { get; }

        // -(NSString * _Nullable)getCode __attribute__((warn_unused_result("")));
        [NullAllowed, Export("getCode")]
        string Code { get; }

        // -(NSString * _Nullable)getState __attribute__((warn_unused_result("")));
        [NullAllowed, Export("getState")]
        string State { get; }

        // -(NSString * _Nonnull)getError __attribute__((warn_unused_result("")));
        [Export("getError")]
        string Error { get; }

        // -(BOOL)isAvailable __attribute__((warn_unused_result("")));
        [Export("isAvailable")]
        bool IsAvailable { get; }
    }

    // @interface AuthorizationService : NSObject
    [BaseType(typeof(NSObject))]
    interface AuthorizationService
    {
        // @property (copy, nonatomic) void (^ _Nullable)(AuthorizationResponse * _Nonnull) callbackSuccess;
        [NullAllowed, Export("callbackSuccess", ArgumentSemantic.Copy)]
        Action<AuthorizationResponse> CallbackSuccess { get; set; }

        // @property (copy, nonatomic) void (^ _Nullable)(CellularException * _Nonnull) callbackFailed;
        [NullAllowed, Export("callbackFailed", ArgumentSemantic.Copy)]
        Action<CellularException> CallbackFailed { get; set; }

        // @property (copy, nonatomic) void (^ _Nullable)(NSString * _Nonnull) callbackLog;
        [NullAllowed, Export("callbackLog", ArgumentSemantic.Copy)]
        Action<NSString> CallbackLog { get; set; }


        // -(void)doAuthorization:(AuthorizationRequest * _Nullable)authRequest;
        [Export("doAuthorization:")]
        void DoAuthorization([NullAllowed] AuthorizationRequest authRequest);
    }

    // @interface CellularException : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface CellularException
    {
        // -(NSString * _Nonnull)getErrorMessage __attribute__((warn_unused_result("")));
        [Export("getErrorMessage")]
        string ErrorMessage { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nonnull localizedDescription;
        [Export("localizedDescription")]
        string LocalizedDescription { get; }
    }



    // @interface CoverageRequest : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface CoverageRequest
    {
    }

    // @interface CoverageResponse : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface CoverageResponse
    {
        // -(NSString * _Nullable)getOperatorCode __attribute__((warn_unused_result("")));
        [NullAllowed, Export("getOperatorCode")]
        string OperatorCode { get; }

        // -(BOOL)isAvailable __attribute__((warn_unused_result("")));
        [Export("isAvailable")]
        bool IsAvailable { get; }

        // -(NSString * _Nonnull)getError __attribute__((warn_unused_result("")));
        [Export("getError")]
        string Error { get; }

        // -(NSString * _Nonnull)getPlainResponse __attribute__((warn_unused_result("")));
        [Export("getPlainResponse")]
        string PlainResponse { get; }

        // -(NSString * _Nullable)getCode __attribute__((warn_unused_result("")));
        [NullAllowed, Export("getCode")]
        string Code { get; }

        // -(NSString * _Nullable)getState __attribute__((warn_unused_result("")));
        [NullAllowed, Export("getState")]
        string State { get; }
    }

    // @interface CoverageService : NSObject <CoverageCallback>
    [BaseType(typeof(NSObject))]
    interface CoverageService
    {
        [NullAllowed, Export("callbackSuccess", ArgumentSemantic.Copy)]
        Action<CoverageResponse> CallbackSuccess { get; set; }

        // @property (copy, nonatomic) void (^ _Nullable)(CellularException * _Nonnull) callbackFailed;
        [NullAllowed, Export("callbackFailed", ArgumentSemantic.Copy)]
        Action<CellularException> CallbackFailed { get; set; }

        // @property (copy, nonatomic) void (^ _Nullable)(NSString * _Nonnull) callbackLog;
        [NullAllowed, Export("callbackLog", ArgumentSemantic.Copy)]
        Action<NSString> CallbackLog { get; set; }


        // -(void) checkCoverage;
        [Export("checkCoverage")]
        void CheckCoverage();

        // -(void)checkCoverage:(CoverageRequest * _Nullable)customCoverageRequest;
        [Export("checkCoverage:")]
        void CheckCoverage([NullAllowed] CoverageRequest customCoverageRequest);

        // -(void)checkCoverageWithPhoneNumber:(NSString * _Nonnull)phone;
        [Export("checkCoverageWithPhoneNumber:")]
        void CheckCoverageWithPhoneNumber(string phone);

        // -(void)checkCoverageWithPhoneNumber:(NSString * _Nonnull)phone :(CoverageRequest * _Nullable)customCoverageRequest;
        [Export("checkCoverageWithPhoneNumber::")]
        void CheckCoverageWithPhoneNumber(string phone, [NullAllowed] CoverageRequest customCoverageRequest);
    }

    [BaseType(typeof(NSObject))]
    interface IPConfiguration
    {
        // @property (readonly, nonatomic, strong, class) IPConfiguration * _Nonnull shared;
        [Static]
        [Export("shared", ArgumentSemantic.Strong)]
        IPConfiguration Shared { get; }

        // @property (copy, nonatomic) NSString * _Nonnull coverageServiceEndpoint;
        [Export("coverageServiceEndpoint")]
        string CoverageServiceEndpoint { get; set; }

        // @property (copy, nonatomic) NSString * _Nonnull authServiceEndpoint;
        [Export("authServiceEndpoint")]
        string AuthServiceEndpoint { get; set; }

        // @property (copy, nonatomic) NSString * _Nonnull redirectUri;
        [Export("redirectUri")]
        string RedirectUri { get; set; }

        // @property (copy, nonatomic) NSString * _Nonnull clientId;
        [Export("clientId")]
        string ClientId { get; set; }


    }
}
