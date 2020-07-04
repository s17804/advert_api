#nullable enable
using System;

namespace AdvertApi.Exceptions
{
    public class BadLoginOrPasswordException : Exception
    {
        public BadLoginOrPasswordException(string? message) : base(message)
        {
        }
    }
}