using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ObjectBussiness
{
    public class PetroleumBusinessDBContext : DbContext
    {
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ResultCandidate> ResultCandidates { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Decentralization> Decentralizations { get; set; }
        public DbSet<ExamRegister> ExamRegister { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Elect> Elects { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<NewsCategory> NewsCategories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);
            IConfigurationRoot configurationRoot = builder.Build();
            optionsBuilder.UseSqlServer(configurationRoot.GetConnectionString("MyConnection"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Add data table Role
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleID = 1, RoleName = "Admin" },
                new Role { RoleID = 2, RoleName = "Candidate" });

            //Add data table Elect
            modelBuilder.Entity<Elect>().HasData(
                new Elect { ElectID = 1, Status = true },
                new Elect { ElectID = 2, Status = false });

            // Add News Category
            modelBuilder.Entity<NewsCategory>().HasData(
               new NewsCategory { CategoryID = 1, CategoryName = "Gasoline Prices" },
               new NewsCategory { CategoryID = 2, CategoryName = "Recruitment Jobs" });

            //Add data table Exam
            modelBuilder.Entity<Exam>().HasData(
                new Exam
                {
                    ExamID = 1,
                    DateCreateTest = new DateTime(2024, 1, 14),
                    ExamName = "Admin(Not select)",
                    Status = "Start",
                    TimeBegin = new DateTime(2024, 1, 14),
                    TimeEnd = new DateTime(2024, 1, 14)
                });

            //Add data table ExamRegister
            modelBuilder.Entity<ExamRegister>().HasData(
                new ExamRegister
                {
                    ExamRegisterID = 1,
                    Age = 17,
                    BirthDay = new DateTime(2024, 1, 14),
                    CandidateName = "Admin",
                    City = "Đà Nẵng",
                    Country = "Việt Nam",
                    Email = "admin@gmail.com",
                    Gender = true,
                    Phone = "0911040107",
                    PlaceOfBirth = "Đà Nẵng",
                    ResidentialAddress = "22 Nguyễn Thức Tự,Hoà Hải,Ngũ Hàng Sơn,Đà Nẵng"
                });

            //Add data table Account
            modelBuilder.Entity<Account>().HasData(
                new Account { AccountID = 1, ExamID = 1, ExamRegisterID = 1, Password = "Admin@123.cntt" });
        }
    }
}