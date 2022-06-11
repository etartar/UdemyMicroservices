using Dapper;
using FreeCourse.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQL"));
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discounts");
            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Models.Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discounts WHERE id=@Id", new
            {
                Id = id
            })).SingleOrDefault();

            if (discount == null)
                return Response<Models.Discount>.Fail("Discount not found.", 404);

            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discounts WHERE code=@Code AND userid=@UserId", new
            {
                Code = code,
                UserId = userId
            })).SingleOrDefault();

            if (discount == null)
                return Response<Models.Discount>.Fail("Discount not found.", 404);

            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discounts (userid,rate,code) VALUES(@UserId, @Rate, @Code)", discount);

            if (saveStatus > 0)
                return Response<NoContent>.Success(204);

            return Response<NoContent>.Fail("An error accured while adding.", 500);
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            var updateStatus = await _dbConnection.ExecuteAsync("UPDATE discounts SET userid=@UserId, code=@Code, rate=@Rate WHERE id=@Id", new
            {
                Id = discount.Id,
                UserId = discount.UserId,
                Code = discount.Code,
                Rate = discount.Rate
            });

            if (updateStatus > 0)
                return Response<NoContent>.Success(204);

            return Response<NoContent>.Fail("An error accured while updating.", 500);
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var deleteStatus = await _dbConnection.ExecuteAsync("DELETE FROM discounts WHERE id=@Id", new
            {
                Id = id
            });

            if (deleteStatus > 0)
                return Response<NoContent>.Success(204);

            return Response<NoContent>.Fail("An error accured while deleting.", 500);
        }
    }
}
