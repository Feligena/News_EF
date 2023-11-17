namespace HW_EF.Models
{
    public static class BlogDbInitialazer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var servise = app.ApplicationServices.CreateScope();
            var context = servise.ServiceProvider.GetRequiredService<BlogDbContext>();

            if (!context.Categories.Any())
            {
                context.Categories.Add(new Category { Name = "Culture" });
                context.Categories.Add(new Category { Name = "Economy" });
                context.Categories.Add(new Category { Name = "Science and technology" });
                context.Categories.Add(new Category { Name = "Policy" });
                context.Categories.Add(new Category { Name = "Social media" });
                context.Categories.Add(new Category { Name = "Business" });
                context.Categories.Add(new Category { Name = "Medicine" });
                context.Categories.Add(new Category { Name = "Trips" });
                context.Categories.Add(new Category { Name = "Rest" });
                context.Categories.Add(new Category { Name = "Real estate" });
                context.SaveChanges();
            }
        }
    }
}

