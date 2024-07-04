using EasyBook.Domain.Shared;

namespace EasyBook.Domain.Errors;

public static class InfraErrors
{
    public static class Pulsar
    {
        public static readonly Error ConnectionIssue = new(
            "Pulsar.ConnectionIssue",
            "Issue connecting with Pulsar");

    }
}