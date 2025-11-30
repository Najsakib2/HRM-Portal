using HRM.Applicatin;
using HRM.Domain;
using Microsoft.EntityFrameworkCore;


namespace HRM.Infrastructure
{
    public class PaySlipRepository(AppDbContext dbContext) : IPaySlipRepository
    {
        public async Task<PaySlip> AddPaySlipAsync(PaySlip paySlip)
        {
            dbContext.PaySlip.Add(paySlip);
            await dbContext.SaveChangesAsync();
            return paySlip;
        }

        public async Task<PaySlip> UpdatePaySlipAsync(int id, PaySlip paySlip)
        {
            var updatePaySlip = await dbContext.PaySlip.FirstOrDefaultAsync(x => x.ID == id);
            if (updatePaySlip is not null)
            {
                updatePaySlip.UserID = paySlip.UserID;
                updatePaySlip.BasicSalary = paySlip.BasicSalary;
                updatePaySlip.HousingAllowance = paySlip.HousingAllowance;
                updatePaySlip.TransportAllowance = paySlip.TransportAllowance;
                updatePaySlip.OtherAllowances = paySlip.OtherAllowances;
                updatePaySlip.TotalSalary = paySlip.TotalSalary;
                updatePaySlip.IsActive = paySlip.IsActive;
                updatePaySlip.EntryUseID = paySlip.EntryUseID;
                updatePaySlip.EntryDate = paySlip.EntryDate;
                updatePaySlip.UpdateUserID = paySlip.UpdateUserID;
                updatePaySlip.UpdateDate = paySlip.UpdateDate;
                await dbContext.SaveChangesAsync();
                return updatePaySlip;
            }
            return paySlip;
        }

        public async Task<bool> DeletePaySlipAsync(int id)
        {
            var deletePaySlip = await dbContext.PaySlip.FirstOrDefaultAsync(x => x.ID == id);

            if (deletePaySlip is not null)
            {
                dbContext.PaySlip.Remove(deletePaySlip);
                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<PaySlip> PaySlipGetDataAsync(int id)
        {
            return await dbContext.PaySlip.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IEnumerable<PaySlip>> PaySlipGetAllDataAsync()
        {
            return await dbContext.PaySlip.ToListAsync();
        }

        //public async Task<List<PaySlip>> GetPaySlipByName(string name)
        //{
        //    return await dbContext.PaySlip.Where(x => x.HolidayName == name).ToListAsync();
        //}
    }
}
