using Microsoft.EntityFrameworkCore;
using SpendingManagement.Data.Entities;
using SpendingManagement.Repository.Expenditures;
using SpendingManagement.Share.ApiResults;
using SpendingManagement.Share.NameTypeOfCategoryGenerator;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace SpendingManagement.Application.Expenditures
{
    public class ExpenditureService : IExpenditureService
    {
        private readonly IExpenditureRepository _expenditureRepository;
        public ExpenditureService(IExpenditureRepository expenditureRepository)
        {
            _expenditureRepository = expenditureRepository;
        }

        public async Task<ApiResultBase<string>> CreateExpenditure(CreateExpenditureRequest request)
        {
            var expenditure = new Expenditure()
            {
                AppUserId = request.UserId,
                CategoryId = request.CategoryId,
                Cost = request.Cost,
                Note = request.Note,
                DateCreate = DateTime.ParseExact(request.CreationDate, "dd-MM-yyyy", CultureInfo.GetCultureInfo("tr-TR")),
                Id = Guid.NewGuid()
            };
            await _expenditureRepository.AddExpenditure(expenditure);
            return new ApiSuccessResult<string>(message: "Thêm thành công");
        }

        public async Task<ApiResultBase<List<ExpenditureResponse>>> GetExpendituresOnDate(DateTime? dateTime, Guid id)
        {
            if (dateTime == null)
            {
                dateTime = DateTime.Now;
            }
            var result = _expenditureRepository.GetAllExpenditures()
                .Where(x => x.DateCreate.ToString("dd-MM-yyyy").Equals(dateTime.Value.ToString("dd-MM-yyyy")) && x.AppUserId == id)
                .Include(x => x.Category)
                .Select(x => new ExpenditureResponse()
                {
                    Id = x.Id,
                    CategoryType = x.Category.TypeOfCategory,
                    CategoryTypeName = TypeCategoryNameGenerator.GenerateName(x.Category.TypeOfCategory),
                    Cost = x.Cost.ToString(),
                    Date = x.DateCreate.ToString("dd-MM-yyyy"),
                    Note = x.Note,
                    ImageIcon = x.Category.LinkIcon,
                    CategoryName = x.Category.Name,
                    CategoryId = x.CategoryId
                }
                ).ToList();
            return new ApiSuccessResult<List<ExpenditureResponse>>(resultObj: result);
        }

        public async Task<ApiResultBase<string>> UpdateExpenditure(UpdateExpenditureRequest request)
        {
            var expenditure = _expenditureRepository.GetExpenditureById(request.Id);
            if (expenditure == null)
            {
                return new ApiErrorResult<string>(message: "Không cập nhật thành công");
            }
            expenditure.CategoryId = request.CategoryId;
            expenditure.DateCreate = DateTime.ParseExact(request.DateCreation, "dd-MM-yyyy", CultureInfo.GetCultureInfo("tr-TR"));
            expenditure.Note = request.Note;
            expenditure.Cost = request.Cost;
            await _expenditureRepository.UpdateExpenditure(expenditure);
            return new ApiSuccessResult<string>(message: "Cập nhật thành công");
        }
        
        public async Task<ApiResultBase<string>> DeleteExpenditure(Guid id)
        {
            await _expenditureRepository.DeleteExpenditure(id);
            return new ApiSuccessResult<string>(message: "Xóa thành công");
        }
    }
}
