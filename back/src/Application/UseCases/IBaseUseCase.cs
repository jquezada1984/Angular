namespace Application.UseCases;

public interface IBaseUseCase<TRequest, TResponse>
{
    Task<TResponse> ExecuteAsync(TRequest request);
} 