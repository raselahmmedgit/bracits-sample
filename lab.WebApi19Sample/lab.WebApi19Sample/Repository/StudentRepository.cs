using lab.WebApi19Sample.Data;
using lab.WebApi19Sample.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lab.WebApi19Sample.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;


        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Student> GetStudentAsync(string id)
        {
            return await _context.Students.SingleOrDefaultAsync(x => x.StudentId == id);
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<int> InsertOrUpdatetStudentAsync(Student model)
        {
            if (string.IsNullOrEmpty(model.StudentId))
            {
                model.StudentId = Guid.NewGuid().ToString();
                await _context.Students.AddAsync(model);
            }
            else
            {
                _context.Students.Update(model);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> InsertStudentAsync(Student model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.StudentId))
                {
                    model.StudentId = Guid.NewGuid().ToString();
                    await _context.Students.AddAsync(model);
                }
                return await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateStudentAsync(Student model)
        {
            if (!string.IsNullOrEmpty(model.StudentId))
            {
                _context.Students.Update(model);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteStudentAsync(Student model)
        {
            if (!string.IsNullOrEmpty(model.StudentId))
            {
                _context.Students.Update(model);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteStudentAsync(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var model = await GetStudentAsync(id);
                _context.Students.Update(model);
            }
            return await _context.SaveChangesAsync();
        }
    }

    public interface IStudentRepository
    {
        Task<Student> GetStudentAsync(string id);
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<int> InsertOrUpdatetStudentAsync(Student model);
        Task<int> InsertStudentAsync(Student model);
        Task<int> UpdateStudentAsync(Student model);
        Task<int> DeleteStudentAsync(Student model);
        Task<int> DeleteStudentAsync(string id);

    }
}
