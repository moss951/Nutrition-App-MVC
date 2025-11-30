using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nutrition_App.Data;
using Nutrition_App.Entities;
using Nutrition_App.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FoodDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("FoodDbConnection")));
builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("UserDbConnection")));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<UserDbContext>().AddDefaultTokenProviders();
builder.Services.AddDbContext<DietLogDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DietLogDbConnection")));
builder.Services.AddDbContext<DietGoalDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DietGoalDbConnection")));
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/Login";
    options.AccessDeniedPath = "/User/Login";
});

// Register the dependency to IUserServices
builder.Services.AddScoped<IUserServices, UserServices>(); // Must be AddScoped()
builder.Services.AddScoped<IFoodServices, FoodServices>();
builder.Services.AddScoped<IDietLogServices, DietLogServices>();
builder.Services.AddScoped<IDietGoalServices, DietGoalServices>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FoodDbContext>();

    // create database from JSON if not already done
    if (!context.Foods.Any())
    {
        var json = File.ReadAllText("./Data/FoodData_Central_Survey_Food.json");
        var foodList = JsonConvert.DeserializeObject<FoodList>(json);

        if (foodList != null)
        {
            // keep track of existing entities for normalization
            var attributeTypes = context.FoodAttributeTypes.ToDictionary(t => t.Id);
            var measureUnits = context.MeasureUnits.ToDictionary(u => u.Id);
            var wweiaFoodCategories = context.WweiaFoodCategories.ToDictionary(c => c.Id);
            var nutrients = context.Nutrients.ToDictionary(n => n.Id);
            var foodPortions = context.FoodPortions.ToDictionary(p => p.Id);

            foreach (var food in foodList.SurveyFoods)
            {
                // update existing attribute types
                foreach (var attr in food.FoodAttributes ?? Enumerable.Empty<FoodAttribute>())
                {
                    if (attr.FoodAttributeType != null)
                    {
                        if (attributeTypes.TryGetValue(attr.FoodAttributeType.Id, out var existingType))
                        {
                            // type exists
                            attr.FoodAttributeType = existingType;
                        }
                        else
                        {
                            // type doesn't exist
                            attributeTypes[attr.FoodAttributeType.Id] = attr.FoodAttributeType;
                        }
                    }
                }

                // update existing food categories
                if (food.WweiaFoodCategory != null)
                {
                    if (wweiaFoodCategories.TryGetValue(food.WweiaFoodCategory.Id, out var existingCategory))
                    {
                        food.WweiaFoodCategory = existingCategory;
                    }
                    else
                    {
                        wweiaFoodCategories[food.WweiaFoodCategory.Id] = food.WweiaFoodCategory;
                    }
                }

                // update existing food portions and measurement units
                foreach (var portion in (food.FoodPortions ?? Enumerable.Empty<FoodPortion>()).ToList())
                {
                    if (portion.MeasureUnit != null)
                    {
                        if (measureUnits.TryGetValue(portion.MeasureUnit.Id, out var existingUnit))
                        {
                            portion.MeasureUnit = existingUnit;
                        }
                        else
                        {
                            measureUnits[portion.MeasureUnit.Id] = portion.MeasureUnit;
                        }
                    }

                    if (foodPortions.TryGetValue(portion.Id, out var existingPortion))
                    {
                        food.FoodPortions[food.FoodPortions.IndexOf(portion)] = existingPortion;
                    }
                    else
                    {
                        foodPortions[portion.Id] = portion;
                    }
                }

                // update existing nutrients
                foreach (var nutrient in food.FoodNutrients ?? Enumerable.Empty<FoodNutrient>())
                {
                    if (nutrient.Nutrient != null)
                    {
                        if (nutrients.TryGetValue(nutrient.Nutrient.Id, out var existingNutrient))
                        {
                            nutrient.Nutrient = existingNutrient;
                        }
                        else
                        {
                            nutrients[nutrient.Nutrient.Id] = nutrient.Nutrient;
                        }
                    }
                }

                context.Foods.Add(food);
            }
            context.SaveChanges();
        }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
