using HRM.Domain;

namespace HRM.Applicatin
{
    public interface IPaySlipRepository
    {
        Task<PaySlip> AddPaySlipAsync(PaySlip paySlip);  
        Task<PaySlip> UpdatePaySlipAsync(int paySlipId, PaySlip paySlip);
        Task<bool> DeletePaySlipAsync(int paySlipId);
        Task<PaySlip> PaySlipGetDataAsync(int id);
        //Task<List<PaySlip>> GetPaySlipByName(string name);
        Task<IEnumerable<PaySlip>> PaySlipGetAllDataAsync();
    }
}