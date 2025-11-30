using HRM.Applicatin;
using HRM.Domain;
using Microsoft.EntityFrameworkCore;

namespace HRM.Infrastructure
{
    public class BankInformationRepository(AppDbContext dbContext) : IBankInformationRepository
    {
        public async Task<BankInformation> AddBankInformationAsync(BankInformation bankInformation)
        {
            dbContext.BankInformation.Add(bankInformation);
            await dbContext.SaveChangesAsync();
            return bankInformation;
        }

        public async Task<BankInformation> UpdateBankInformationAsync(int id, BankInformation bankInformation)
        {
            var updateBankInformation = await dbContext.BankInformation.FirstOrDefaultAsync(x => x.ID == id);
            if (updateBankInformation is not null)
            {
                updateBankInformation.UserID = bankInformation.UserID;
                updateBankInformation.BankName = bankInformation.BankName;
                updateBankInformation.AccountNumber = bankInformation.AccountNumber;
                updateBankInformation.BranceName = bankInformation.BranceName;
                updateBankInformation.EntryUseID = bankInformation.EntryUseID;
                updateBankInformation.EntryDate = bankInformation.EntryDate;
                updateBankInformation.UpdateUserID = bankInformation.UpdateUserID;
                updateBankInformation.UpdateDate = bankInformation.UpdateDate;
                await dbContext.SaveChangesAsync();
                return updateBankInformation;
            }
            return bankInformation;
        }

        public async Task<bool> DeleteBankInformationAsync(int id)
        {
            var deleteBankInformation = await dbContext.BankInformation.FirstOrDefaultAsync(x => x.ID == id);

            if (deleteBankInformation is not null)
            {
                dbContext.BankInformation.Remove(deleteBankInformation);
                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<BankInformation> BankInformationGetDataAsync(int id)
        {
            return await dbContext.BankInformation.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IEnumerable<BankInformation>> BankInformationGetAllDataAsync()
        {
            return await dbContext.BankInformation.ToListAsync();
        }

        public async Task<List<BankInformation>> GetBankInformationByName(string name)
        {
            return await dbContext.BankInformation.Where(x => x.BranceName == name).ToListAsync();
        }
    }
}
