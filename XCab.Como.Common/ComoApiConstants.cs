namespace xcab.como.common
{
    public class ComoApiConstants
    {
#if DEBUG
        public static string BaseComoUrl = "https://api.rc.como.systems";
#else
        public static string BaseComoUrl = "https://api.como.systems";
#endif
        public static string RcComoUrl = "https://api.rc.como.systems";
        public static string ExternalImageEndpoint = "/files/downloadexternalfile";
        public static string InternalImageEndpoint = "/files/downloadFiles";
        public static string InternalUserLoginEndpoint = "/api/internalUsers/login";
        public static string ApiTokenEndpoint = "/api/apiTokens/create/";
        public static string BookJobEndpoint = "/graphql/";
        public static string Username = "local\\testuser";
        public static string Password = "Password1234";
        public static string ApiUsername = "XCAB";
        //public static string ApiSessionId = "E69JrAm3dI6a04EDxl63kkzYsBwgBL1YtkrFdCmE2vwGHPbJvuUi%2BzfuUqSgbZUD5cZCsIt4xfIhUczOLyk6vfSbRldrNqWwNuXGknMSe9w%3D";
        public static string ApiSessionId = "wtUhuq%2BiKq0CUX%2F4Dpkze0sJ5ZpSD98mTdB13nMbJ%2BjRPkHXJSw%2Fa30LXi7sHzGSCZ0%2F%2Bb6SNlK0IeX6FzisXTwmq%2Bhv0f8YITeJpS4lObk%3D";
        //public static string ApiRefreshToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2Mjc1MzY3OTcsImV4cCI6MTYyODc0NjM5NywiaXNzIjoiQ29tbyIsImF1ZCI6IkNvbW8iLCJzdWIiOiJ0ZXN0dXNlciIsImp0aSI6IjE4NTQ1ZDdiLWUxYmQtNDU4Yi1iMDM5LTU0ZDdjYjgyOTEwZiIsInVzZXJJZCI6NTR9.f3YLdsS-COgk8gO-QXgUcpiRni5sQJ2lP5S_KkLYsXc";
        public static string ApiRefreshToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2NjI5NTc3NTcsImV4cCI6MTY2NDE2NzM1NywiaXNzIjoiQ29tbyIsImF1ZCI6IkNvbW8iLCJzdWIiOiJ0ZXN0dXNlciIsImp0aSI6IjdlODE4MWEwLWZkYjYtNGIxMy04ZDEyLWNlNTRlOTFlOTM0NSIsInVzZXJJZCI6Mn0.EqTFY9zeai-0yXkSkwprDlVU_DGfgxRPI1yAPb52TJ4";
        public static string ApiAccessToken = "GgXNAKsAhADFAOwAYADSAC//2hD8iCb8XHprklu92WjBp6gG2JWyRQ==";
    }
}
