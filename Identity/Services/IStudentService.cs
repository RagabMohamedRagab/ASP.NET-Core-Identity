using Identity.DAL.ViewModel;
using System.Collections.Generic;

namespace Identity.Services {
    public interface IStudentService {
        int Create(StudentVM student);
        int Create(LevelVM level);
        int Create(GenderVM genderVM );
        IEnumerable<LevelVM> GetAllLevels();
        IEnumerable<StudentVM> GetAllStudents();

    }
}
