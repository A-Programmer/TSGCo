using Project.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Auth.Utilities;

namespace Project.Auth.DataLayer.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            
            var admin = new User("admin", "admin".GetSha256Hash(), true) { Id = Guid.NewGuid() };
            
            var adminFirstName = new UserClaim("given_name", "Kamran") { Id = Guid.NewGuid() };
            adminFirstName.SetUserId(admin.Id);
            
            var adminLastName = new UserClaim("family_name", "Sadin") { Id = Guid.NewGuid() };
            adminLastName.SetUserId(admin.Id);
            
            var adminRole = new UserClaim("role", "admin") { Id = Guid.NewGuid() };
            adminRole.SetUserId(admin.Id);

            var user = new User("user", "user".GetSha256Hash(), true) { Id = Guid.NewGuid() };
            
            var userFName = new UserClaim("given_name", "Mohsen") { Id = Guid.NewGuid() };
            userFName.SetUserId(admin.Id);
            
            var userLName = new UserClaim("family_name", "Safari") { Id = Guid.NewGuid() };
            userLName.SetUserId(admin.Id);
            
            var userRole = new UserClaim("role", "user");
            userRole.SetUserId(admin.Id);

            builder.HasData(
                admin,
                user
            );
        }
    }
}