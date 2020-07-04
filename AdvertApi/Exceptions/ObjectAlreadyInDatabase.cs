#nullable enable
using System;

namespace AdvertApi.Exceptions
{
    public class ObjectAlreadyInDatabaseException : Exception
    {
        public ObjectAlreadyInDatabaseException(string? message) : base(message)
        {
        }
    }
}