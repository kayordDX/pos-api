using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace Kayord.Pos.Common.Extensions.Swagger;

public class CustomOperationsProcessor : IOperationProcessor
{
    public bool Process(OperationProcessorContext context)
    {
        string operationId = context.OperationDescription.Operation.OperationId;
        string text = AppDomain.CurrentDomain.FriendlyName.Replace(".", string.Empty);
        string oldValue = text + "Features";
        operationId = operationId.Replace(oldValue, string.Empty);
        operationId = operationId.Replace("KayordKitFeatures", string.Empty);
        operationId = operationId.Replace(text, string.Empty);
        operationId = operationId.Replace("KayordKit", string.Empty);
        operationId = operationId.Replace("Endpoint", string.Empty);
        context.OperationDescription.Operation.OperationId = operationId;
        return true;
    }
}