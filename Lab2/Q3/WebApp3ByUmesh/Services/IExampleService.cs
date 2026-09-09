namespace WebApp3ByUmesh.Services
{
    public interface IExampleService
    {
        Guid GetOperationId();
    }

    // Marker Interfaces
    public interface ITransientService : IExampleService { }
    public interface IScopedService : IExampleService { }
    public interface ISingletonService : IExampleService { }

    // Implement all interfaces
    public class ExampleService : ITransientService, IScopedService, ISingletonService
    {
        private readonly Guid _operationId = Guid.NewGuid();

        public Guid GetOperationId() => _operationId;
    }
}