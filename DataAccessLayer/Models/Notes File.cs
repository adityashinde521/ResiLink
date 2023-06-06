using YourNamespace.DataAccess;
using YourNamespace.Models;

Act as a senior asp .net core dveloper and the above the above mentioned layers Help me implement the user management Module 
in the realworld Property Management project which is online platform for rental and buying properties. Use of Identity.EntityFrameworkCore
is necessary , Hierarchy must be 3 tier. The module must contain Registration,  Authentication, and Authorization User Roles as - 
Admin only one to manage platform, Tenant and Property Manager. Before geeting into code lets start with the files and folder hirerachy
that would be required for the module list all files and thier description



- ProjectName
  - ProjectName.API (Presentation Layer)
    - Controllers
      - AccountController.cs
      - ...
    - Startup.cs
    - Program.cs
    - appsettings.json
  - ProjectName.Business (Business Logic Layer)
    - Models
      - User.cs
      - ...
    - Services
      - UserService.cs
      - ...
  - ProjectName.Data (Data Access Layer)
    - Context
      - ApplicationDbContext.cs
      - ...
    - Repositories
      - UserRepository.cs
      - ...
  - ProjectName.Tests (Unit Tests)
    - Controllers
      - AccountControllerTests.cs
      - ...
    - Services
      - UserServiceTests.cs
      - ...
  - ProjectName.sln (Solution File)



/*
    CODE 
*/

	

User.cs:
```csharp
using Microsoft.AspNetCore.Identity;

namespace YourNamespace.Models
{
	public class User : IdentityUser
	{
		// Custom properties for User entity
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Gender { get; set; }

		// Navigation property for UserRole
		public virtual UserRole UserRole { get; set; }
	}
}
```

UserRole.cs:
```csharp
using Microsoft.AspNetCore.Identity;

namespace YourNamespace.Models
{
	public class UserRole : IdentityRole
	{
		// No additional properties needed for UserRole entity
	}
}
```

UserMgtDbContext.cs:
```csharp
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YourNamespace.Models;

namespace YourNamespace.DataAccess
{
	public class UserMgtDbContext : IdentityDbContext<User, UserRole, string>
	{
		public UserMgtDbContext(DbContextOptions<UserMgtDbContext> options)
			: base(options)
		{
		}

		// No additional DbSet properties needed, as User and UserRole are already defined in the base class

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Configure any additional entity configurations or relationships here
		}
	}
}
```

In this updated structure:
- The User entity is defined in the User.cs file and includes custom properties like FirstName, LastName, and Gender. It also has a navigation property UserRole, which represents the role assigned to the user.
- The UserRole entity is defined in the UserRole.cs file. Since no additional properties are required for UserRole, it extends the IdentityRole class directly.
-The UserMgtDbContext class is also updated, inheriting from IdentityDbContext with User as the user type and UserRole as the role type. No additional DbSet properties are needed because User and UserRole are already defined in the base class.

Remember to replace "YourNamespace" with the actual namespace used in your project and make any necessary adjustments based on your specific requirements.