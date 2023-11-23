using BusinessBuddyApp.Entities;

namespace BusinessBuddyApp
{
    public class Seeder
    {
        private readonly BusinessBudyDbContext _dbContext;
        public Seeder(BusinessBudyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            { 
                if(!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
            }
        }

        public List<Role> GetRoles() 
        {
            var roles = new List<Role>()
            {
                new Role
                {
                    Name = "Client"
                },
                new Role
                {
                    Name = "User"
                },
                new Role
                {
                    Name = "Admin"
                }
            };

            return roles;
        }
    }
}
