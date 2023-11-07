using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SignalR101.Helpers;
using SignalR101.Models;
using SignalR101.Repository.Abstract;

namespace SignalR101.Repository.Concrete.EfCore
{
	public class EfGenericRepository<T>:IGenericRepository<T> where T:class
	{
        protected readonly ApplicationDbContext context;


		public EfGenericRepository(ApplicationDbContext _context)
		{
            context = _context;
		}


        public void Add(T entity)
        {
            try
            { 
                context.Add(entity);
                ResponseHelper.Success(entity);
                Save();
            }
            catch (Exception ex)
            {
                ResponseHelper.Fail<T>(ex.ToString());
            }
           
        }


        public async Task<ResponseModel<T>> AddAsync(T entity)
        {
            try
            {
                await context.AddAsync(entity);
                Save();
                return ResponseHelper.Success(entity);
                
            }
            catch (Exception ex)
            {
                return ResponseHelper.Fail<T>(ex.ToString());
            }
        }


        public ResponseModel<T> Delete(T entity)
        {
            try
            {
                context.Remove(entity);
                Save();
                return ResponseHelper.Success(entity);

            }
            catch (Exception ex)
            {
                return ResponseHelper.Fail<T>(ex.ToString());
            }          
        }


        public ResponseModel<T> DeleteAll(List<T> values)
        {
            try
            {
                context.RemoveRange(values);
                Save();
                return ResponseHelper.Success(values[0]);

            }
            catch (Exception ex)
            {
                return ResponseHelper.Fail<T>(ex.ToString());
            }

        }


        public void Edit(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }


        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate);
        }


        public ResponseModel<T> Get(string id)
        {
            try
            {
              var data = context.Set<T>().Find(id);
                Save();
                return ResponseHelper.Success(data!);
            }
            catch (Exception ex)
            {
                return ResponseHelper.Fail<T>(ex.ToString());
            }
          
        }


        public IQueryable<T> GetAll()
        {
            return context.Set<T>();
            
        }

   
        public void Save()
        {
            context.SaveChanges();
        }

        
    }
}

