using AutoMapper;
using lab.WebApi19Sample.EntityModels;
using lab.WebApi19Sample.Helpers;
using lab.WebApi19Sample.Repository;
using lab.WebApi19Sample.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lab.WebApi19Sample.Manager
{
    public class StudentManager : IStudentManager
    {
        private readonly IStudentRepository _iStudentRepository;
        private readonly IMapper _iMapper;

        public StudentManager(IStudentRepository iStudentRepository, IMapper iMapper)
        {
            _iStudentRepository = iStudentRepository;
            _iMapper = iMapper;
        }

        public async Task<StudentViewModel> GetStudentAsync(string id)
        {
            var data = await _iStudentRepository.GetStudentAsync(id);
            return _iMapper.Map<Student, StudentViewModel>(data);
        }

        public async Task<IEnumerable<StudentViewModel>> GetStudentsAsync()
        {
            var data = await _iStudentRepository.GetStudentsAsync();
            return _iMapper.Map<IEnumerable<Student>, IEnumerable<StudentViewModel>>(data);
        }

        public async Task<int> InsertOrUpdatetStudentAsync(StudentViewModel model)
        {
            var data = _iMapper.Map<StudentViewModel, Student>(model);
            return await _iStudentRepository.InsertOrUpdatetStudentAsync(data);
        }

        public async Task<ResultApi> InsertStudentAsync(StudentViewModel model)
        {
            try
            {
                var data = _iMapper.Map<StudentViewModel, Student>(model);

                var saveChange = await _iStudentRepository.InsertStudentAsync(data);

                if (saveChange > 0)
                {
                    return ResultApi.Ok(MessageHelper.Save);
                }
                else
                {
                    return ResultApi.Fail(MessageHelper.SaveFail);
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public async Task<ResultApi> UpdateStudentAsync(StudentViewModel model)
        {
            var data = _iMapper.Map<StudentViewModel, Student>(model);

            var saveChange = await _iStudentRepository.UpdateStudentAsync(data);

            if (saveChange > 0)
            {
                return ResultApi.Ok(MessageHelper.Update);
            }
            else
            {
                return ResultApi.Fail(MessageHelper.UpdateFail);
            }
        }

        public async Task<ResultApi> DeleteStudentAsync(string id)
        {
            var model = await GetStudentAsync(id);
            if (model != null)
            {
                var data = _iMapper.Map<StudentViewModel, Student>(model);

                var saveChange = await _iStudentRepository.DeleteStudentAsync(data);

                if (saveChange > 0)
                {
                    return ResultApi.Ok(MessageHelper.Delete);
                }
                else
                {
                    return ResultApi.Fail(MessageHelper.DeleteFail);
                }
            }
            else
            {
                return ResultApi.Fail(MessageHelper.DeleteFail);
            }

        }
    }

    public interface IStudentManager
    {
        Task<StudentViewModel> GetStudentAsync(string id);
        Task<IEnumerable<StudentViewModel>> GetStudentsAsync();
        Task<ResultApi> InsertStudentAsync(StudentViewModel viewModel);
        Task<ResultApi> UpdateStudentAsync(StudentViewModel viewModel);
        Task<ResultApi> DeleteStudentAsync(string id);
    }
}
