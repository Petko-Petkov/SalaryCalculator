namespace NetSalaryContracts
{
    using System.Threading.Tasks;
    using TaxableModels;

    public interface INetSalaryCalculator
    {
        Task<Salary> CalculateNetAmount(Salary salary);
    }
}
