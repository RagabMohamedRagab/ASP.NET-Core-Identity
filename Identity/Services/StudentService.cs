using AutoMapper;
using Identity.DAL.Context;
using Identity.DAL.Domain_Modes;
using Identity.DAL.ViewModel;
using System.Collections.Generic;

namespace Identity.Services {
    public class StudentService : IStudentService {
        private readonly AppDbContext _appDb;
     
        public StudentService(AppDbContext appDb, IMapper mapper)
        {
            _appDb = appDb;
        }
        public int Create(StudentVM student)
        {
            if (string.IsNullOrEmpty(student.FullName) || string.IsNullOrEmpty(student.Level) || student.Age <= 0 || string.IsNullOrEmpty(student.Gender))
            {
                return 0;
            }
            try
            {
                Student std = new Student()
                {
                    Name = student.FullName,
                };
                _appDb.Students.Add();
                return _appDb.SaveChanges();
            }
            catch (System.Exception)
            {

                return 0;
            }
        }

        public IEnumerable<LeveVM> GetAllLevels()
        {
            return null;
        }

        public IEnumerable<StudentVM> GetAllStudents()
        {
            return null;
        }
    }
}
