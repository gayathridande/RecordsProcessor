namespace RecordProcessor.Application.Validators
{
    public interface IValidator<T>
    {
        ValidationResult IsValid(T arg);
    }
}