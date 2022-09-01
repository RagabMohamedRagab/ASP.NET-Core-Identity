using Identity.DAL.ViewModel;
using System.Collections.Generic;

namespace Identity.Services {
    public interface IStudentService {
        int Create(StudentVM student);
        IEnumerable<LeveVM> GetAllLevels();
        IEnumerable<StudentVM> GetAllStudents();

    }
}
