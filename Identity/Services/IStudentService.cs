using Identity.DAL.ViewModel;
using System.Collections.Generic;

namespace Identity.Services {
    public interface IStudentService {
        int Create(StudentVM student);
        StudentVM Find(int Id);
        int Update(int Id,StudentVM student);  
        IEnumerable<StudentVM> GetAllStudents();
        int Remove(int Id);
        int Create(LevelVM level);
        int Create(GenderVM genderVM );
        IEnumerable<LevelVM> GetAllLevels();
        IEnumerable<GenderVM> GetAllGenders();
        GenderVM FindbyId(int Id);
        int UpdateGender(int Id, GenderVM gender);
        int Delete(int Id);
        int DeleteLevel(int Id);
        int UpdateLevel(int Id, LevelVM level);
        LevelVM FindLevel(int id);
     

    }
}
