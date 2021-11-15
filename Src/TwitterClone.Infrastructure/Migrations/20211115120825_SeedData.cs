using Microsoft.EntityFrameworkCore.Migrations;
using TwitterClone.Domain.Entities;

#nullable disable

namespace TwitterClone.Infrastructure.Migrations;

public partial class SeedData : Migration
{
    private static (User, Guid) MakeUser()
    {
        User user = new("Admin", "Admin", "admin@twitterclone.com", "password");

        return (user, Guid.Parse("41ec910b-e61a-4471-8d78-ded12db5d124"));
    }

    protected override void Up(MigrationBuilder migrationBuilder)
    {
        (User user, Guid id) = MakeUser();
        _ = migrationBuilder.InsertData(
            table: "User",
            columns: new[] { nameof(user.Id), nameof(user.Name), nameof(user.NickName), nameof(user.Email), nameof(user.Password), nameof(user.CreatedAt), nameof(user.UserCreateId), nameof(user.UpdatedAt), nameof(user.UserLastUpdateId), nameof(user.IsActive) },
            values: new object[] { id, user.Name, user.NickName, user.Email, user.Password, user.CreatedAt, id, user.UpdatedAt, id, true });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        (_, Guid id) = MakeUser();
        migrationBuilder.DeleteData(table: "User", keyColumn: "Id", keyValue: id);
    }

}
