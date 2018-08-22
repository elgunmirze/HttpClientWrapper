using System;
using System.Net.Http;

namespace HttpClientWrapper.Exceptions
{
    public static class HttpClientExceptions
    {
        public static Exception ThrowException(HttpResponseMessage response)
        {
            var exception = new Exception($"Server returned an error. StatusCode : {response.StatusCode}");

            exception.Data.Add("StatusCode", response.StatusCode);

            throw exception;
        }
    }
}