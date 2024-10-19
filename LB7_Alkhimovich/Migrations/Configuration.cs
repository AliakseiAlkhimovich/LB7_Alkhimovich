namespace LB7_Alkhimovich.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LB7_Alkhimovich.CarContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LB7_Alkhimovich.CarContext";
        }

        protected override void Seed(LB7_Alkhimovich.CarContext context)
        {
           
        }
    }
}
