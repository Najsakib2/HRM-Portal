using HRM.Domain;

namespace HRM.Applicatin
{
    public interface IBankInformationRepository
    {
        Task<BankInformation> AddBankInformationAsync(BankInformation bankInformation);
        Task<BankInformation> UpdateBankInformationAsync(int bankInformationId, BankInformation bankInformation);
        Task<bool> DeleteBankInformationAsync(int bankInformationId);
        Task<BankInformation> BankInformationGetDataAsync(int id);
        Task<List<BankInformation>> GetBankInformationByName(string name);
        Task<IEnumerable<BankInformation>> BankInformationGetAllDataAsync();
    }
}