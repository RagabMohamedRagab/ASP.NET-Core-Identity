using AutoMapper;
using Identity.DAL.Context;
using Identity.DAL.Domain_Modes;
using Identity.DAL.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace Identity.Services {
    public class StudentService : IStudentService {
        private readonly AppDbContext _appDb;

        public StudentService(AppDbContext appDb, IMapper mapper)
        {
            _appDb = appDb;
        }
        #region Student
        public int Remove(int Id)
        {
            if (Id <= 0)
            {
                return 0;
            }
            var obj = _appDb.Students.Find(Id);
            if(obj != null)
            {
                _appDb.Students.Remove(obj);
                return _appDb.SaveChanges();
            }
            return 0;
        }
        public StudentVM Find(int Id)
        {
            if (Id <= 0)
            {
                return null;
            }
            var data = _appDb.Students.Find(Id);
            if (data == null)
            {
                return null;
            }
            return new StudentVM() { FullName = data.Name, Id = data.Id, Age = data.Age, ImgUrl = data.ImgUrl, Gender = data.GenderId.ToString(), Level = data.StudentId.ToString() };
        }


        public IEnumerable<StudentVM> GetAllStudents()
        {
            return (_appDb.Students.Select(b => new StudentVM()
            {
                Id = b.Id,
                FullName = b.Name,
                Age = b.Age,
                Gender = _appDb.Genders.FirstOrDefault(c => c.Id == b.GenderId).Name,
                Level = _appDb.Levels.FirstOrDefault(c => c.Id == b.StudentId).Name,
                ImgUrl = b.ImgUrl,
            }));
        }


        public int Create(StudentVM student)
        {
            if (string.IsNullOrEmpty(student.FullName) || string.IsNullOrEmpty(student.Level) || student.Age <= 0 || string.IsNullOrEmpty(student.Gender))
            {
                return 0;
            }
            try
            {
                int? leveliD = _appDb.Levels.SingleOrDefault(level => level.Name.ToLower() == student.Level.ToLower()).Id;
                if (leveliD == null || leveliD <= 0)
                {
                    leveliD = null;
                }
                int? GenderId = _appDb.Genders.SingleOrDefault(gender => gender.Name.ToLower() == student.Gender.ToLower()).Id;
                if (GenderId == null || GenderId <= 0)
                {
                    GenderId = null;
                }
                Student std = new Student()
                {
                    Name = student.FullName,
                    Age = student.Age,
                    StudentId = leveliD,
                    GenderId = GenderId,
                    ImgUrl = student.File.FileName,
                };
                _appDb.Students.Add(std);
                return _appDb.SaveChanges();
            }
            catch (System.Exception)
            {
                return 0;
            }
        }
        #endregion
        #region Level

        public int Create(LevelVM level)
        {
            if (string.IsNullOrEmpty(level.Name))
            {
                return 0;
            }
            try
            {
                Level entity = new Level()
                {
                    Name = level.Name,
                };
                _appDb.Levels.Add(entity);
                return _appDb.SaveChanges();
            }
            catch (System.Exception)
            {
                return 0;
            }
        }
        public IEnumerable<LevelVM> GetAllLevels()
        {
            return _appDb.Levels.Select(level => new LevelVM() { Name = level.Name });
        }


        #endregion
        #region Gender
        public int Create(GenderVM genderVM)
        {
            if (string.IsNullOrEmpty(genderVM.Name))
            {
                return 0;
            }
            try
            {
                Gender entity = new Gender()
                {
                    Name = genderVM.Name,
                };
                _appDb.Genders.Add(entity);
                return _appDb.SaveChanges();
            }
            catch (System.Exception)
            {
                return 0;
            }
        }
        public IEnumerable<GenderVM> GetAllGenders()
        {
            return _appDb.Genders.Select(b => new GenderVM() { Name = b.Name });
        }


        #endregion
    }
}
