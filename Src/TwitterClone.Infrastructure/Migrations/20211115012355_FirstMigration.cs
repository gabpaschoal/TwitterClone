using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwitterClone.Infrastructure.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "char(100)", maxLength: 100, nullable: false),
                    NickName = table.Column<string>(type: "char(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "char(70)", maxLength: 70, nullable: false),
                    Password = table.Column<string>(type: "char(70)", maxLength: 70, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserLastUpdateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserDeletedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_User_UserCreateId",
                        column: x => x.UserCreateId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_User_UserDeletedId",
                        column: x => x.UserDeletedId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_User_UserLastUpdateId",
                        column: x => x.UserLastUpdateId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tweet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TweetMessage = table.Column<string>(type: "char(240)", maxLength: 240, nullable: false),
                    TweetReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ETweetType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserLastUpdateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserDeletedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tweet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tweet_Tweet_TweetReferenceId",
                        column: x => x.TweetReferenceId,
                        principalTable: "Tweet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tweet_User_UserCreateId",
                        column: x => x.UserCreateId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tweet_User_UserDeletedId",
                        column: x => x.UserDeletedId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tweet_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tweet_User_UserLastUpdateId",
                        column: x => x.UserLastUpdateId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserBlock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserBlockedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserLastUpdateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserDeletedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBlock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBlock_User_UserBlockedId",
                        column: x => x.UserBlockedId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserBlock_User_UserCreateId",
                        column: x => x.UserCreateId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserBlock_User_UserDeletedId",
                        column: x => x.UserDeletedId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserBlock_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserBlock_User_UserLastUpdateId",
                        column: x => x.UserLastUpdateId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserFollow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserFollowedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserLastUpdateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserDeletedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFollow_User_UserCreateId",
                        column: x => x.UserCreateId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserFollow_User_UserDeletedId",
                        column: x => x.UserDeletedId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserFollow_User_UserFollowedId",
                        column: x => x.UserFollowedId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserFollow_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserFollow_User_UserLastUpdateId",
                        column: x => x.UserLastUpdateId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TweetLike",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TweetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserLastUpdateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserDeletedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TweetLike_Tweet_TweetId",
                        column: x => x.TweetId,
                        principalTable: "Tweet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TweetLike_User_UserCreateId",
                        column: x => x.UserCreateId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TweetLike_User_UserDeletedId",
                        column: x => x.UserDeletedId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TweetLike_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TweetLike_User_UserLastUpdateId",
                        column: x => x.UserLastUpdateId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tweet_TweetReferenceId",
                table: "Tweet",
                column: "TweetReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tweet_UserCreateId",
                table: "Tweet",
                column: "UserCreateId");

            migrationBuilder.CreateIndex(
                name: "IX_Tweet_UserDeletedId",
                table: "Tweet",
                column: "UserDeletedId");

            migrationBuilder.CreateIndex(
                name: "IX_Tweet_UserId",
                table: "Tweet",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tweet_UserLastUpdateId",
                table: "Tweet",
                column: "UserLastUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetLike_TweetId",
                table: "TweetLike",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetLike_UserCreateId",
                table: "TweetLike",
                column: "UserCreateId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetLike_UserDeletedId",
                table: "TweetLike",
                column: "UserDeletedId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetLike_UserId",
                table: "TweetLike",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetLike_UserLastUpdateId",
                table: "TweetLike",
                column: "UserLastUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserCreateId",
                table: "User",
                column: "UserCreateId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserDeletedId",
                table: "User",
                column: "UserDeletedId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserLastUpdateId",
                table: "User",
                column: "UserLastUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlock_UserBlockedId",
                table: "UserBlock",
                column: "UserBlockedId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlock_UserCreateId",
                table: "UserBlock",
                column: "UserCreateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlock_UserDeletedId",
                table: "UserBlock",
                column: "UserDeletedId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlock_UserId",
                table: "UserBlock",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlock_UserLastUpdateId",
                table: "UserBlock",
                column: "UserLastUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollow_UserCreateId",
                table: "UserFollow",
                column: "UserCreateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollow_UserDeletedId",
                table: "UserFollow",
                column: "UserDeletedId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollow_UserFollowedId",
                table: "UserFollow",
                column: "UserFollowedId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollow_UserId",
                table: "UserFollow",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollow_UserLastUpdateId",
                table: "UserFollow",
                column: "UserLastUpdateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TweetLike");

            migrationBuilder.DropTable(
                name: "UserBlock");

            migrationBuilder.DropTable(
                name: "UserFollow");

            migrationBuilder.DropTable(
                name: "Tweet");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
