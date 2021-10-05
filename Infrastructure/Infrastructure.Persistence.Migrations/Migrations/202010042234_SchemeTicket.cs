using FluentMigrator;

namespace Infrastructure.Persistence.Migrations
{
    [Migration(202010042234, "SchemeTicket")]
    public class _202010042234_SchemeTicket : Migration
    {
        public override void Down()
        {
            Delete.Table("ticket");
        }

        public override void Up()
        {
            Create.Table("ticket")
            .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("username").AsString().NotNullable()
            .WithColumn("insert_date").AsDateTime()
            .WithColumn("update_date").AsDateTime()
            .WithColumn("status").AsBoolean().WithDefaultValue(true);

            Execute.EmbeddedScript("202010042234_SchemeTicket.sql");
        }
    }
}
